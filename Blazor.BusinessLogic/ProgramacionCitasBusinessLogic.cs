using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Blazor.Infrastructure.Models;
using DevExpress.Spreadsheet;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Dominus.Backend.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.BusinessLogic
{
    public class ProgramacionCitasBusinessLogic : GenericBusinessLogic<ProgramacionCitas>
    {
        public ProgramacionCitasBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public ProgramacionCitasBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public byte[] DescargarXLSX0256(long sedeId, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                Workbook workbook = new Workbook();
                workbook.CreateNewDocument();
                Worksheet worksheet = workbook.Worksheets.Add("0256");
                List<ProgramacionCitas> data = new GenericBusinessLogic<ProgramacionCitas>(this.UnitOfWork).Tabla()
                    .Include(x => x.Pacientes)
                    .Include(x => x.Pacientes.Generos)
                    .Include(x => x.Pacientes.TiposIdentificacion)
                    .Include(x => x.Sedes)
                    .Where(x => x.EstadosId == 6 && x.FechaInicio.Date >= fechaDesde && x.FechaInicio.Date <= fechaHasta && x.SedesId == sedeId)
                    .OrderBy(x => x.FechaInicio).ToList();

                //Titulos
                int column = 0;
                worksheet.Rows[0][column].SetValue("T. DOCUMENTO"); column++;
                worksheet.Rows[0][column].SetValue("NO. DE DOCUMENTO"); column++;
                worksheet.Rows[0][column].SetValue("FECHA DE NACIMIENTO"); column++;
                worksheet.Rows[0][column].SetValue("SEXO"); column++;
                worksheet.Rows[0][column].SetValue("PRIMER NOMBRE"); column++;
                worksheet.Rows[0][column].SetValue("SEGUNDO NOMBRE"); column++;
                worksheet.Rows[0][column].SetValue("PRIMER APELLIDO"); column++;
                worksheet.Rows[0][column].SetValue("SEGUNDO APELLIDO"); column++;
                worksheet.Rows[0][column].SetValue("FECHA DE LA SOLICITUD DE LA CITA"); column++;
                worksheet.Rows[0][column].SetValue("FECHA DE LA CITA"); column++;
                worksheet.Rows[0][column].SetValue("SEDE"); column++;

                for (int row = 0; row < data.Count; row++)
                {
                    column = 0;
                    worksheet.Rows[row + 1][column].SetValue(data[row].Pacientes.TiposIdentificacion.Codigo); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].Pacientes.NumeroIdentificacion); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].Pacientes.FechaNacimiento.Date); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].Pacientes.Generos.Codigo); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].Pacientes.PrimerNombre); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].Pacientes.SegundoNombre); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].Pacientes.PrimerApellido); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].Pacientes.SegundoApellido); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].CreationDate.Date); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].FechaInicio.Date); column++;
                    worksheet.Rows[row + 1][column].SetValue(data[row].Sedes.Nombre); column++;

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

        public bool VerificarAgendaDisponiblePorServicio(long servicioId)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            int result = (from programacionDisponible in unitOfWork.Repository<ProgramacionDisponible>().Table.Where(x => x.ServiciosId == servicioId)
                          join programacionAgenda in unitOfWork.Repository<ProgramacionAgenda>().Table
                             on programacionDisponible.ProgramacionAgendaId equals programacionAgenda.Id
                          where (programacionAgenda.FechaInicio.Date >= DateTime.Now.Date
                          || (DateTime.Now.Date >= programacionAgenda.FechaInicio.Date && DateTime.Now.Date <= programacionAgenda.FechaFinal.Date))
                          select programacionAgenda.Id
            ).Count();

                return (result > 0);
        }

        public SchedulerModel ObtenerSchedulerAgendaDisponible(long servicioId, long consultorioId, long? empleadoId, long estadosIdTipoDuracion, long duracion, DateTime? fechaScheduler)
        {
            SchedulerModel schedulerModel = new SchedulerModel();

            if (servicioId == 0 || consultorioId == 0)
            {
                schedulerModel.Habilitado = false;
                return schedulerModel;
            }

            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            List<ProgramacionAgenda> result = new List<ProgramacionAgenda>(); // Este dato es para sacar los parametros desde la programacion de la agenda
            var servicio = unitOfWork.Repository<Servicios>().FindById(x => x.Id == servicioId, true);

            List<long> estados = new List<long> { 3, 4, 5, 6 };
            if (servicio.RequiereProfesional && empleadoId != null)
            {
                result = (from programacionDisponible in unitOfWork.Repository<ProgramacionDisponible>().GetTable(true).Where(x => x.ServiciosId == servicioId && x.EmpleadosId == empleadoId)
                          join programacionAgenda in unitOfWork.Repository<ProgramacionAgenda>().Table
                             on programacionDisponible.ProgramacionAgendaId equals programacionAgenda.Id
                          where (programacionAgenda.FechaInicio.Date >= DateTime.Now.Date
                          || (DateTime.Now.Date >= programacionAgenda.FechaInicio.Date && DateTime.Now.Date <= programacionAgenda.FechaFinal.Date))
                          select programacionAgenda).ToList();
                schedulerModel.Data = (unitOfWork.Repository<ProgramacionCitas>().GetTable(true).Where(x => x.EmpleadosId == empleadoId && estados.Contains(x.EstadosId))).AsEnumerable<ProgramacionCitas>();
            }
            else
            {
                result = (from programacionDisponible in unitOfWork.Repository<ProgramacionDisponible>().GetTable(true).Where(x => x.ServiciosId == servicioId && x.ConsultoriosId == consultorioId)
                          join programacionAgenda in unitOfWork.Repository<ProgramacionAgenda>().Table
                             on programacionDisponible.ProgramacionAgendaId equals programacionAgenda.Id
                          where (programacionAgenda.FechaInicio.Date >= DateTime.Now.Date
                          || (DateTime.Now.Date >= programacionAgenda.FechaInicio.Date && DateTime.Now.Date <= programacionAgenda.FechaFinal.Date))
                          select programacionAgenda).ToList();
                schedulerModel.Data = (unitOfWork.Repository<ProgramacionCitas>().GetTable(true).Where(x => x.ConsultoriosId == consultorioId && estados.Contains(x.EstadosId))).AsEnumerable<ProgramacionCitas>();
            }


            if (result != null && result.Count > 0)
            {
                var fechaInicio = result.Min(x => x.FechaInicio);
                var fechaHoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                schedulerModel.FechaInicial = (fechaInicio < fechaHoy ? fechaHoy : fechaInicio);
                schedulerModel.FechaMinima = (fechaInicio < fechaHoy ? fechaHoy : fechaInicio);
                schedulerModel.FechaMaxima = result.Max(x => x.FechaFinal);

                int horaMinima = result.Min(x => x.FechaInicio.Hour) < result.Min(x => x.FechaFinal.Hour) ? result.Min(x => x.FechaInicio.Hour) : result.Min(x => x.FechaFinal.Hour);
                int horaMaxima = result.Max(x => x.FechaInicio.Hour) > result.Max(x => x.FechaFinal.Hour) ? result.Max(x => x.FechaInicio.Hour) : result.Max(x => x.FechaFinal.Hour);
                int minMaxima = result.Max(x => x.FechaInicio.Minute) > result.Max(x => x.FechaFinal.Minute) ? result.Max(x => x.FechaInicio.Minute) : result.Max(x => x.FechaFinal.Minute);

                schedulerModel.HoraMinima = horaMinima;
                schedulerModel.HoraMaxima = horaMaxima + (double)minMaxima / 100;

                long minutos = (estadosIdTipoDuracion == 10080 ? (duracion * 60) : duracion);
                schedulerModel.IntervaloCelda = Convert.ToInt32(minutos);
            }
            else
            {
                schedulerModel.Habilitado = false;
            }

            if (fechaScheduler != null)
            {
                schedulerModel.FechaInicial = fechaScheduler.GetValueOrDefault();
            }

            return schedulerModel;
        }

        public Dictionary<string, object> ConsultarDisponibilidad(long servicioId, long consultorioId, long? empleadoId, DateTime fechaInicio, long duracion, long tipoDuracion)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            List<ProgramacionAgenda> result = new List<ProgramacionAgenda>();
            var servicio = unitOfWork.Repository<Servicios>().FindById(x => x.Id == servicioId, true);
            ProgramacionCitas citaCruce = null;
            List<long> estados = new List<long> { 3, 4, 5, 6 };

            long minutos = (tipoDuracion == 10080 ? (duracion * 60) : duracion);
            DateTime fechaFinal = fechaInicio.AddMinutes(minutos);

            if (servicio.RequiereProfesional && empleadoId != null)
            {
                result = (from programacionDisponible in unitOfWork.Repository<ProgramacionDisponible>().GetTable(true).Where(x => x.EmpleadosId == empleadoId)
                          join programacionAgenda in unitOfWork.Repository<ProgramacionAgenda>().Table
                             on programacionDisponible.ProgramacionAgendaId equals programacionAgenda.Id
                          where fechaInicio >= programacionAgenda.FechaInicio && fechaInicio <= programacionAgenda.FechaFinal
                          select programacionAgenda).ToList();
                citaCruce = unitOfWork.Repository<ProgramacionCitas>().GetTable(true)
                    .Where(x=> x.EmpleadosId == empleadoId && estados.Contains(x.EstadosId)
                    && ((x.FechaInicio >= fechaInicio && x.FechaInicio < fechaFinal)
                        || (x.FechaFinal > fechaInicio && x.FechaFinal < fechaFinal)
                        || (fechaInicio >= x.FechaInicio && fechaInicio < x.FechaFinal)
                        || (fechaFinal > x.FechaInicio && fechaFinal < x.FechaFinal)
                    )).FirstOrDefault();
            }
            else
            {
                result = (from programacionDisponible in unitOfWork.Repository<ProgramacionDisponible>().GetTable(true).Where(x => x.ConsultoriosId == consultorioId)
                          join programacionAgenda in unitOfWork.Repository<ProgramacionAgenda>().Table
                             on programacionDisponible.ProgramacionAgendaId equals programacionAgenda.Id
                          where fechaInicio >= programacionAgenda.FechaInicio && fechaInicio <= programacionAgenda.FechaFinal
                          select programacionAgenda).ToList();
                citaCruce = unitOfWork.Repository<ProgramacionCitas>().GetTable(true)
                    .Where(x => x.ConsultoriosId == consultorioId && estados.Contains(x.EstadosId)
                    && ((x.FechaInicio >= fechaInicio && x.FechaInicio < fechaFinal)
                        || (x.FechaFinal > fechaInicio && x.FechaFinal < fechaFinal)
                        || (fechaInicio >= x.FechaInicio && fechaInicio < x.FechaFinal)
                        || (fechaFinal > x.FechaInicio && fechaFinal < x.FechaFinal)
                    )).FirstOrDefault();
            }

            Dictionary<string, object> data = new Dictionary<string, object>();
            if (citaCruce != null)
            {
                data.Add("Disponible", false);
                data.Add("CitaCruce", citaCruce);
            }
            else
            {
                var disponible = EstaDisponible(result, fechaInicio);
                data.Add("Disponible", disponible);
                if (result != null && result.Count > 0 && disponible)
                {
                    var agenda = result.FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(agenda.Observaciones))
                        data.Add("ObservacionesAgenda", agenda.Observaciones);
                }
            }
            data.Add("DiaSemanaFecha", GetDiaSemanaEsp(fechaInicio));
            data.Add("Festivo", BuscarFestivo(fechaInicio));
            data.Add("FechaInicio", fechaInicio);
            data.Add("FechaFinal", fechaFinal);

            return data;
        }

        private Festivos BuscarFestivo(DateTime fechainicio)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);

            var soloFecha = fechainicio.Date;
            var festivo = unitOfWork.Repository<Festivos>().GetTable(false).FirstOrDefault(x => x.Dia == soloFecha);
            return festivo;
        }

        private string GetDiaSemanaEsp(DateTime fechaInicio)
        {
            DayOfWeek diaSemanaFecha = fechaInicio.DayOfWeek;

            if (diaSemanaFecha == DayOfWeek.Monday) return "Lunes";
            if (diaSemanaFecha == DayOfWeek.Tuesday) return "Martes";
            if (diaSemanaFecha == DayOfWeek.Wednesday) return "Miercoles";
            if (diaSemanaFecha == DayOfWeek.Thursday) return "Jueves";
            if (diaSemanaFecha == DayOfWeek.Friday) return "Viernes";
            if (diaSemanaFecha == DayOfWeek.Saturday) return "Sabado";
            if (diaSemanaFecha == DayOfWeek.Sunday) return "Domingo";

            return "";
        }

        private bool EstaDisponible(List<ProgramacionAgenda> result, DateTime fechaInicio)
        {
            if (fechaInicio < DateTime.Now)
                return false;

            DayOfWeek diaSemanaFecha = fechaInicio.DayOfWeek;

            foreach (var programacionAgenda in result)
            {
                if (programacionAgenda.Lunes || programacionAgenda.Martes || programacionAgenda.Miercoles
                    || programacionAgenda.Jueves || programacionAgenda.Viernes || programacionAgenda.Sabado || programacionAgenda.Domingo)
                {
                    if (programacionAgenda.Lunes && diaSemanaFecha == DayOfWeek.Monday) return true;
                    if (programacionAgenda.Martes && diaSemanaFecha == DayOfWeek.Tuesday) return true;
                    if (programacionAgenda.Miercoles && diaSemanaFecha == DayOfWeek.Wednesday) return true;
                    if (programacionAgenda.Jueves && diaSemanaFecha == DayOfWeek.Thursday) return true;
                    if (programacionAgenda.Viernes && diaSemanaFecha == DayOfWeek.Friday) return true;
                    if (programacionAgenda.Sabado && diaSemanaFecha == DayOfWeek.Saturday) return true;
                    if (programacionAgenda.Domingo && diaSemanaFecha == DayOfWeek.Sunday) return true;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public IQueryable<Convenios> GetConveniosByEntidad(long Id, long ServicioId)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);

            var convenios = unitOfWork.Repository<Convenios>().GetTable(false).Where(x => x.EntidadesId == Id);
            var conveniosServicios = unitOfWork.Repository<ConveniosServicios>().GetTable(false).Where(x => x.ServiciosId == ServicioId);

            var result = from convenio in convenios
                         join conveniServi in conveniosServicios on
                            convenio.Id equals conveniServi.ConveniosId
                         select convenio;
            return result;

        }

        public IQueryable<Consultorios> GetConsultoriosPorServicio(long servicioId, long sedesId)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);

            var serviciosconsultorios = unitOfWork.Repository<ServiciosConsultorios>().GetTable(false).Where(x => x.ServiciosId == servicioId);
            var consultorios = unitOfWork.Repository<Consultorios>().GetTable(true);

            var result = from consultorio in consultorios
                         join servicioConsultorio in serviciosconsultorios on
                         consultorio.Id equals servicioConsultorio.ConsultoriosId
                         where consultorio.SedesId == sedesId
                         select consultorio;
            return result;
        }

        public IQueryable<Empleados> GetEmpleadosPorServicio(long servicioId)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);

            var servicio = unitOfWork.Repository<Servicios>().FindById(x => x.Id == servicioId, false);
            if (servicio != null && servicio.RequiereProfesional)
            {
                var empleadosServicios = unitOfWork.Repository<ServiciosEmpleados>().GetTable(false).Where(x => x.ServiciosId == servicioId);
                var empleados = unitOfWork.Repository<Empleados>().GetTable(true);

                var result = from empleado in empleados
                             join empleadoServicio in empleadosServicios on
                             empleado.Id equals empleadoServicio.EmpleadosId
                             select empleado;
                return result;
            }
            else
            {
                var result = from empleado in unitOfWork.Repository<Empleados>().GetTable(false)
                             where empleado.Id == 0
                             select empleado;
                return result;
            }
        }

        public IQueryable<Entidades> GetEntidadesByPaciente(long Id)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);

            var pacientes = unitOfWork.Repository<PacientesEntidades>().GetTable(false).Where(x => x.PacientesId == Id);
            var entidades = unitOfWork.Repository<Entidades>().GetTable(true);

            var result = from entidad in entidades
                         join paciente in pacientes on
                         entidad.Id equals paciente.EntidadesId
                         select entidad;
            return result;

        }

        public int GetSiguienteConsecutivo()
        {
            try
            {
                BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
                return unitOfWork.Repository<ProgramacionCitas>().Table.Max(x => x.Consecutivo) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public override ProgramacionCitas Add(ProgramacionCitas data)
        {
            data.Consecutivo = this.GetSiguienteConsecutivo();
            data.EstadosId = 3;
            return base.Add(data);
        }

        public decimal ObtenerValorTarifaConvenio(long convenioId, long servicioId)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            var result = (from convenio in unitOfWork.Repository<Convenios>().Table
                          join listaPrecio in unitOfWork.Repository<ListaPrecios>().Table on convenio.ListaPreciosId equals listaPrecio.Id
                          join precioServicio in unitOfWork.Repository<PreciosServicios>().Table on listaPrecio.Id equals precioServicio.ListaPreciosId
                          where convenio.Id == convenioId && precioServicio.ServiciosId == servicioId
                          select new { listaPrecio = listaPrecio, precioServicio = precioServicio }).FirstOrDefault();
            decimal tarifaConvenio = 0;
            if (result != null)
            {
                if (result.listaPrecio.TipoEstadosId != 54)
                {
                    tarifaConvenio = result.precioServicio.Precio + result.listaPrecio.Valor;
                }
                else
                {
                    tarifaConvenio = result.precioServicio.Precio + (result.precioServicio.Precio * (result.listaPrecio.Porcentaje / 100));
                }
            }

            return tarifaConvenio;
        }

        public Dictionary<string, object> ConsultarDisponibilidadCitaAdicional(DateTime fechaInicio, long consultorioId)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            List<long> estados = new List<long> { 3, 4, 5, 6 };
            var totalCitas = unitOfWork.Repository<ProgramacionCitas>().Table
                .Count(x => x.FechaInicio == fechaInicio && estados.Contains(x.EstadosId) && x.ConsultoriosId == consultorioId);
            if (totalCitas >= 2)
            {
                result.Add("Disponible", false);
                result.Add("Error", "Ya existe una cita adicional programada en este horario.");
            }
            else
            {
                result.Add("Disponible", true);
            }
            return result;
        }

    }
}
