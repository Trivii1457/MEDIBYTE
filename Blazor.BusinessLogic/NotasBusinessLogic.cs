using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Blazor.Infrastructure.Entities.Models;
using DevExpress.Compression;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Blazor.BusinessLogic
{
    public class NotasBusinessLogic : GenericBusinessLogic<Notas>
    {
        public NotasBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public NotasBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public async Task EnviarEmail(Notas nota, string pathPdf)
        {
            if (string.IsNullOrWhiteSpace(nota.DIANResponse))
            {
                throw new Exception("La nota no ha sido aceptada por la dian.");
            }
            else if (!nota.DIANResponse.Contains("DIAN Aceptado"))
            {
                throw new Exception("La nota no ha sido aceptada por la dian.");
            }

            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            try
            {
                string correo = null;
                if (nota.Facturas.AdmisionesId != null)
                    correo = unitOfWork.Repository<Pacientes>().GetTable().FirstOrDefault(x => x.Id == nota.PacientesId)?.CorreoElectronico;
                else
                    correo = unitOfWork.Repository<Entidades>().GetTable().FirstOrDefault(x => x.Id == nota.EntidadesId)?.CorreoElectronico;

                byte[] contentarray = null;
                HttpClient http = new HttpClient();
                var response = await http.GetAsync(nota.XmlUrl);
                if (response.IsSuccessStatusCode)
                    contentarray = await response.Content.ReadAsByteArrayAsync();
                else
                    throw new Exception($"Error en descargar XMl desde acepta. | {response.StatusCode} - {response.ReasonPhrase}");
                string content = Encoding.UTF8.GetString(contentarray);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);
                content = @"<?xml version='1.0' encoding='UTF-8'?>";
                content += doc.DocumentElement.ChildNodes[3].InnerXml;


                string pathXml = Path.Combine(Path.GetTempPath(), $"{nota.Documentos.Prefijo}-{nota.Consecutivo}.xml");
                File.WriteAllText(pathXml, content, Encoding.UTF8);

                ZipArchive archive = new ZipArchive();
                archive.FileName = $"{nota.Documentos.Prefijo}-{nota.Consecutivo}.zip";
                archive.AddFile(pathPdf, "/");
                archive.AddFile(pathXml, "/");
                MemoryStream msZip = new MemoryStream();
                archive.Save(msZip);
                msZip = new MemoryStream(msZip.ToArray());

                Empresas empresas = unitOfWork.Repository<Empresas>().FindById(x => x.Id == nota.EmpresasId, false);

                EnvioEmailConfig envioEmailConfig = new EnvioEmailConfig();
                envioEmailConfig.Origen = "FACTURACION";
                envioEmailConfig.Asunto = $"Envio Nota Electronica {nota.Documentos.Prefijo}-{nota.Consecutivo}";
                envioEmailConfig.Template = "EmailEnvioNotaElectronica";
                envioEmailConfig.Destinatarios.Add(correo);
                envioEmailConfig.ArchivosAdjuntos.Add($"{nota.Documentos.Prefijo}-{nota.Consecutivo}.zip", msZip);
                envioEmailConfig.Datos = new Dictionary<string, string>
                {
                    {"nombreCia",$"{empresas.RazonSocial}" }
                };

                new ConfiguracionEnvioEmailBusinessLogic(this.UnitOfWork).EnviarEmail(envioEmailConfig);

            }
            catch (Exception e)
            {
                string fullError = e.Message;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    fullError += " | " + e.Message;
                }

                DApp.LogToFile($"{fullError} | {e.StackTrace}");
                throw new Exception(fullError);
            }
        }

        public override Notas Add(Notas data)
        {
            var documento = new GenericBusinessLogic<Documentos>(this.UnitOfWork.Settings).FindById(x => x.Id == data.DocumentosId, false);
            try
            {
                data.Consecutivo = new GenericBusinessLogic<Documentos>(this.UnitOfWork.Settings).GetSecuence($"{documento.Prefijo}");
            }
            catch (Exception e)
            {
                throw new Exception($"Error obteniendo consecutivo para {data.SedesId}-{documento.Prefijo}. | {e.Message}");
            }
            return base.Add(data);
        }

        public Notas AprobarNota(Notas nota)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                nota.Documentos = unitOfWork.Repository<Documentos>().FindById(x => x.Id == nota.DocumentosId, false);
                nota.NotasConceptos = unitOfWork.Repository<NotasConceptos>().FindById(x => x.Id == nota.NotasConceptosId, false);
                nota.Facturas = unitOfWork.Repository<Facturas>().FindById(x => x.Id == nota.FacturasId, false);

                var detalles = unitOfWork.Repository<NotasDetalles>().Table.Where(x => x.NotasId == nota.Id);
                nota.ValorTotal = detalles.Sum(x => x.ValorTotal);
                nota.ValorSubtotal = detalles.Sum(x => x.SubTotal);
                nota.ValorDescuentos = detalles.Sum(x => x.SubTotal * (x.PorcDescuento / 100));
                nota.ValorImpuestos = detalles.Sum(x => (x.SubTotal - (x.SubTotal * (x.PorcDescuento / 100))) * (x.PorcImpuesto / 100));
                nota.MontoEscrito = DApp.Util.NumeroEnLetras(nota.ValorTotal);
                nota.Estadosid = 10085;
                nota = unitOfWork.Repository<Notas>().Modify(nota);

                if (nota.FacturasId != null && nota.Documentos.Transaccion == 3)
                {
                    nota.Facturas.Saldo -= nota.ValorTotal;
                }
                else if (nota.FacturasId != null && nota.Documentos.Transaccion == 4)
                {
                    nota.Facturas.Saldo += nota.ValorTotal;
                }
                if (nota.Facturas.Saldo < 0)
                    nota.Facturas.Saldo = 0;

                nota.Facturas.TieneNotas = true;
                if (nota.Documentos.Transaccion == 3 && nota.NotasConceptos.Codigo == "2")
                {
                    nota.Facturas.Estadosid = unitOfWork.Repository<Estados>().FindById(x => x.Tipo == "FACTURA" && x.Nombre == "ANULADA", false).Id;
                }
                unitOfWork.Repository<Facturas>().Modify(nota.Facturas);

                if (nota.Documentos.Transaccion == 3 && nota.NotasConceptos.Codigo == "1")
                {
                    var pacientesId = detalles.Select(x=>x.PacientesId).Distinct().ToList();
                    if (pacientesId.Count > 0)
                    {
                        var listServices = new BlazorUnitWork(UnitOfWork.Settings).Repository<AdmisionesServiciosPrestados>().Table.Include(x => x.Admisiones).Where(x => x.FacturasId == nota.FacturasId || x.Admisiones.FacturaCopagoCuotaModeradoraId == nota.FacturasId && pacientesId.Contains(x.Admisiones.PacientesId)).ToList();
                        var listAdminsiones = listServices.Select(x => x.Admisiones).Distinct().ToList();

                        if (listAdminsiones != null && listAdminsiones.Count > 0)
                            foreach (var adm in listAdminsiones)
                            {
                                adm.Facturado = false;
                                adm.FacturaCopagoCuotaModeradoraId = null;
                                unitOfWork.Repository<Admisiones>().Modify(adm);
                            }

                        if (listServices != null && listServices.Count > 0)
                            foreach (var ser in listServices)
                            {
                                ser.Facturado = false;
                                ser.FacturasId = null;
                                ser.FacturasGeneracionId = null;
                                unitOfWork.Repository<AdmisionesServiciosPrestados>().Modify(ser);
                            }
                    }
                }

                if (nota.Documentos.Transaccion == 3 && nota.NotasConceptos.Codigo == "2")
                {
                    var listServices = new BlazorUnitWork(UnitOfWork.Settings).Repository<AdmisionesServiciosPrestados>().Table.Include(x => x.Admisiones).Where(x => x.FacturasId == nota.FacturasId || x.Admisiones.FacturaCopagoCuotaModeradoraId == nota.FacturasId).ToList();
                    var listAdminsiones = listServices.Select(x => x.Admisiones).Distinct().ToList();

                    if(listAdminsiones!=null && listAdminsiones.Count>0)
                        foreach (var adm in listAdminsiones)
                        {
                            adm.Facturado = false;
                            adm.FacturaCopagoCuotaModeradoraId = null;
                            unitOfWork.Repository<Admisiones>().Modify(adm);
                        }

                    if (listServices != null && listServices.Count > 0)
                        foreach (var ser in listServices)
                        {
                            ser.Facturado = false;
                            ser.FacturasId = null;
                            ser.FacturasGeneracionId = null;
                            unitOfWork.Repository<AdmisionesServiciosPrestados>().Modify(ser);
                        }
                }

                unitOfWork.CommitTransaction();
                return nota;
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public Notas AnularNota(Notas nota)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                nota.Documentos = unitOfWork.Repository<Documentos>().FindById(x => x.Id == nota.DocumentosId, false);
                nota.Facturas = unitOfWork.Repository<Facturas>().FindById(x => x.Id == nota.FacturasId, false);

                var detalles = unitOfWork.Repository<NotasDetalles>().Table.Where(x => x.NotasId == nota.Id);
                nota.ValorTotal = detalles.Sum(x => x.ValorTotal);
                nota.ValorSubtotal = detalles.Sum(x => x.SubTotal);
                nota.ValorDescuentos = detalles.Sum(x => x.SubTotal * (x.PorcDescuento / 100));
                nota.ValorImpuestos = detalles.Sum(x => (x.SubTotal - (x.SubTotal * (x.PorcDescuento / 100))) * (x.PorcImpuesto / 100));
                nota.MontoEscrito = DApp.Util.NumeroEnLetras(nota.ValorTotal);
                nota.Estadosid = 10086;
                nota = unitOfWork.Repository<Notas>().Modify(nota);

                if (nota.FacturasId != null && nota.Documentos.Transaccion == 3)
                {
                    nota.Facturas.Saldo += nota.ValorTotal;
                }
                else if (nota.FacturasId != null && nota.Documentos.Transaccion == 4)
                {
                    nota.Facturas.Saldo -= nota.ValorTotal;
                }
                var totalFacturasConNotas = unitOfWork.Repository<Notas>().Table.Where(x => x.FacturasId == nota.FacturasId && x.Estadosid == 10085).Count();
                if (totalFacturasConNotas == 0)
                    nota.Facturas.TieneNotas = false;
                unitOfWork.Repository<Facturas>().Modify(nota.Facturas);
                unitOfWork.CommitTransaction();
                return nota;
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public ECreditNote GetCreditNote(long idNota)
        {
            Notas nota = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<Notas>()
                .Tabla().Include(x => x.Empresas)
                .Include(x => x.Empresas.TiposRegimenContable)
                .Include(x => x.Empresas.Ciudades)
                .Include(x => x.Empresas.Ciudades.Departamentos)
                .Include(x => x.Empresas.Ciudades.Departamentos.Paises)
                .Include(x => x.Empresas.TiposPersonas)
                .Include(x => x.Empresas.TiposIdentificacion)
                .Include(x => x.Sedes)
                .Include(x => x.Sedes.Ciudades)
                .Include(x => x.Sedes.Ciudades.Departamentos)
                .Include(x => x.Sedes.Ciudades.Departamentos.Paises)
                .Include(x => x.Pacientes)
                .Include(x => x.Pacientes.TiposIdentificacion)
                .Include(x => x.Pacientes.Ciudades)
                .Include(x => x.Pacientes.Ciudades.Departamentos)
                .Include(x => x.Pacientes.Ciudades.Departamentos.Paises)
                .Include(x => x.Entidades)
                .Include(x => x.Entidades.TiposPersonas)
                .Include(x => x.Entidades.TiposIdentificacion)
                .Include(x => x.Entidades.Ciudades)
                .Include(x => x.Entidades.Ciudades.Departamentos)
                .Include(x => x.Entidades.Ciudades.Departamentos.Paises)
                .Include(x => x.Documentos)
                .Include(x => x.NotasConceptos)
                .FirstOrDefault(x => x.Id == idNota);
            nota.NotasDetalles = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<NotasDetalles>().Tabla()
                .Include(x => x.Servicios)
                .Include(x => x.Servicios.Cups)
                .Include(x => x.Servicios.TiposImpuestos)

                .Where(x => x.NotasId == idNota).ToList();

            Facturas invoiceRef = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<Facturas>().FindById(x => x.Id == (long)nota.FacturasId, false);

            TaxScheme taxSchemeClient = null;

            if (nota.Empresas != null)
            {
                nota.Empresas.EmpresasEsquemasImpuestos = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<EmpresasEsquemasImpuestos>().FindAll(x => x.EmpresasId == nota.EmpresasId, true);
                nota.Empresas.EmpresasResponsabilidadesFiscales = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<EmpresasResponsabilidadesFiscales>().FindAll(x => x.EmpresasId == nota.EmpresasId, true);
            }

            if (nota.Entidades != null)
            {
                nota.Entidades.EntidadesEsquemasImpuestos = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<EntidadesEsquemasImpuestos>().FindAll(x => x.EntidadesId == nota.EntidadesId, true);
                nota.Entidades.EntidadesResponsabilidadesFiscales = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<EntidadesResponsabilidadesFiscales>().FindAll(x => x.EntidadesId == nota.EntidadesId, true);
                taxSchemeClient = new TaxScheme { ID = nota.Entidades.GetCodigoImpuestoRecaudados(), Name = nota.Entidades.GetNombreImpuestoRecaudados() };
            }

            List<TotalByTaxScheme> InvoiceDetailsByTax = nota.NotasDetalles.GroupBy(x => new { x.Servicios.TiposImpuestos.Codigo, x.PorcImpuesto }).Select(g => new TotalByTaxScheme { TaxSchemeCode = g.Key.Codigo, TaxPercentaje = g.Key.PorcImpuesto, Value = g.Sum(x => Math.Round(x.ValorServicio * x.Cantidad, 2)), DiscountValue = g.Sum(x => Math.Round(x.DiscountValue, 2)), TaxValue = g.Sum(x => Math.Round(x.TaxValue, 2)) }).ToList();

            ECreditNote enote = new ECreditNote();

            enote.IssueDate = nota.Fecha;
            enote.IssueTime = nota.Fecha;
            enote.CreditNoteTypeCode = "91";
            if (string.IsNullOrWhiteSpace(nota.Referencia))
                enote.CustomizationID = "22";
            else
                enote.CustomizationID = "20";

            enote.ID = nota.Documentos.Prefijo + "" + nota.Consecutivo.ToString();
            enote.DocumentCurrencyCode = "COP";
            if (nota.PeriodoInicial > nota.PeriodoFinal)
            {
                enote.CreditNotePeriod = new InvoicePeriod { StartDate = (DateTime)nota.PeriodoInicial, EndDate = (DateTime)nota.PeriodoFinal };
            }

            enote.DiscrepancyResponse = new DiscrepancyResponse { ReferenceID = nota.Referencia, ResponseCode = nota.NotasConceptos.Codigo, Description = Regex.Replace(nota.NotasConceptos.Descripcion.Replace(";", ""), @"[^a-zA-z0-9 ]+", "") };
            if (invoiceRef != null)
            {

                enote.BillingReference = new List<InvoiceDocumentReference>();
                InvoiceDocumentReference billRef = new InvoiceDocumentReference();
                billRef.ID = nota.Referencia;
                billRef.UUID = invoiceRef.CUFE;
                billRef.IssueDate = invoiceRef.IssueDate.GetValueOrDefault();
                enote.BillingReference.Add(billRef);

            }

            enote.AccountingSupplierParty = new AccountingSupplierParty();
            enote.AccountingSupplierParty.AdditionalAccountID = nota.Empresas.TiposPersonas.Codigo;
            enote.AccountingSupplierParty.Party = new Party();
            enote.AccountingSupplierParty.Party.PartyIdentification = nota.Empresas.TiposIdentificacion.CodigoAlterno;
            enote.AccountingSupplierParty.Party.ID = nota.Empresas.NumeroIdentificacion;
            enote.AccountingSupplierParty.Party.schemeID = nota.Empresas.DV;
            enote.AccountingSupplierParty.Party.PartyName = new PartyName { Name = nota.Empresas.RazonSocial };
            enote.AccountingSupplierParty.Party.PhysicalLocation = new PhysicalLocation();
            enote.AccountingSupplierParty.Party.PhysicalLocation.Address = new Address();
            enote.AccountingSupplierParty.Party.PhysicalLocation.Address.ID = nota.Empresas.Ciudades.Codigo;
            enote.AccountingSupplierParty.Party.PhysicalLocation.Address.CityName = nota.Empresas.Ciudades.Nombre;
            enote.AccountingSupplierParty.Party.PhysicalLocation.Address.CountrySubentity = nota.Empresas.Ciudades.Departamentos.Nombre;
            enote.AccountingSupplierParty.Party.PhysicalLocation.Address.CountrySubentityCode = nota.Empresas.Ciudades.Departamentos.Codigo;
            enote.AccountingSupplierParty.Party.PhysicalLocation.Address.AddressLine = new AddressLine { Line = nota.Empresas.Direccion };

            enote.AccountingSupplierParty.Party.PhysicalLocation.Address.Country = new CountryE { IdentificationCode = nota.Empresas.Ciudades.Departamentos.Paises.Codigo, Name = nota.Empresas.Ciudades.Departamentos.Paises.Nombre };

            enote.AccountingSupplierParty.Party.PartyTaxScheme = new PartyTaxScheme { RegistrationName = nota.Empresas.RazonSocial, TaxLevelCode = nota.Empresas.GetResponsabilidadesFiscales(), TaxScheme = new TaxScheme { ID = nota.Empresas.GetCodigoImpuestoRecaudados(), Name = nota.Empresas.GetNombreImpuestoRecaudados() }, ListName = nota.Empresas.TiposRegimenContable.Codigo };

            enote.SellerSupplierParty = new SellerSupplierParty();
            enote.SellerSupplierParty.ID = nota.Sedes.Codigo;
            enote.SellerSupplierParty.Party = new Party1();

            enote.SellerSupplierParty.Party.PartyName = new PartyName { Name = nota.Sedes.Nombre };
            enote.SellerSupplierParty.Party.PhysicalLocation = new PhysicalLocation();

            enote.SellerSupplierParty.Party.PhysicalLocation.Address = new Address();
            enote.SellerSupplierParty.Party.PhysicalLocation.Address.ID = nota.Sedes.Ciudades.Codigo;
            enote.SellerSupplierParty.Party.PhysicalLocation.Address.CityName = nota.Sedes.Ciudades.Nombre;
            enote.SellerSupplierParty.Party.PhysicalLocation.Address.CountrySubentity = nota.Sedes.Ciudades.Departamentos.Nombre;
            enote.SellerSupplierParty.Party.PhysicalLocation.Address.CountrySubentityCode = nota.Sedes.Ciudades.Departamentos.Codigo;
            enote.SellerSupplierParty.Party.PhysicalLocation.Address.AddressLine = new AddressLine { Line = nota.Sedes.Direccion };
            enote.SellerSupplierParty.Party.PhysicalLocation.Address.Country = new CountryE { IdentificationCode = nota.Sedes.Ciudades.Departamentos.Paises.Codigo, Name = nota.Sedes.Ciudades.Departamentos.Paises.Nombre };

            if (nota.Pacientes != null)
            {
                enote.AccountingCustomerParty = new AccountingCustomerParty();
                enote.AccountingCustomerParty.AdditionalAccountID = "2";
                enote.AccountingCustomerParty.Party = new Party2();
                enote.AccountingCustomerParty.Party.PartyIdentification = nota.Pacientes.TiposIdentificacion.CodigoAlterno;
                enote.AccountingCustomerParty.Party.ID = nota.Pacientes.NumeroIdentificacion.Trim();
                enote.AccountingCustomerParty.Party.Person = new Person { FirstName = nota.Pacientes.NombreCompleto, LastName = nota.Pacientes.PrimerNombre + " " + nota.Pacientes.SegundoNombre + " " + nota.Pacientes.SegundoApellido };

                enote.AccountingCustomerParty.Party.PhysicalLocation = new PhysicalLocation { Address = new Address() };

                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.ID = nota.Pacientes.Ciudades.Codigo;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.CityName = nota.Pacientes.Ciudades.Nombre;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.CountrySubentity = nota.Pacientes.Ciudades.Departamentos.Nombre;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.CountrySubentityCode = nota.Pacientes.Ciudades.Departamentos.Codigo;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.AddressLine = new AddressLine { Line = nota.Pacientes.Direccion };

                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.Country = new CountryE { IdentificationCode = nota.Pacientes.Ciudades.Departamentos.Paises.Codigo, Name = nota.Pacientes.Ciudades.Departamentos.Paises.Nombre };

                enote.AccountingCustomerParty.Party.PartyTaxScheme = new PartyTaxScheme2();
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationName = nota.Pacientes.NombreCompleto;
                enote.AccountingCustomerParty.Party.PartyTaxScheme.TaxLevelCode = "R-99-PN";
                enote.AccountingCustomerParty.Party.PartyTaxScheme.ListName = "No aplica";
                enote.AccountingCustomerParty.Party.PartyTaxScheme.TaxScheme = new TaxScheme { ID = "ZZ", Name = "No aplica" };
            }
            else
            {
                enote.AccountingCustomerParty = new AccountingCustomerParty();
                enote.AccountingCustomerParty.AdditionalAccountID = nota.Entidades.TiposPersonas.Codigo;

                enote.AccountingCustomerParty.Party = new Party2();
                enote.AccountingCustomerParty.Party.PartyIdentification = nota.Entidades.TiposIdentificacion.CodigoAlterno;
                enote.AccountingCustomerParty.Party.ID = nota.Entidades.NumeroIdentificacion.Trim();
                enote.AccountingCustomerParty.Party.schemeID = nota.Entidades.DV;
                enote.AccountingCustomerParty.Party.PartyName = new PartyName { Name = nota.Entidades.Nombre };

                enote.AccountingCustomerParty.Party.PhysicalLocation = new PhysicalLocation { Address = new Address() };

                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.ID = nota.Entidades.Ciudades.Codigo;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.CityName = nota.Entidades.Ciudades.Nombre;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.CountrySubentity = nota.Entidades.Ciudades.Departamentos.Nombre;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.CountrySubentityCode = nota.Entidades.Ciudades.Departamentos.Codigo;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.AddressLine = new AddressLine { Line = nota.Entidades.Direccion };

                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.Country = new CountryE { IdentificationCode = nota.Entidades.Ciudades.Departamentos.Paises.Codigo, Name = nota.Entidades.Ciudades.Departamentos.Paises.Nombre };

                enote.AccountingCustomerParty.Party.PartyTaxScheme = new PartyTaxScheme2();
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationName = nota.Entidades.Nombre;
                enote.AccountingCustomerParty.Party.PartyTaxScheme.TaxLevelCode = nota.Entidades.GetResponsabilidadesFiscales();
                enote.AccountingCustomerParty.Party.PartyTaxScheme.ListName = nota.Entidades.GetRegimenFiscal();
                enote.AccountingCustomerParty.Party.PartyTaxScheme.TaxScheme = new TaxScheme { ID = taxSchemeClient.ID, Name = taxSchemeClient.Name };
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress = new Address();
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress.ID = nota.Entidades.Ciudades.Codigo;
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress.CityName = nota.Entidades.Ciudades.Nombre;
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress.CountrySubentity = nota.Entidades.Ciudades.Departamentos.Nombre;
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress.CountrySubentityCode = nota.Entidades.Ciudades.Departamentos.Codigo;
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress.AddressLine = new AddressLine { Line = nota.Entidades.Direccion };

                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress.Country = new CountryE { IdentificationCode = nota.Entidades.Ciudades.Departamentos.Paises.Codigo, Name = nota.Entidades.Ciudades.Departamentos.Paises.Nombre };

            }

            enote.PaymentMeans = new PaymentMeans { ID = ((nota.FormasPagos.Codigo == "1") ? "1" : "2"), PaymentDueDate = nota.Fecha.AddDays((double)nota.FormasPagos.DiasVencimiento) };

            enote.TaxTotal = new List<TaxTotal>();

            foreach (var item in InvoiceDetailsByTax)
            {
                if (item.TaxValue > 0)
                {
                    TaxTotal tax = new TaxTotal { TaxAmount = item.TaxValue };
                    tax.TaxSubtotal = new TaxSubtotal();
                    tax.TaxSubtotal.TaxableAmount = (item.TaxValue > 0) ? item.Value - item.DiscountValue : 0;
                    tax.TaxSubtotal.TaxAmount = (decimal)item.TaxValue;
                    if (item.TaxSchemeCode == "22")
                    {
                        tax.TaxSubtotal.BaseUnitMeasure = "B%";
                        tax.TaxSubtotal.UnitCode = "NAR";
                        tax.TaxSubtotal.PerUnitAmount = new PerUnitAmountType { Value = 50 };
                    }
                    tax.TaxSubtotal.TaxCategory = new TaxCategory();
                    if (item.TaxSchemeCode != "22")
                        tax.TaxSubtotal.TaxCategory.Percent = new PercentType { Value = item.TaxPercentaje };
                    tax.TaxSubtotal.TaxCategory.TaxScheme = new TaxScheme { ID = item.TaxSchemeCode, Name = item.TaxSchemeName };

                    enote.TaxTotal.Add(tax);
                }
            }
            if (enote.TaxTotal.Count == 0)
                enote.TaxTotal = null;

            enote.LegalMonetaryTotal = new LegalMonetaryTotal();
            enote.LegalMonetaryTotal.LineExtensionAmount = nota.ValorSubtotal - nota.ValorDescuentos;
            enote.LegalMonetaryTotal.TaxExclusiveAmount = nota.ValorImpuestos;

            enote.LegalMonetaryTotal.TaxExclusiveBaseAmount = nota.ValorImpuestos > 0 ? (nota.ValorSubtotal - nota.ValorDescuentos) : 0;

            enote.LegalMonetaryTotal.TaxInclusiveAmount = (nota.ValorSubtotal - nota.ValorDescuentos + nota.ValorImpuestos);
            //enote.LegalMonetaryTotal.PrePaidAmount = new PrePaidAmountType { Value = nota.ValorCopagoCuotaModeradora };

            enote.LegalMonetaryTotal.AllowanceLineAmount = new AllowanceLineAmountType { Value = nota.ValorDescuentos };
            enote.LegalMonetaryTotal.PayableAmount = nota.ValorTotal;
            enote.LegalMonetaryTotal.PayableExpectedAmount = nota.ValorTotal;
            //enote.LegalMonetaryTotal.TextAmount = WrittenAmount.ConvertToString(nota.ValorTotal);
            enote.LegalMonetaryTotal.TextAmount = DApp.Util.NumeroEnLetras(nota.ValorTotal);
            enote.LegalMonetaryTotal.LineCountNumeric = new LineCountNumericType { Value = nota.NotasDetalles.Count };
            enote.CreditNoteLine = new List<CreditLine>();

            for (int i = 0; i < nota.NotasDetalles.Count; i++)
            {
                CreditLine data = new CreditLine();
                data.ID = (i + 1);
                var cup = nota.NotasDetalles[i].Servicios.Cups;
                if (cup != null)
                {
                    data.StandardItemIdentification = new StandardItemIdentification { ID = nota.NotasDetalles[i].Servicios.Cups.Codigo.TrimStart().TrimEnd(), schemeID = "999" };
                }
                else
                {
                    data.StandardItemIdentification = new StandardItemIdentification { ID = nota.NotasDetalles[i].Servicios.Codigo.TrimStart().TrimEnd(), schemeID = "999" };
                }
                data.CreditedQuantity = nota.NotasDetalles[i].Cantidad;
                data.UnitCode = "NAR";
                data.LineExtensionAmount = nota.NotasDetalles[i].SubTotalValue - nota.NotasDetalles[i].DiscountValue;
                //data.NetLineExtensionAmount = new NetLineExtensionAmountType { Value = InvoiceDetails[i].TotalValue }  ;
                data.Price = new Price { PriceAmount = nota.NotasDetalles[i].ValorServicio, BaseQuantity = nota.NotasDetalles[i].Cantidad };
                data.Item = new Item { Description = new List<string>() };
                data.Item.AddDescription(nota.NotasDetalles[i].Servicios.Nombre.Replace("Í", "I"));

                if (nota.NotasDetalles[i].PorcDescuento > 0)
                {
                    data.AllowanceCharge = new List<AllowanceCharge>();
                    AllowanceCharge discount = new AllowanceCharge();
                    discount.ID = 1;
                    discount.ChargeIndicator = false;
                    discount.AllowanceChargeReason = "Descuento general";
                    discount.MultiplierFactorNumeric = nota.NotasDetalles[i].PorcDescuento;
                    discount.BaseAmount = nota.NotasDetalles[i].SubTotalValue;
                    discount.Amount = nota.NotasDetalles[i].DiscountValue;
                    data.AllowanceCharge.Add(discount);
                }
                if (nota.NotasDetalles[i].TaxValue > 0)
                {
                    data.TaxTotal = new TaxTotal();
                    data.TaxTotal.TaxAmount = nota.NotasDetalles[i].TaxValue;
                    data.TaxTotal.TaxSubtotal = new TaxSubtotal { TaxableAmount = nota.NotasDetalles[i].TaxValue > 0 ? nota.NotasDetalles[i].SubTotalValue - nota.NotasDetalles[i].DiscountValue : 0, TaxAmount = nota.NotasDetalles[i].TaxValue };
                    data.TaxTotal.TaxSubtotal.TaxCategory = new TaxCategory();
                    data.TaxTotal.TaxSubtotal.TaxCategory.Percent = new PercentType { Value = nota.NotasDetalles[i].PorcImpuesto };
                    data.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme = new TaxScheme { ID = nota.NotasDetalles[i].Servicios.TiposImpuestos.Codigo, Name = nota.NotasDetalles[i].Servicios.TiposImpuestos.Descripcion };
                }
                enote.CreditNoteLine.Add(data);

            }

            return enote;
        }

        public async Task<string> SendCreditNoteAsync(long idNota, string urlAcepta)
        {
            string content = "";
            ECreditNote invoice = GetCreditNote(idNota);
            string xml = invoice.SerializeToXml();
                //.Replace(@"<?xml version=""1.0"" encoding=""utf-8""?>", "");
            HttpClient http = new HttpClient();
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("docid", invoice.ID));
            nvc.Add(new KeyValuePair<string, string>("comando", "emitir"));
            nvc.Add(new KeyValuePair<string, string>("parametros", ""));
            nvc.Add(new KeyValuePair<string, string>("datos", xml));

            var encodedItems = nvc.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
            var encodedContent = new StringContent(String.Join("&", encodedItems), null, "application/x-www-form-urlencoded");

            var response = await http.PostAsync(urlAcepta, encodedContent);

            //var response = await http.PostAsync(urlAcepta, new FormUrlEncodedContent(nvc));
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }
            //if (content.StartsWith("Error"))
            //    factura.ErrorReference = content;

            //Manager.GetBusinessLogic<InterfaceInvoice>().Modify(factura);
            return content;
        }

        public EDebitNote GetDebitNote(long idNota)
        {
            Notas nota = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<Notas>()
                .Tabla().Include(x => x.Empresas)
                .Include(x => x.Empresas.TiposRegimenContable)
                .Include(x => x.Empresas.Ciudades)
                .Include(x => x.Empresas.Ciudades.Departamentos)
                .Include(x => x.Empresas.Ciudades.Departamentos.Paises)
                .Include(x => x.Empresas.TiposPersonas)
                .Include(x => x.Empresas.TiposIdentificacion)
                .Include(x => x.Sedes)
                .Include(x => x.Sedes.Ciudades)
                .Include(x => x.Sedes.Ciudades.Departamentos)
                .Include(x => x.Sedes.Ciudades.Departamentos.Paises)
                .Include(x => x.Pacientes)
                .Include(x => x.Pacientes.TiposIdentificacion)
                .Include(x => x.Pacientes.Ciudades)
                .Include(x => x.Pacientes.Ciudades.Departamentos)
                .Include(x => x.Pacientes.Ciudades.Departamentos.Paises)
                .Include(x => x.Entidades)
                .Include(x => x.Entidades.TiposPersonas)
                .Include(x => x.Entidades.TiposIdentificacion)
                .Include(x => x.Entidades.Ciudades)
                .Include(x => x.Entidades.Ciudades.Departamentos)
                .Include(x => x.Entidades.Ciudades.Departamentos.Paises)
                .Include(x => x.Documentos)
                .Include(x => x.NotasConceptos)
                .FirstOrDefault(x => x.Id == idNota);
            nota.NotasDetalles = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<NotasDetalles>().Tabla()
                .Include(x => x.Servicios)
                .Include(x => x.Servicios.Cups)
                .Include(x => x.Servicios.TiposImpuestos)

                .Where(x => x.NotasId == idNota).ToList();

            Facturas invoiceRef = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<Facturas>().FindById(x => x.Id == (long)nota.FacturasId, false);

            TaxScheme taxSchemeClient = null;

            if (nota.Empresas != null)
            {
                nota.Empresas.EmpresasEsquemasImpuestos = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<EmpresasEsquemasImpuestos>().FindAll(x => x.EmpresasId == nota.EmpresasId,true);
                nota.Empresas.EmpresasResponsabilidadesFiscales = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<EmpresasResponsabilidadesFiscales>().FindAll(x => x.EmpresasId == nota.EmpresasId, true);
            }

            if (nota.Entidades != null)
            {
                nota.Entidades.EntidadesEsquemasImpuestos = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<EntidadesEsquemasImpuestos>().FindAll(x => x.EntidadesId == nota.EntidadesId, true);
                nota.Entidades.EntidadesResponsabilidadesFiscales = new Dominus.Backend.DataBase.BusinessLogic(this.UnitOfWork.Settings).GetBusinessLogic<EntidadesResponsabilidadesFiscales>().FindAll(x => x.EntidadesId == nota.EntidadesId, true);
                taxSchemeClient = new TaxScheme { ID = nota.Entidades.GetCodigoImpuestoRecaudados(), Name = nota.Entidades.GetNombreImpuestoRecaudados() };
            }

            List<TotalByTaxScheme> InvoiceDetailsByTax = nota.NotasDetalles.GroupBy(x => new { x.Servicios.TiposImpuestos.Codigo, x.PorcImpuesto }).Select(g => new TotalByTaxScheme { TaxSchemeCode = g.Key.Codigo, TaxPercentaje = g.Key.PorcImpuesto, Value = g.Sum(x => Math.Round(x.ValorServicio * x.Cantidad, 2)), DiscountValue = g.Sum(x => Math.Round(x.DiscountValue, 2)), TaxValue = g.Sum(x => Math.Round(x.TaxValue, 2)) }).ToList();

            EDebitNote enote = new EDebitNote();

            enote.IssueDate = nota.Fecha;
            enote.IssueTime = nota.Fecha;
            enote.DebitNoteTypeCode = "92";
            if (string.IsNullOrWhiteSpace(nota.Referencia))
                enote.CustomizationID = "32";
            else
                enote.CustomizationID = "30";

            enote.ID = nota.Documentos.Prefijo + "" + nota.Consecutivo.ToString();
            enote.DocumentCurrencyCode = "COP";
            if (nota.PeriodoInicial > nota.PeriodoFinal)
            {
                enote.DebitNotePeriod = new InvoicePeriod { StartDate = (DateTime)nota.PeriodoInicial, EndDate = (DateTime)nota.PeriodoFinal };
            }

            enote.DiscrepancyResponse = new DiscrepancyResponse { ReferenceID = nota.Referencia, ResponseCode = nota.NotasConceptos.Codigo, Description = Regex.Replace(nota.NotasConceptos.Descripcion.Replace(";", ""), @"[^a-zA-z0-9 ]+", "") };

            if (invoiceRef != null)
            {

                enote.BillingReference = new List<InvoiceDocumentReference>();
                InvoiceDocumentReference billRef = new InvoiceDocumentReference();
                billRef.ID = nota.Referencia;
                billRef.UUID = invoiceRef.CUFE;
                billRef.IssueDate = invoiceRef.IssueDate.GetValueOrDefault();
                enote.BillingReference.Add(billRef);

            }

            enote.AccountingSupplierParty = new AccountingSupplierParty();
            enote.AccountingSupplierParty.AdditionalAccountID = nota.Empresas.TiposPersonas.Codigo;
            enote.AccountingSupplierParty.Party = new Party();
            enote.AccountingSupplierParty.Party.PartyIdentification = nota.Empresas.TiposIdentificacion.CodigoAlterno;
            enote.AccountingSupplierParty.Party.ID = nota.Empresas.NumeroIdentificacion;
            enote.AccountingSupplierParty.Party.schemeID = nota.Empresas.DV;
            enote.AccountingSupplierParty.Party.PartyName = new PartyName { Name = nota.Empresas.RazonSocial };
            enote.AccountingSupplierParty.Party.PhysicalLocation = new PhysicalLocation();
            enote.AccountingSupplierParty.Party.PhysicalLocation.Address = new Address();
            enote.AccountingSupplierParty.Party.PhysicalLocation.Address.ID = nota.Empresas.Ciudades.Codigo;
            enote.AccountingSupplierParty.Party.PhysicalLocation.Address.CityName = nota.Empresas.Ciudades.Nombre;
            enote.AccountingSupplierParty.Party.PhysicalLocation.Address.CountrySubentity = nota.Empresas.Ciudades.Departamentos.Nombre;
            enote.AccountingSupplierParty.Party.PhysicalLocation.Address.CountrySubentityCode = nota.Empresas.Ciudades.Departamentos.Codigo;
            enote.AccountingSupplierParty.Party.PhysicalLocation.Address.AddressLine = new AddressLine { Line = nota.Empresas.Direccion };

            enote.AccountingSupplierParty.Party.PhysicalLocation.Address.Country = new CountryE { IdentificationCode = nota.Empresas.Ciudades.Departamentos.Paises.Codigo, Name = nota.Empresas.Ciudades.Departamentos.Paises.Nombre };

            enote.AccountingSupplierParty.Party.PartyTaxScheme = new PartyTaxScheme { RegistrationName = nota.Empresas.RazonSocial, TaxLevelCode = nota.Empresas.GetResponsabilidadesFiscales(), TaxScheme = new TaxScheme { ID = nota.Empresas.GetCodigoImpuestoRecaudados(), Name = nota.Empresas.GetNombreImpuestoRecaudados() }, ListName = nota.Empresas.TiposRegimenContable.Codigo };

            enote.SellerSupplierParty = new SellerSupplierParty();
            enote.SellerSupplierParty.ID = nota.Sedes.Codigo;
            enote.SellerSupplierParty.Party = new Party1();

            enote.SellerSupplierParty.Party.PartyName = new PartyName { Name = nota.Sedes.Nombre };
            enote.SellerSupplierParty.Party.PhysicalLocation = new PhysicalLocation();

            enote.SellerSupplierParty.Party.PhysicalLocation.Address = new Address();
            enote.SellerSupplierParty.Party.PhysicalLocation.Address.ID = nota.Sedes.Ciudades.Codigo;
            enote.SellerSupplierParty.Party.PhysicalLocation.Address.CityName = nota.Sedes.Ciudades.Nombre;
            enote.SellerSupplierParty.Party.PhysicalLocation.Address.CountrySubentity = nota.Sedes.Ciudades.Departamentos.Nombre;
            enote.SellerSupplierParty.Party.PhysicalLocation.Address.CountrySubentityCode = nota.Sedes.Ciudades.Departamentos.Codigo;
            enote.SellerSupplierParty.Party.PhysicalLocation.Address.AddressLine = new AddressLine { Line = nota.Sedes.Direccion };
            enote.SellerSupplierParty.Party.PhysicalLocation.Address.Country = new CountryE { IdentificationCode = nota.Sedes.Ciudades.Departamentos.Paises.Codigo, Name = nota.Sedes.Ciudades.Departamentos.Paises.Nombre };

            if (nota.Pacientes != null)
            {
                enote.AccountingCustomerParty = new AccountingCustomerParty();
                enote.AccountingCustomerParty.AdditionalAccountID = "2";
                enote.AccountingCustomerParty.Party = new Party2();
                enote.AccountingCustomerParty.Party.PartyIdentification = nota.Pacientes.TiposIdentificacion.CodigoAlterno;
                enote.AccountingCustomerParty.Party.ID = nota.Pacientes.NumeroIdentificacion.Trim();
                enote.AccountingCustomerParty.Party.Person = new Person { FirstName = nota.Pacientes.NombreCompleto, LastName = nota.Pacientes.PrimerNombre + " " + nota.Pacientes.SegundoNombre + " " + nota.Pacientes.SegundoApellido };

                enote.AccountingCustomerParty.Party.PhysicalLocation = new PhysicalLocation { Address = new Address() };

                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.ID = nota.Pacientes.Ciudades.Codigo;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.CityName = nota.Pacientes.Ciudades.Nombre;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.CountrySubentity = nota.Pacientes.Ciudades.Departamentos.Nombre;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.CountrySubentityCode = nota.Pacientes.Ciudades.Departamentos.Codigo;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.AddressLine = new AddressLine { Line = nota.Pacientes.Direccion };

                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.Country = new CountryE { IdentificationCode = nota.Pacientes.Ciudades.Departamentos.Paises.Codigo, Name = nota.Pacientes.Ciudades.Departamentos.Paises.Nombre };

                enote.AccountingCustomerParty.Party.PartyTaxScheme = new PartyTaxScheme2();
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationName = nota.Pacientes.NombreCompleto;
                enote.AccountingCustomerParty.Party.PartyTaxScheme.TaxLevelCode = "R-99-PN";
                enote.AccountingCustomerParty.Party.PartyTaxScheme.ListName = "No aplica";
                enote.AccountingCustomerParty.Party.PartyTaxScheme.TaxScheme = new TaxScheme { ID = "ZZ", Name = "No aplica" };
            }
            else
            {
                enote.AccountingCustomerParty = new AccountingCustomerParty();
                enote.AccountingCustomerParty.AdditionalAccountID = nota.Entidades.TiposPersonas.Codigo;

                enote.AccountingCustomerParty.Party = new Party2();
                enote.AccountingCustomerParty.Party.PartyIdentification = nota.Entidades.TiposIdentificacion.CodigoAlterno;
                enote.AccountingCustomerParty.Party.ID = nota.Entidades.NumeroIdentificacion.Trim();
                enote.AccountingCustomerParty.Party.schemeID = nota.Entidades.DV;
                enote.AccountingCustomerParty.Party.PartyName = new PartyName { Name = nota.Entidades.Nombre };

                enote.AccountingCustomerParty.Party.PhysicalLocation = new PhysicalLocation { Address = new Address() };

                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.ID = nota.Entidades.Ciudades.Codigo;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.CityName = nota.Entidades.Ciudades.Nombre;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.CountrySubentity = nota.Entidades.Ciudades.Departamentos.Nombre;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.CountrySubentityCode = nota.Entidades.Ciudades.Departamentos.Codigo;
                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.AddressLine = new AddressLine { Line = nota.Entidades.Direccion };

                enote.AccountingCustomerParty.Party.PhysicalLocation.Address.Country = new CountryE { IdentificationCode = nota.Entidades.Ciudades.Departamentos.Paises.Codigo, Name = nota.Entidades.Ciudades.Departamentos.Paises.Nombre };

                enote.AccountingCustomerParty.Party.PartyTaxScheme = new PartyTaxScheme2();
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationName = nota.Entidades.Nombre;
                enote.AccountingCustomerParty.Party.PartyTaxScheme.TaxLevelCode = nota.Entidades.GetResponsabilidadesFiscales();
                enote.AccountingCustomerParty.Party.PartyTaxScheme.ListName = nota.Entidades.GetRegimenFiscal();
                enote.AccountingCustomerParty.Party.PartyTaxScheme.TaxScheme = new TaxScheme { ID = taxSchemeClient.ID, Name = taxSchemeClient.Name };
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress = new Address();
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress.ID = nota.Entidades.Ciudades.Codigo;
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress.CityName = nota.Entidades.Ciudades.Nombre;
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress.CountrySubentity = nota.Entidades.Ciudades.Departamentos.Nombre;
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress.CountrySubentityCode = nota.Entidades.Ciudades.Departamentos.Codigo;
                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress.AddressLine = new AddressLine { Line = nota.Entidades.Direccion };

                enote.AccountingCustomerParty.Party.PartyTaxScheme.RegistrationAddress.Country = new CountryE { IdentificationCode = nota.Entidades.Ciudades.Departamentos.Paises.Codigo, Name = nota.Entidades.Ciudades.Departamentos.Paises.Nombre };

            }

            enote.PaymentMeans = new PaymentMeans { ID = ((nota.FormasPagos.Codigo == "1") ? "1" : "2"), PaymentDueDate = nota.Fecha.AddDays((double)nota.FormasPagos.DiasVencimiento) };

            enote.TaxTotal = new List<TaxTotal>();

            foreach (var item in InvoiceDetailsByTax)
            {
                if (item.TaxValue > 0)
                {
                    TaxTotal tax = new TaxTotal { TaxAmount = item.TaxValue };
                    tax.TaxSubtotal = new TaxSubtotal();
                    tax.TaxSubtotal.TaxableAmount = (item.TaxValue > 0) ? item.Value - item.DiscountValue : 0;
                    tax.TaxSubtotal.TaxAmount = (decimal)item.TaxValue;
                    if (item.TaxSchemeCode == "22")
                    {
                        tax.TaxSubtotal.BaseUnitMeasure = "B%";
                        tax.TaxSubtotal.UnitCode = "NAR";
                        tax.TaxSubtotal.PerUnitAmount = new PerUnitAmountType { Value = 50 };
                    }
                    tax.TaxSubtotal.TaxCategory = new TaxCategory();
                    if (item.TaxSchemeCode != "22")
                        tax.TaxSubtotal.TaxCategory.Percent = new PercentType { Value = item.TaxPercentaje };
                    tax.TaxSubtotal.TaxCategory.TaxScheme = new TaxScheme { ID = item.TaxSchemeCode, Name = item.TaxSchemeName };

                    enote.TaxTotal.Add(tax);
                }
            }
            if (enote.TaxTotal.Count == 0)
                enote.TaxTotal = null;

            enote.RequestedMonetaryTotal = new LegalMonetaryTotal();
            enote.RequestedMonetaryTotal.LineExtensionAmount = nota.ValorSubtotal - nota.ValorDescuentos;
            enote.RequestedMonetaryTotal.TaxExclusiveAmount = nota.ValorImpuestos;

            enote.RequestedMonetaryTotal.TaxExclusiveBaseAmount = nota.ValorImpuestos > 0 ? (nota.ValorSubtotal - nota.ValorDescuentos) : 0;

            enote.RequestedMonetaryTotal.TaxInclusiveAmount = (nota.ValorSubtotal - nota.ValorDescuentos + nota.ValorImpuestos);
            //enote.LegalMonetaryTotal.PrePaidAmount = new PrePaidAmountType { Value = nota.ValorCopagoCuotaModeradora };

            enote.RequestedMonetaryTotal.AllowanceLineAmount = new AllowanceLineAmountType { Value = nota.ValorDescuentos };
            enote.RequestedMonetaryTotal.PayableAmount = nota.ValorTotal;
            enote.RequestedMonetaryTotal.PayableExpectedAmount = nota.ValorTotal;
            //enote.RequestedMonetaryTotal.TextAmount = WrittenAmount.ConvertToString(nota.ValorTotal);
            enote.RequestedMonetaryTotal.TextAmount = DApp.Util.NumeroEnLetras(nota.ValorTotal);
            enote.RequestedMonetaryTotal.LineCountNumeric = new LineCountNumericType { Value = nota.NotasDetalles.Count };
            enote.DebitNoteLine = new List<DebitLine>();

            for (int i = 0; i < nota.NotasDetalles.Count; i++)
            {
                DebitLine data = new DebitLine();
                data.ID = (i + 1);
                data.StandardItemIdentification = new StandardItemIdentification { ID = nota.NotasDetalles[i].Servicios.Codigo.TrimStart().TrimEnd(), schemeID = "999" };
                data.DebitedQuantity = nota.NotasDetalles[i].Cantidad;
                data.UnitCode = "NAR";
                data.LineExtensionAmount = nota.NotasDetalles[i].SubTotalValue - nota.NotasDetalles[i].DiscountValue;
                //data.NetLineExtensionAmount = new NetLineExtensionAmountType { Value = InvoiceDetails[i].TotalValue }  ;
                data.Price = new Price { PriceAmount = nota.NotasDetalles[i].ValorServicio, BaseQuantity = nota.NotasDetalles[i].Cantidad };
                data.Item = new Item { Description = new List<string>() };
                data.Item.AddDescription(nota.NotasDetalles[i].Servicios.Nombre.Replace("Í", "I"));

                if (nota.NotasDetalles[i].PorcDescuento > 0)
                {
                    data.AllowanceCharge = new List<AllowanceCharge>();
                    AllowanceCharge discount = new AllowanceCharge();
                    discount.ID = 1;
                    discount.ChargeIndicator = false;
                    discount.AllowanceChargeReason = "Descuento general";
                    discount.MultiplierFactorNumeric = nota.NotasDetalles[i].PorcDescuento;
                    discount.BaseAmount = nota.NotasDetalles[i].SubTotalValue;
                    discount.Amount = nota.NotasDetalles[i].DiscountValue;
                    data.AllowanceCharge.Add(discount);
                }

                if (nota.NotasDetalles[i].TaxValue > 0)
                {
                    data.TaxTotal = new TaxTotal();
                    data.TaxTotal.TaxAmount = nota.NotasDetalles[i].TaxValue;
                    data.TaxTotal.TaxSubtotal = new TaxSubtotal { TaxableAmount = nota.NotasDetalles[i].TaxValue > 0 ? nota.NotasDetalles[i].SubTotalValue - nota.NotasDetalles[i].DiscountValue : 0, TaxAmount = nota.NotasDetalles[i].TaxValue };
                    data.TaxTotal.TaxSubtotal.TaxCategory = new TaxCategory();
                    data.TaxTotal.TaxSubtotal.TaxCategory.Percent = new PercentType { Value = nota.NotasDetalles[i].PorcImpuesto };
                    data.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme = new TaxScheme { ID = nota.NotasDetalles[i].Servicios.TiposImpuestos.Codigo, Name = nota.NotasDetalles[i].Servicios.TiposImpuestos.Descripcion };
                }
                enote.DebitNoteLine.Add(data);

            }

            return enote;
        }

        public async Task<string> SendDebitNoteAsync(long idNota, string urlAcepta)
        {
            string content = "";
            EDebitNote invoice = GetDebitNote(idNota);
            string xml = invoice.SerializeToXml();
                //.Replace(@"<?xml version=""1.0"" encoding=""utf-8""?>", "");
            HttpClient http = new HttpClient();
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("docid", invoice.ID));
            nvc.Add(new KeyValuePair<string, string>("comando", "emitir"));
            nvc.Add(new KeyValuePair<string, string>("parametros", ""));
            nvc.Add(new KeyValuePair<string, string>("datos", xml));

            var encodedItems = nvc.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
            var encodedContent = new StringContent(String.Join("&", encodedItems), null, "application/x-www-form-urlencoded");

            var response = await http.PostAsync(urlAcepta, encodedContent);

            //var response = await http.PostAsync(urlAcepta, new FormUrlEncodedContent(nvc));
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }
            //if (content.StartsWith("Error"))
            //    factura.ErrorReference = content;

            //Manager.GetBusinessLogic<InterfaceInvoice>().Modify(factura);
            return content;
        }
    }
}
