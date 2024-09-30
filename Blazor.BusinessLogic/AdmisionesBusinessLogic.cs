using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using DevExpress.Export.Xl;
using Dominus.Backend.DataBase;
using System;
using System.Linq;
using DevExpress.Spreadsheet;
using System.IO;
using System.Collections.Generic;

namespace Blazor.BusinessLogic
{
    public class AdmisionesBusinessLogic : GenericBusinessLogic<Admisiones>
    {
        public AdmisionesBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public AdmisionesBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public byte[] ExcelEntregaAdmisiones(long sedeId, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                Workbook workbook = new Workbook();
                workbook.CreateNewDocument();
                Worksheet worksheet = workbook.Worksheets.Add("Entrega Admisiones");
                List<VEntregaAdmisiones> data = new GenericBusinessLogic<VEntregaAdmisiones>(this.UnitOfWork).FindAll(x => x.SEDE_ID == sedeId && x.ADMISION_FECHA_CREACION >= fechaDesde && x.ADMISION_FECHA_CREACION <= fechaHasta, false);

                //Titulos
                int column = 0;
                worksheet.Rows[0][column].SetValue("SEDE"); column++;
                worksheet.Rows[0][column].SetValue("USUARIO ADMISIONO"); column++;
                worksheet.Rows[0][column].SetValue("NO. ADMISION"); column++;
                worksheet.Rows[0][column].SetValue("ESTADO ADMISION"); column++;
                worksheet.Rows[0][column].SetValue("ENTIDAD"); column++;
                worksheet.Rows[0][column].SetValue("CONVENIO"); column++;
                worksheet.Rows[0][column].SetValue("FILIAL"); column++;
                worksheet.Rows[0][column].SetValue("TIPO USUARIO"); column++;
                worksheet.Rows[0][column].SetValue("TIPO IDENTIFICACION"); column++;
                worksheet.Rows[0][column].SetValue("NUMERO IDENTIFICACION"); column++;
                worksheet.Rows[0][column].SetValue("NOMBRES PACIENTE"); column++;
                worksheet.Rows[0][column].SetValue("NUMERO AUTORIZACION"); column++;
                worksheet.Rows[0][column].SetValue("FECHA AUTORIZACION"); column++;
                worksheet.Rows[0][column].SetValue("USUARIO FACTURO"); column++;
                worksheet.Rows[0][column].SetValue("FACTURA"); column++;
                worksheet.Rows[0][column].SetValue("CAJA"); column++;
                worksheet.Rows[0][column].SetValue("ID CICLO CAJA"); column++;
                worksheet.Rows[0][column].SetValue("CODIGO SERVICIO"); column++;
                worksheet.Rows[0][column].SetValue("SERVICIO"); column++;
                worksheet.Rows[0][column].SetValue("CANTIDAD"); column++;
                worksheet.Rows[0][column].SetValue("VALOR UNITARIO"); column++;
                worksheet.Rows[0][column].SetValue("VALOR TOTAL"); column++;
                worksheet.Rows[0][column].SetValue("CUOTA MODERADORA"); column++;
                worksheet.Rows[0][column].SetValue("COPAGO"); column++;
                worksheet.Rows[0][column].SetValue("CUOTA RECUPERACION"); column++;
                worksheet.Rows[0][column].SetValue("PAGOS COMPARTIDOS"); column++;
                worksheet.Rows[0][column].SetValue("PAGOS PARTICULAR"); column++;
                worksheet.Rows[0][column].SetValue("% DESCUENTO"); column++;
                worksheet.Rows[0][column].SetValue("VALOR DESCUENTO"); column++;
                worksheet.Rows[0][column].SetValue("NETO ENTIDAD"); column++;
                worksheet.Rows[0][column].SetValue("VALOR RECAUDADO"); column++;

                for (int row = 0; row < data.Count; row++)
                {
                    column = 0;
                    worksheet.Rows[row + 1][column].SetValue(data[row].SEDE_NOMBRE); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].USUARIO_CREO); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].ADMISION_ID); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].ADMISION_ESTADO); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].ENTIDAD_NOMBRE); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].CONVENIO_NOMBRE); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].FILIAL_NOMBRE); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].TIPO_USUARIO); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].PACIENTE_TIPO_IDENTIFICACION); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].PACIENTE_NUMERO_IDENTIFICACION); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].PACIENTE_NOMBRES); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].NUMERO_AUTORIZACION); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].FECHA_AUTORIZACION); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].FACTURA_USUARIO_CREO); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].FACTURA_DOCUMENTO); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].CAJA_NOMBRE); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].CICLO_CAJA_ID); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].CUPS); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].SERVICIO_NOMBRE); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].SERVICIO_CANTIDAD); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].SERVICIO_VALOR); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].SERVICIOS_VALOR_TOTAL); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].CUOTA_MODERADORA); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].COPAGO); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].CUOTA_RECUPERACION); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].PAGO_COMPARTIDO); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].PAGO_PARTICULAR); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].PORC_DESCUENTO); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].VALOR_DESCUENTO); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].VALOR_ENTIDAD); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].RECAUDO_VALOR_APLICADO); column++;
                }
                worksheet.Columns.AutoFit(0, column);

                byte[] book = workbook.SaveDocument(DocumentFormat.Xlsx);
                workbook.Dispose();
                return book;
            }
            catch
            {
                throw;
            }
        }

        public void configSendEmail(long idPaciente)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            var empleado = unitOfWork.Repository<Empleados>().FindAll(x => x.AutorizaDescuento == true, true).FirstOrDefault();
            if (empleado == null)
                throw new Exception("No existe un empleado que autorice descuento en admisiones.");
            var usuario = unitOfWork.Repository<User>().FindById(x => x.Id == empleado.UserId, false);
            if (usuario == null)
                throw new Exception($"El empleado {empleado.NombreCompleto} no tiene un usuario asignado.");
            var Paciente = unitOfWork.Repository<Pacientes>().FindById(x => x.Id == idPaciente, false);

            EnvioEmailConfig envioEmailConfig = new EnvioEmailConfig();
            envioEmailConfig.Origen = "POR DEFECTO";
            envioEmailConfig.Asunto = "Autorización de descuento en admisiones";
            envioEmailConfig.Template = "EmailAutorizacionAdmision";
            envioEmailConfig.Destinatarios.Add(usuario.Email);
            envioEmailConfig.Datos = new Dictionary<string, string>
                {
                    {"UserName",$"{empleado.NombreCompleto}" },
                    {"NombrePaciente",$"{Paciente.NombreCompleto}" }
                };
            new ConfiguracionEnvioEmailBusinessLogic(this.UnitOfWork).EnviarEmail(envioEmailConfig);
        }

        public decimal ValorCopago(decimal PorcentajeCopago, decimal CopagoMaximoEvento, long CitaId)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            var cita = unitOfWork.Repository<ProgramacionCitas>().FindById(x => x.Id == CitaId, false);

            if (cita.ConveniosId == null)
                return 0;

            var convenio = unitOfWork.Repository<Convenios>().FindById(x => x.Id == cita.ConveniosId, false);
            var precioServicio = unitOfWork.Repository<PreciosServicios>().FindById(x => x.ListaPreciosId == convenio.ListaPreciosId && x.ServiciosId == cita.ServiciosId, true);

            decimal valorBase = 0;
            if (precioServicio == null)
            {
                valorBase = unitOfWork.Repository<Servicios>().FindById(x => x.Id == cita.ServiciosId, false).TarifaPlena;
            }
            else
            {
                if (precioServicio.ListaPrecios.TipoEstadosId != 54)
                {
                    valorBase = precioServicio.Precio + precioServicio.ListaPrecios.Valor;
                }
                else
                {
                    valorBase = precioServicio.Precio + (precioServicio.Precio * (precioServicio.ListaPrecios.Porcentaje / 100));
                }
            }

            var valorServicio = valorBase * (PorcentajeCopago / 100);
            if (valorServicio >= CopagoMaximoEvento)
                valorServicio = CopagoMaximoEvento;

            valorServicio = valorServicio * cita.Cantidad;

            valorServicio = Math.Round(valorServicio / 50, 0) * 50;

            return valorServicio;
        }

        public override Admisiones Add(Admisiones data)
        {
            var logicaData = new GenericBusinessLogic<Admisiones>(this.UnitOfWork.Settings);
            var logicaArchivo = new GenericBusinessLogic<Archivos>(logicaData.UnitOfWork);
            logicaData.BeginTransaction();
            try
            {
                if (data.ExoneracionArchivo != null)
                {
                    data.ExoneracionArchivoId = ManageArchivo(data.ExoneracionArchivo, data.ExoneracionArchivoId, logicaArchivo);
                    data.ExoneracionArchivo.Id = data.ExoneracionArchivoId.GetValueOrDefault(0);
                }
                data = logicaData.Add(data);

                var logicaCita = new GenericBusinessLogic<ProgramacionCitas>(logicaData.UnitOfWork);
                var cita = logicaCita.FindById(x => x.Id == data.ProgramacionCitasId, true);
                cita.EstadosId = 4;
                cita.ServiciosId = data.ProgramacionCitas.ServiciosId;
                cita.EstadosIdTipoDuracion = data.ProgramacionCitas.EstadosIdTipoDuracion;
                cita.Duracion = data.ProgramacionCitas.Duracion;
                cita.Cantidad = data.ProgramacionCitas.Cantidad;
                cita.EntidadesId = data.ProgramacionCitas.EntidadesId;
                cita.ConveniosId = data.ConveniosId;
                cita = logicaCita.Modify(cita);


                // Calculamos el valor del servicio si tiene lista de precios si no seria tarifa plena del servicio
                var valorServicio = cita.Servicios.TarifaPlena;
                if (cita.Convenios != null && cita.Convenios.ListaPreciosId != null)
                {
                    var logicaPreciosServicios = new GenericBusinessLogic<PreciosServicios>(logicaData.UnitOfWork);
                    var servicioEnLista = logicaPreciosServicios.FindById(x => x.ListaPreciosId == cita.Convenios.ListaPreciosId && x.ServiciosId == cita.ServiciosId, true);
                    if (servicioEnLista != null)
                    {
                        if (servicioEnLista.ListaPrecios.TipoEstadosId != 54)
                        {
                            valorServicio = servicioEnLista.Precio + servicioEnLista.ListaPrecios.Valor;
                        }
                        else
                        {
                            valorServicio = servicioEnLista.Precio + (servicioEnLista.Precio * (servicioEnLista.ListaPrecios.Porcentaje / 100));
                        }
                    }
                }

                var logicaAdmisionesServiciosPrestados = new GenericBusinessLogic<AdmisionesServiciosPrestados>(logicaData.UnitOfWork);
                AdmisionesServiciosPrestados admisionesServiciosPrestados = new AdmisionesServiciosPrestados();
                admisionesServiciosPrestados.Id = 0;
                admisionesServiciosPrestados.LastUpdate = DateTime.Now;
                admisionesServiciosPrestados.UpdatedBy = data.UpdatedBy;
                admisionesServiciosPrestados.CreationDate = DateTime.Now;
                admisionesServiciosPrestados.CreatedBy = data.CreatedBy;
                admisionesServiciosPrestados.AdmisionesId = data.Id;
                admisionesServiciosPrestados.ServiciosId = cita.ServiciosId;
                admisionesServiciosPrestados.ValorServicio = valorServicio;
                admisionesServiciosPrestados.AtencionesId = null;
                admisionesServiciosPrestados.Observaciones = null;
                admisionesServiciosPrestados.Recomendaciones = null;
                admisionesServiciosPrestados.Facturado = false;
                admisionesServiciosPrestados.PorcImpuesto = 0;
                admisionesServiciosPrestados.FacturasId = null;
                admisionesServiciosPrestados.Cantidad = cita.Cantidad;
                admisionesServiciosPrestados.FacturasGeneracionId = null;
                admisionesServiciosPrestados = logicaAdmisionesServiciosPrestados.Add(admisionesServiciosPrestados);

                logicaData.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                logicaData.RollbackTransaction();
                throw e;
            }
        }

        public override Admisiones Modify(Admisiones data)
        {
            var logicaData = new GenericBusinessLogic<Admisiones>(this.UnitOfWork.Settings);
            var logicaArchivo = new GenericBusinessLogic<Archivos>(logicaData.UnitOfWork);
            logicaData.BeginTransaction();
            try
            {
                if (data.ExoneracionArchivo != null)
                {
                    data.ExoneracionArchivoId = ManageArchivo(data.ExoneracionArchivo, data.ExoneracionArchivoId, logicaArchivo);
                    data.ExoneracionArchivo.Id = data.ExoneracionArchivoId.GetValueOrDefault(0);
                }
                data = logicaData.Modify(data);

                logicaData.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                logicaData.RollbackTransaction();
                throw e;
            }
        }

        public override Admisiones Remove(Admisiones data)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                data = unitOfWork.Repository<Admisiones>().Remove(data);
                EliminarArchivoDeMaestro(data.ExoneracionArchivoId, unitOfWork);
                unitOfWork.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public void AnularAtencion(long admisionId, string detalleAnulacion)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                if (admisionId <= 0 || string.IsNullOrWhiteSpace(detalleAnulacion))
                {
                    throw new Exception("El detalle para la anulación es obligatorio.");
                }

                var admision = unitOfWork.Repository<Admisiones>().FindById(x => x.Id == admisionId, true);
                admision.ProgramacionCitas.EstadosId = 3;
                unitOfWork.Repository<ProgramacionCitas>().Modify(admision.ProgramacionCitas);
                admision.DetalleAnulacion = detalleAnulacion;
                admision.EstadosId = 72;
                unitOfWork.Repository<Admisiones>().Modify(admision);
                unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw e;
            }
        }
    }

}

