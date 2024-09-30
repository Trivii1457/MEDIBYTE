using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.BusinessLogic
{
    public class FacturasGeneracionBusinessLogic : GenericBusinessLogic<Facturas>
    {
        public FacturasGeneracionBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public FacturasGeneracionBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public FacturasGeneracion GenerarFactura(List<ServiciosFacturar> models, string usuario)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                FacturasGeneracion factGen = new FacturasGeneracion();
                List<Facturas> facturas = new List<Facturas>();
                if (models != null && models.Count > 0)
                {
                    factGen = unitOfWork.Repository<FacturasGeneracion>().FindById(x => x.Id == models[0].FacturasGeneracionId, false);
                    var encFacturas = models.Select(x => new ServiciosFacturar { EmpresasId = x.EmpresasId, SedesId = x.SedesId, EntidadesId = x.EntidadesId, ConvenioId = x.ConvenioId }).Distinct().ToList();
                    if (encFacturas != null && encFacturas.Count > 0)
                    {
                        foreach (var itemaFact in encFacturas)
                        {
                            Facturas fact = new Facturas();
                            fact.EmpresasId = itemaFact.EmpresasId;
                            fact.MediosPagoId = factGen.MediosPagoId;
                            fact.TiposRegimenesId = factGen.TiposRegimenesId;
                            fact.FilialesId = factGen.FilialesId;
                            fact.SedesId = itemaFact.SedesId;
                            fact.EntidadesId = itemaFact.EntidadesId;
                            fact.ConvenioId = itemaFact.ConvenioId;
                            fact.OrdenCompra = factGen.OrdenCompra;
                            fact.ReferenciaFactura = factGen.ReferenciaFactura;
                            fact.Fecha = DateTime.Now;
                            fact.DocumentosId = factGen.DocumentosId;
                            fact.FormasPagosId = unitOfWork.Repository<Convenios>().FindById(x => x.Id == itemaFact.ConvenioId, false).FormasPagosId;

                            long consecutivo = 0;
                            var documento = unitOfWork.Repository<Documentos>().FindById(x => x.Id == factGen.DocumentosId, false);
                            try
                            {
                                consecutivo = new GenericBusinessLogic<Documentos>(this.UnitOfWork.Settings).GetSecuence($"{documento.Prefijo}");
                            }
                            catch (Exception)
                            {
                                consecutivo = 1;
                            }

                            if (documento.ResolucionDian != 0)
                            {
                                if (consecutivo < documento.ConsecutivoDesde || consecutivo > documento.ConsecutivoHasta)
                                {
                                    throw new Exception($"El consecutivo supero la resolucion de la DIAN del documento {documento.Prefijo}.");
                                }
                                if (DateTime.Now < documento.FechaDesde || DateTime.Now > documento.FechaHasta)
                                {
                                    throw new Exception($"La fecha supero la resolucion de la DIAN del documento {documento.Prefijo}.");
                                }
                            }

                            fact.NroConsecutivo = consecutivo;
                            fact.ValorSubtotal = 0;
                            fact.ValorDescuentos = 0;
                            fact.ValorImpuestos = 0;
                            fact.ValorTotal = 0;
                            fact.Saldo = 0;
                            fact.LastUpdate = DateTime.Now;
                            fact.UpdatedBy = usuario;
                            fact.CreationDate = DateTime.Now;
                            fact.CreatedBy = usuario;
                            foreach (var item in models)
                            {
                                if (item.EmpresasId == itemaFact.EmpresasId && item.SedesId == itemaFact.SedesId && item.EntidadesId == itemaFact.EntidadesId && item.ConvenioId == itemaFact.ConvenioId)
                                    fact.ServiciosAfacturar.Add(item);
                            }
                            facturas.Add(fact);
                        }
                    }
                }
                if (facturas != null && facturas.Count > 0)
                {
                    foreach (var factura in facturas)
                    {
                        //Esto se debe cambiar
                        List<long> admisiones = factura.ServiciosAfacturar.Select(x => x.AdmisionesId).Distinct().ToList();
                        if (admisiones != null && admisiones.Count > 0)
                            factura.ValorCopagoCuotaModeradora = unitOfWork.Repository<Admisiones>().FindAll(x => admisiones.Contains(x.Id), false).Sum(y => y.ValorCopago);
                        else
                            factura.ValorCopagoCuotaModeradora = 0;
                        factura.ValorSubtotal = factura.ServiciosAfacturar.Sum(x => x.SubTotal);
                        factura.ValorDescuentos = factura.ServiciosAfacturar.Sum(x => x.ValorDescuento);
                        factura.ValorImpuestos = factura.ServiciosAfacturar.Sum(x => x.ValorImpuesto);
                        factura.ValorTotal = ((factura.ValorSubtotal - factura.ValorDescuentos) + factura.ValorImpuestos) - factura.ValorCopagoCuotaModeradora;
                        factura.Saldo = factura.ValorTotal;
                        factura.FehaInicial = factGen.FechaInicio;
                        factura.FechaFinal = factGen.FechaFinal;
                        factura.OrdenCompra = factGen.OrdenCompra;
                        factura.ReferenciaFactura = factGen.ReferenciaFactura;
                        factura.DocumentosId = factGen.DocumentosId;
                        factura.Observaciones = factGen.Observaciones;
                        factura.MontoEscrito = DApp.Util.NumeroEnLetras(factura.ValorTotal);
                        factura.Estadosid = unitOfWork.Repository<Estados>().FindById(x => x.Tipo == "FACTURA" && x.Nombre == "GENERADA", false).Id;

                        if (factura.ValorTotal < 0)
                        {
                            throw new Exception("Valor total a pagar es negativo. Por favor verificar saldos.");
                        }

                        var newFact = unitOfWork.Repository<Facturas>().Add(factura);
                        List<long> idsServ = factura.ServiciosAfacturar.Select(x => x.Id).ToList();
                        factura.ServicioFacturados = unitOfWork.Repository<AdmisionesServiciosPrestados>().FindAll(x => idsServ.Contains(x.Id), false);
                        if (factura.ServicioFacturados != null && factura.ServicioFacturados.Count > 0)
                        {
                            for (int i = 0; i < factura.ServicioFacturados.Count; i++)
                            {

                                FacturasDetalles facturasDetalles = new FacturasDetalles();
                                facturasDetalles.Id = 0;
                                facturasDetalles.LastUpdate = DateTime.Now;
                                facturasDetalles.UpdatedBy = usuario;
                                facturasDetalles.CreationDate = DateTime.Now;
                                facturasDetalles.CreatedBy = usuario;
                                facturasDetalles.FacturasId = newFact.Id;
                                facturasDetalles.NroLinea = i + 1;
                                facturasDetalles.AdmisionesServiciosPrestadosId = factura.ServicioFacturados[i].Id;
                                facturasDetalles.ServiciosId = factura.ServicioFacturados[i].ServiciosId;
                                facturasDetalles.Cantidad = factura.ServicioFacturados[i].Cantidad;
                                facturasDetalles.PorcDescuento = 0;
                                facturasDetalles.PorcImpuesto = factura.ServicioFacturados[i].PorcImpuesto;
                                facturasDetalles.ValorServicio = factura.ServicioFacturados[i].ValorServicio;
                                facturasDetalles.SubTotal = facturasDetalles.Cantidad * facturasDetalles.ValorServicio;
                                facturasDetalles.ValorTotal = facturasDetalles.SubTotal - (factura.ServicioFacturados[i].ValorServicio * (facturasDetalles.PorcImpuesto / 100));
                                unitOfWork.Repository<FacturasDetalles>().Add(facturasDetalles);

                                factura.ServicioFacturados[i].FacturasGeneracionId = factGen.Id;
                                factura.ServicioFacturados[i].FacturasId = newFact.Id;
                                factura.ServicioFacturados[i].Facturado = true;
                                unitOfWork.Repository<AdmisionesServiciosPrestados>().Modify(factura.ServicioFacturados[i]);
                            }
                        }
                    }
                }
                unitOfWork.CommitTransaction();
                return factGen;
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw (e);
            }
        }

    }
}