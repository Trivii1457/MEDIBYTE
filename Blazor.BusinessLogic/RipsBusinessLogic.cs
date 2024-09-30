using Blazor.BusinessLogic.Models;
using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Blazor.BusinessLogic
{
    public class RipsBusinessLogic : GenericBusinessLogic<Rips>
    {
        private readonly string delimitador = ",";
        private string PathArchivos = "";
        private List<string> registroCT = new List<string>();

        public RipsBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public RipsBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public override Rips Add(Rips data)
        {
            data.Consecutivo = RipsConsecutivo();
            return base.Add(data);
        }

        public long RipsConsecutivo()
        {
            try
            {
                BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
                return unitOfWork.Repository<Rips>().Table.Max(x => x.Consecutivo) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public ArchivoDescargaModel GenerarArchivos(long id)
        {
            List<string> errores = new List<string>();
            ArchivoDescargaModel archivoDescargaModel = new ArchivoDescargaModel();
            GenericBusinessLogic<Rips> logicaRips = new GenericBusinessLogic<Rips>(this.UnitOfWork.Settings);
            try
            {
                if (id > 0)
                {
                    Rips rips = logicaRips.Tabla(true)
                        .Include(x => x.Empresas.TiposIdentificacion)
                        .FirstOrDefault(x => x.Id == id);

                    this.PathArchivos = Path.Combine(Path.GetTempPath(), "ArchivosRips");
                    if (Directory.Exists(this.PathArchivos))
                    {
                        Directory.Delete(this.PathArchivos, true);
                    }
                    Directory.CreateDirectory(this.PathArchivos);

                    if (rips.GenerarUS)
                    {
                        try
                        {
                            GenerarUS(rips);
                        }
                        catch (Exception e)
                        {
                            errores.Add($"Error en generar US. | {e.Message}");
                        }
                    }

                    if (rips.GenerarAF)
                    {
                        try
                        {
                            GenerarAF(rips);
                        }
                        catch (Exception e)
                        {
                            errores.Add($"Error en generar AF. | {e.Message}");
                        }
                    }

                    if (rips.GenerarAC)
                    {
                        try
                        {
                            GenerarAC(rips);
                        }
                        catch (Exception e)
                        {
                            errores.Add($"Error en generar AC. | {e.Message}");
                        }
                    }

                    if (rips.GenerarAP)
                    {
                        try
                        {
                            GenerarAP(rips);
                        }
                        catch (Exception e)
                        {
                            errores.Add($"Error en generar AP. | {e.Message}");
                        }
                    }

                    //Archivo CT
                    File.WriteAllText(Path.Combine(this.PathArchivos, $"CT{rips.Periodo.ToString("MMyyyy")}.txt"), string.Join(Environment.NewLine, registroCT));

                    if (errores.Count <= 0)
                    {
                        archivoDescargaModel.Nombre = $"RIPS_{rips.FechaRemision.ToString("yyyyMMdd")}_{rips.Consecutivo}_{rips.Periodo.ToString("MMyyyy")}_{rips.Entidades.Alias}.zip";
                        archivoDescargaModel.ContentType = "application/zip";
                        string pathZip = Path.Combine(Path.GetTempPath(), archivoDescargaModel.Nombre);
                        ZipFile.CreateFromDirectory(this.PathArchivos, pathZip);
                        byte[] result = File.ReadAllBytes(pathZip);
                        archivoDescargaModel.Archivo = result;
                        Directory.Delete(this.PathArchivos, true);
                        File.Delete(pathZip);

                        return archivoDescargaModel;
                    }
                }
                else
                {
                    errores.Add("El rips no es correcto. Revisar servicio.");
                }
            }
            catch (Exception e)
            {
                errores.Add($"Error general. | {e.Message}");
                throw;
            }

            archivoDescargaModel.Nombre = $"ERRORES_RIPS{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt";
            archivoDescargaModel.ContentType = "text/plain";
            string pathErrores = Path.Combine(Path.GetTempPath(), archivoDescargaModel.Nombre);
            File.WriteAllLines(pathErrores, errores);
            byte[] resultErrores = File.ReadAllBytes(pathErrores);
            archivoDescargaModel.Archivo = resultErrores;
            File.Delete(pathErrores);

            return archivoDescargaModel;

        }

        #region US
        private void GenerarUS(Rips rips)
        {
            List<string> registros = new List<string>();
            GenericBusinessLogic<AdmisionesServiciosPrestados> logicaAdmisionesServiciosPrestados = new GenericBusinessLogic<AdmisionesServiciosPrestados>(this.UnitOfWork.Settings);
            var result = logicaAdmisionesServiciosPrestados.Tabla(true)
                .Include(x => x.Admisiones.Pacientes)
                .Include(x => x.Admisiones.ProgramacionCitas.Entidades)
                .Include(x => x.Admisiones.Pacientes.TiposIdentificacion)
                .Include(x => x.Admisiones.Pacientes.TiposRegimenes)
                .Include(x => x.Admisiones.Pacientes.Generos)
                .Include(x => x.Admisiones.Pacientes.Ciudades)
                .Include(x => x.Admisiones.Pacientes.Ciudades.Departamentos)
                .Where(x => x.Facturas.EntidadesId == rips.EntidadesId && x.Facturas.Estadosid != 1087)
                .Where(x => x.Facturas.Fecha.Date.Year == rips.Periodo.Date.Year && x.Facturas.Fecha.Date.Month == rips.Periodo.Date.Month)
                .OrderBy(x=>x.Admisiones.Pacientes.TiposIdentificacion.Codigo).ThenBy(x=>x.Admisiones.Pacientes.NumeroIdentificacion).ToList();

            if (result == null || result.Count <= 0)
                throw new Exception($"No existen facturas en el periodo {rips.Periodo.ToString("MMyyyy")} para la entidad {rips.Entidades.Alias}");

            foreach (var item in result)
            {
                Dictionary<int, string> data = new Dictionary<int, string>();

                data.Add(1, item.Admisiones.Pacientes.TiposIdentificacion.Codigo); // Tipo de identificación del usuario
                data.Add(2, item.Admisiones.Pacientes.NumeroIdentificacion); // Número de identificación del usuario del sistema
                data.Add(3, item.Admisiones.ProgramacionCitas.Entidades?.CodigoReps); // Código entidad administradora
                data.Add(4, item.Admisiones.Pacientes.TiposRegimenes?.CodigoRips); // Tipo de usuario
                data.Add(5, item.Admisiones.Pacientes.PrimerApellido); // Primer apellido del usuario
                data.Add(6, item.Admisiones.Pacientes.SegundoApellido); // Segundo apellido del usuario
                data.Add(7, item.Admisiones.Pacientes.PrimerNombre); // Primer nombre del usuario
                data.Add(8, item.Admisiones.Pacientes.SegundoNombre); // Segundo nombredel usuario

                if (item.Atenciones == null)
                    throw new Exception($"La admision No. {item.Admisiones.Id} no tiene una atencion aun.");

                var diferencia = item.Atenciones.FechaAtencion - item.Admisiones.Pacientes.FechaNacimiento;
                int years = (int)(diferencia.TotalDays / 365.25);
                int months = (int)(((diferencia.TotalDays / 365.25) - years) * 12);

                if (diferencia.TotalDays <= 29)
                {
                    data.Add(9, Convert.ToInt16(diferencia.TotalDays).ToString()); // Edad
                    data.Add(10, "3"); // Unidad de medida de la edad
                }
                else if (years > 0)
                {
                    data.Add(9, years.ToString()); // Edad
                    data.Add(10, "1"); // Unidad de medida de la edad
                }
                else
                {
                    data.Add(9, months.ToString()); // Edad
                    data.Add(10, "2"); // Unidad de medida de la edad
                }
                data.Add(11, item.Admisiones.Pacientes.Generos.Codigo); // Sexo
                data.Add(12, item.Admisiones.Pacientes.Ciudades.Departamentos.Codigo); // Código del departamento de residencia habitual

                if (item.Admisiones.Pacientes.Ciudades.Codigo.Length >= 5)
                    data.Add(13, item.Admisiones.Pacientes.Ciudades.Codigo.Substring(2, 3)); // Código del municipio de residencia habitual
                else
                    data.Add(13, item.Admisiones.Pacientes.Ciudades.Codigo);
                data.Add(14, "U"); // Zona de residencia habitual

                registros.Add(String.Join(delimitador, data.OrderBy(x => x.Key).Select(x => x.Value?.ToUpper())));
            }
            registroCT.Add($"{rips.Empresas.CodigoReps}{delimitador}" +
                $"{rips.FechaRemision.ToString("dd/MM/yyyy")}{delimitador}" +
                $"US{rips.Periodo.ToString("MMyyyy")}{delimitador}" +
                $"{registros.Count}"
            );
            File.WriteAllText(Path.Combine(this.PathArchivos, $"US{rips.Periodo.ToString("MMyyyy")}.txt"), string.Join(Environment.NewLine, registros));
        }
        #endregion

        #region AF
        private void GenerarAF(Rips rips)
        {
            List<string> registros = new List<string>();
            GenericBusinessLogic<Facturas> logicaFacturas = new GenericBusinessLogic<Facturas>(this.UnitOfWork.Settings);
            var result = logicaFacturas.Tabla(true)
                .Where(x => x.EntidadesId == rips.EntidadesId && x.Estadosid != 1087)
                .Where(x => x.Fecha.Date.Year == rips.Periodo.Date.Year && x.Fecha.Date.Month == rips.Periodo.Date.Month).ToList();

            foreach (var item in result)
            {
                Dictionary<int, string> data = new Dictionary<int, string>();

                data.Add(1, rips.Empresas.CodigoReps); // Código delprestador de servicios de salud
                data.Add(2, rips.Empresas.RazonSocial); // Razón social o apellidos y nombre del prestador de servicios de salud
                data.Add(3, rips.Empresas.TiposIdentificacion.Codigo); // Tipo de identificación del prestador de servicios de salud
                data.Add(4, rips.Empresas.NumeroIdentificacion); // Número de identificación del prestador
                data.Add(5, $"{item.Documentos.Prefijo}{item.NroConsecutivo}"); // Número de la factura
                data.Add(6, item.Fecha.ToString("dd/MM/yyyy")); // Fecha de expedición de la factura
                data.Add(7, item.FehaInicial.ToString("dd/MM/yyyy")); // Fecha de inicio
                data.Add(8, item.FechaFinal.ToString("dd/MM/yyyy")); // Fecha final
                data.Add(9, item.Entidades.CodigoReps); // Código entidad administradora
                data.Add(10, item.Entidades.Alias); // Alias entidad administradora
                data.Add(11, item.Convenio?.NroContrato); //Número del contrato
                data.Add(12, item.Convenio?.PlanBeneficio); // Plan de beneficios
                data.Add(13, item.Convenio?.NroPoliza); // Número de la póliza
                data.Add(14, Convert.ToInt64(item.ValorCopagoCuotaModeradora).ToString()); // Valor total del pago compartido (copago)
                data.Add(15, "0"); // Valor de la comisión
                data.Add(16, Convert.ToInt64(item.ValorDescuentos).ToString()); // Valor total de descuentos
                data.Add(17, Convert.ToInt64(item.ValorSubtotal).ToString()); // Valor neto a pagar por la entidad contratante (Valor bruto de la factura)

                registros.Add(String.Join(delimitador, data.OrderBy(x => x.Key).Select(x => x.Value?.ToUpper())));
            }

            registroCT.Add($"{rips.Empresas.CodigoReps}{delimitador}" +
                $"{rips.FechaRemision.ToString("dd/MM/yyyy")}{delimitador}" +
                $"AF{rips.Periodo.ToString("MMyyyy")}{delimitador}" +
                $"{registros.Count}"
            );
            File.WriteAllText(Path.Combine(this.PathArchivos, $"AF{rips.Periodo.ToString("MMyyyy")}.txt"), string.Join(Environment.NewLine, registros));

        }
        #endregion

        #region AC
        private void GenerarAC(Rips rips)
        {
            List<string> registros = new List<string>();
            GenericBusinessLogic<AdmisionesServiciosPrestados> logicaAdmisionesServiciosPrestados = new GenericBusinessLogic<AdmisionesServiciosPrestados>(this.UnitOfWork.Settings);
            var result = logicaAdmisionesServiciosPrestados.Tabla(true)
                .Include(x => x.Facturas.Documentos)
                .Include(x => x.Admisiones.Pacientes)
                .Include(x => x.Servicios.Cups)
                .Include(x => x.Admisiones.Pacientes.TiposIdentificacion)
                .Include(x => x.Admisiones.ProgramacionCitas)
                .Include(x => x.Admisiones.ProgramacionCitas.Servicios)
                .Include(x => x.Admisiones.ProgramacionCitas.Servicios.Cups)
                .Include(x => x.Admisiones.Pacientes.TiposIdentificacion)
                .Include(x => x.Admisiones.Diagnosticos)
                .Include(x => x.Atenciones.FinalidadConsulta)
                .Include(x => x.Atenciones.CausasExternas)
                .Where(x => x.Facturas.EntidadesId == rips.EntidadesId && x.Facturas.Estadosid != 1087)
                .Where(x => x.Servicios.TiposServiciosId == 1)
                .Where(x => x.Facturas.Fecha.Date.Year == rips.Periodo.Date.Year && x.Facturas.Fecha.Date.Month == rips.Periodo.Date.Month)
                .OrderBy(x => x.Facturas.Documentos.Prefijo).ThenBy(x => x.Facturas.NroConsecutivo).ToList();

            foreach (var item in result)
            {
                Dictionary<int, string> data = new Dictionary<int, string>();

                if (item.Atenciones == null)
                    throw new Exception($"La admision No. {item.Admisiones.Id} no tiene una atencion aun.");

                data.Add(1, $"{item.Facturas.Documentos.Prefijo}{item.Facturas.NroConsecutivo}"); // Número de la factura
                data.Add(2, rips.Empresas.CodigoReps); // Código del prestador de servicios de salud
                data.Add(3, item.Admisiones.Pacientes.TiposIdentificacion.Codigo); // Tipo de identificación del usuario
                data.Add(4, item.Admisiones.Pacientes.NumeroIdentificacion); // Número de identificación del usuario en el sistema
                data.Add(5, item.Atenciones.FechaAtencion.ToString("dd/MM/yyy")); // Fecha de la consulta
                data.Add(6, item.Admisiones.NroAutorizacion); // Número de autorización
                data.Add(7, item.Servicios.Codigo); // Código de la consulta
                data.Add(8, item.Atenciones.FinalidadConsulta?.CodigoRips); // Finalidad de la consulta
                data.Add(9, item.Atenciones.CausasExternas?.CodigoRips); // Causa externa
                data.Add(10, item.Admisiones.Diagnosticos.Codigo); // Código del diagnóstico principal
                data.Add(11, ""); // Código del diagnóstico relacionado No. 1
                data.Add(12, ""); // Código del diagnóstico relacionado No. 2
                data.Add(13, ""); // Código del diagnóstico relacionado No. 3
                data.Add(14, "1"); // Tipo de diagnóstico principal

                var valorFinal = item.Admisiones.ValorPagarParticular;
                data.Add(15, Convert.ToInt64(valorFinal).ToString()); // Valor de la consulta

                data.Add(16, Convert.ToInt64(item.Admisiones.ValorCopago).ToString()); // Valor de la cuota moderadora

                valorFinal = item.Admisiones.ValorPagarParticular - item.Admisiones.ValorCopago;
                data.Add(17, Convert.ToInt64(valorFinal).ToString()); // Valor neto a pagar

                registros.Add(String.Join(delimitador, data.OrderBy(x => x.Key).Select(x => x.Value?.ToUpper())));
            }

            registroCT.Add($"{rips.Empresas.CodigoReps}{delimitador}" +
                $"{rips.FechaRemision.ToString("dd/MM/yyyy")}{delimitador}" +
                $"AC{rips.Periodo.ToString("MMyyyy")}{delimitador}" +
                $"{registros.Count}"
            );
            File.WriteAllText(Path.Combine(this.PathArchivos, $"AC{rips.Periodo.ToString("MMyyyy")}.txt"), string.Join(Environment.NewLine, registros));
        }
        #endregion

        #region AP
        private void GenerarAP(Rips rips)
        {
            List<string> registros = new List<string>();
            GenericBusinessLogic<AdmisionesServiciosPrestados> logicaAdmisionesServiciosPrestados = new GenericBusinessLogic<AdmisionesServiciosPrestados>(this.UnitOfWork.Settings);
            var result = logicaAdmisionesServiciosPrestados.Tabla(true)
                .Include(x => x.Facturas.Documentos)
                .Include(x => x.Admisiones.Pacientes)
                .Include(x => x.Admisiones.Diagnosticos)
                .Include(x => x.Servicios.Cups)
                .Include(x => x.Admisiones.Pacientes.TiposIdentificacion)
                .Include(x => x.Admisiones.ProgramacionCitas)
                .Include(x => x.Admisiones.ProgramacionCitas.Servicios)
                .Include(x => x.Admisiones.ProgramacionCitas.Servicios.Cups)
                .Include(x => x.Admisiones.Pacientes.TiposIdentificacion)
                .Include(x => x.Atenciones.AmbitoRealizacionProcedimiento)
                .Include(x => x.Atenciones.FinalidadProcedimiento)
                .Where(x => x.Facturas.EntidadesId == rips.EntidadesId && x.Facturas.Estadosid != 1087)
                .Where(x=>x.Servicios.TiposServiciosId == 2)
                .Where(x => x.Facturas.Fecha.Date.Year == rips.Periodo.Date.Year && x.Facturas.Fecha.Date.Month == rips.Periodo.Date.Month)
                .OrderBy(x => x.Facturas.Documentos.Prefijo).ThenBy(x => x.Facturas.NroConsecutivo).ToList();

            foreach (var item in result)
            {
                Dictionary<int, string> data = new Dictionary<int, string>();

                if (item.Atenciones == null)
                    throw new Exception($"La admision No. {item.Admisiones.Id} no tiene una atencion aun.");

                data.Add(1, $"{item.Facturas.Documentos.Prefijo}{item.Facturas.NroConsecutivo}"); // Número de la factura
                data.Add(2, rips.Empresas.CodigoReps); // Código del prestador de servicios de salud
                data.Add(3, item.Admisiones.Pacientes.TiposIdentificacion.Codigo); // Tipo de identificación del usuario
                data.Add(4, item.Admisiones.Pacientes.NumeroIdentificacion); // Número de identificación del usuario en el sistema
                data.Add(5, item.Atenciones.FechaAtencion.ToString("dd/MM/yyy")); // Fecha del procedimiento
                data.Add(6, item.Admisiones.NroAutorizacion); // Número de autorización
                data.Add(7, item.Servicios.Codigo); // Código de la consulta

                //Se coloca 1 ambulatorio porque toro dijo.
                data.Add(8, item.Atenciones.AmbitoRealizacionProcedimiento == null ? "1" : item.Atenciones.AmbitoRealizacionProcedimiento.Id.ToString()); // Ámbito de realización del procedimiento
                data.Add(9, item.Atenciones.FinalidadProcedimiento == null ? "1" : item.Atenciones.FinalidadProcedimiento.Id.ToString()); // Finalidad del procedimiento

                data.Add(10, ""); // Personal que atiende

                if (item.Admisiones.ProgramacionCitas.Servicios.EsQuirurgico) // Código del diagnóstico principal si el servicio es quirurgico
                    data.Add(11, item.Admisiones?.Diagnosticos.Codigo);
                else
                    data.Add(11, "");

                data.Add(12, ""); // Diagnóstico relacionado
                data.Add(13, ""); // Complicación
                data.Add(14, ""); // Forma de realización del acto quirúrgico
                data.Add(15, Convert.ToInt64(item.ValorServicio * item.Cantidad).ToString()); // Valor del procedimiento


                registros.Add(String.Join(delimitador, data.OrderBy(x => x.Key).Select(x => x.Value?.ToUpper())));
            }

            registroCT.Add($"{rips.Empresas.CodigoReps}{delimitador}" +
                $"{rips.FechaRemision.ToString("dd/MM/yyyy")}{delimitador}" +
                $"AP{rips.Periodo.ToString("MMyyyy")}{delimitador}" +
                $"{registros.Count}"
            );
            File.WriteAllText(Path.Combine(this.PathArchivos, $"AP{rips.Periodo.ToString("MMyyyy")}.txt"), string.Join(Environment.NewLine, registros));
        }
        #endregion 
    }
}
