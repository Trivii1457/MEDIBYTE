using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Dominus.Backend.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace Blazor.BusinessLogic
{
    public class ProgramacionAgendaBusinessLogic : GenericBusinessLogic<ProgramacionAgenda>
    {
        public ProgramacionAgendaBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public ProgramacionAgendaBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        private void VerificarCruzeConsultorio(ProgramacionAgenda data, ServiciosConsultorios serviciosConsultorios, BlazorUnitWork unitOfWork)
        {
            var query = (from pa in unitOfWork.Repository<ProgramacionAgenda>().Table
                         join pd in unitOfWork.Repository<ProgramacionDisponible>().Table on pa.Id equals pd.ProgramacionAgendaId
                         where ((data.FechaInicio >= pa.FechaInicio && data.FechaInicio <= pa.FechaFinal)
                         || (data.FechaFinal >= pa.FechaInicio && data.FechaFinal <= pa.FechaFinal)
                         || (data.FechaFinal >= pa.FechaInicio && data.FechaFinal <= pa.FechaFinal)
                         || (pa.FechaInicio >= data.FechaInicio && pa.FechaInicio <= data.FechaFinal)
                         || (pa.FechaFinal >= data.FechaInicio && pa.FechaFinal <= data.FechaFinal))
                         && pd.ServiciosId == serviciosConsultorios.ServiciosId && pd.ConsultoriosId == serviciosConsultorios.ConsultoriosId
                         select pa
                         );

            var diferenciaDias = (data.FechaFinal - data.FechaInicio).TotalDays;
            if (diferenciaDias > 2)
            {
                if (data.Lunes) query = query.Where(x => x.Lunes);
                if (data.Martes) query = query.Where(x => x.Martes);
                if (data.Miercoles) query = query.Where(x => x.Miercoles);
                if (data.Jueves) query = query.Where(x => x.Jueves);
                if (data.Viernes) query = query.Where(x => x.Viernes);
                if (data.Sabado) query = query.Where(x => x.Sabado);
                if (data.Domingo) query = query.Where(x => x.Domingo);
            }
            else
            {
                DayOfWeek dayOfWeekInicio = data.FechaInicio.DayOfWeek;
                DayOfWeek dayOfWeekFinal = data.FechaFinal.DayOfWeek;

                Func<ProgramacionAgenda, bool> predicado = x => false;

                if (dayOfWeekInicio == DayOfWeek.Monday) predicado += x => false || x.Lunes;
                if (dayOfWeekInicio == DayOfWeek.Tuesday) predicado += x => false || x.Martes;
                if (dayOfWeekInicio == DayOfWeek.Wednesday) predicado += x => false || x.Miercoles;
                if (dayOfWeekInicio == DayOfWeek.Thursday) predicado += x => false || x.Jueves;
                if (dayOfWeekInicio == DayOfWeek.Friday) predicado += x => false || x.Viernes;
                if (dayOfWeekInicio == DayOfWeek.Saturday) predicado += x => false || x.Sabado;
                if (dayOfWeekInicio == DayOfWeek.Sunday) predicado += x => false || x.Domingo;

                if (dayOfWeekFinal == DayOfWeek.Monday) predicado += x => false || x.Lunes;
                if (dayOfWeekFinal == DayOfWeek.Tuesday) predicado += x => false || x.Martes;
                if (dayOfWeekFinal == DayOfWeek.Wednesday) predicado += x => false || x.Miercoles;
                if (dayOfWeekFinal == DayOfWeek.Thursday) predicado += x => false || x.Jueves;
                if (dayOfWeekFinal == DayOfWeek.Friday) predicado += x => false || x.Viernes;
                if (dayOfWeekFinal == DayOfWeek.Saturday) predicado += x => false || x.Sabado;
                if (dayOfWeekFinal == DayOfWeek.Sunday) predicado += x => false || x.Domingo;

                query = query.Where(predicado).AsQueryable();
            }

            List<ProgramacionAgenda> result = query.ToList();
            if (result.Count > 0)
            {
                var horaInicio = result.Min(x => x.FechaInicio.TimeOfDay);
                var horafinal = result.Max(x => x.FechaFinal.TimeOfDay);
                bool huboCruceHoras = false;

                if ((data.FechaInicio.TimeOfDay >= horaInicio && data.FechaInicio.TimeOfDay <= horafinal)
                    || (data.FechaFinal.TimeOfDay >= horaInicio && data.FechaFinal.TimeOfDay <= horafinal)
                    || (data.FechaFinal.TimeOfDay >= horaInicio && data.FechaFinal.TimeOfDay <= horafinal)
                    || (horaInicio >= data.FechaInicio.TimeOfDay && horaInicio <= data.FechaFinal.TimeOfDay)
                    || (horafinal >= data.FechaInicio.TimeOfDay && horafinal <= data.FechaFinal.TimeOfDay))
                {
                    huboCruceHoras = true;
                }

                if (huboCruceHoras)
                    throw new Exception($"El servicio {serviciosConsultorios.Servicios.Nombre} y el consultorio {serviciosConsultorios.Consultorios.Nombre} se cruza en fechas con la agenda de consecutivo No. {result[0].Consecutivo}");
            }
        }

        private void VerificarCruzeEmpleado(ProgramacionAgenda data, ServiciosEmpleados serviciosEmpleados, BlazorUnitWork unitOfWork)
        {
            var query = (from pa in unitOfWork.Repository<ProgramacionAgenda>().Table
                         join pd in unitOfWork.Repository<ProgramacionDisponible>().Table on pa.Id equals pd.ProgramacionAgendaId
                         where ((data.FechaInicio >= pa.FechaInicio && data.FechaInicio <= pa.FechaFinal)
                         || (data.FechaFinal >= pa.FechaInicio && data.FechaFinal <= pa.FechaFinal)
                         || (data.FechaFinal >= pa.FechaInicio && data.FechaFinal <= pa.FechaFinal)
                         || (pa.FechaInicio >= data.FechaInicio && pa.FechaInicio <= data.FechaFinal)
                         || (pa.FechaInicio >= data.FechaInicio && pa.FechaInicio <= data.FechaFinal)
                         || (pa.FechaFinal >= data.FechaInicio && pa.FechaFinal <= data.FechaFinal))
                         && pd.ServiciosId == serviciosEmpleados.ServiciosId && pd.EmpleadosId == serviciosEmpleados.EmpleadosId
                         select pa
                         );

            var diferenciaDias = (data.FechaFinal - data.FechaInicio).TotalDays;
            if (diferenciaDias > 2)
            {
                if (data.Lunes) query = query.Where(x => x.Lunes == true);
                if (data.Martes) query = query.Where(x => x.Martes == true);
                if (data.Miercoles) query = query.Where(x => x.Miercoles == true);
                if (data.Jueves) query = query.Where(x => x.Jueves == true);
                if (data.Viernes) query = query.Where(x => x.Viernes == true);
                if (data.Sabado) query = query.Where(x => x.Sabado == true);
                if (data.Domingo) query = query.Where(x => x.Domingo == true);
            }
            else
            {
                DayOfWeek dayOfWeekInicio = data.FechaInicio.DayOfWeek;
                DayOfWeek dayOfWeekFinal = data.FechaFinal.DayOfWeek;

                Func<ProgramacionAgenda, bool> predicado = x => false;

                if (dayOfWeekInicio == DayOfWeek.Monday) predicado += x => false || x.Lunes;
                if (dayOfWeekInicio == DayOfWeek.Tuesday) predicado += x => false || x.Martes;
                if (dayOfWeekInicio == DayOfWeek.Wednesday) predicado += x => false || x.Miercoles;
                if (dayOfWeekInicio == DayOfWeek.Thursday) predicado += x => false || x.Jueves;
                if (dayOfWeekInicio == DayOfWeek.Friday) predicado += x => false || x.Viernes;
                if (dayOfWeekInicio == DayOfWeek.Saturday) predicado += x => false || x.Sabado;
                if (dayOfWeekInicio == DayOfWeek.Sunday) predicado += x => false || x.Domingo;

                if (dayOfWeekFinal == DayOfWeek.Monday) predicado += x => false || x.Lunes;
                if (dayOfWeekFinal == DayOfWeek.Tuesday) predicado += x => false || x.Martes;
                if (dayOfWeekFinal == DayOfWeek.Wednesday) predicado += x => false || x.Miercoles;
                if (dayOfWeekFinal == DayOfWeek.Thursday) predicado += x => false || x.Jueves;
                if (dayOfWeekFinal == DayOfWeek.Friday) predicado += x => false || x.Viernes;
                if (dayOfWeekFinal == DayOfWeek.Saturday) predicado += x => false || x.Sabado;
                if (dayOfWeekFinal == DayOfWeek.Sunday) predicado += x => false || x.Domingo;

                query = query.Where(predicado).AsQueryable();
            }

            var result = query.ToList();
            if (result.Count > 0)
            {
                var horaInicio = result.Min(x => x.FechaInicio.TimeOfDay);
                var horafinal = result.Max(x => x.FechaFinal.TimeOfDay);
                bool huboCruceHoras = false;

                if ((data.FechaInicio.TimeOfDay >= horaInicio && data.FechaInicio.TimeOfDay <= horafinal)
                    || (data.FechaFinal.TimeOfDay >= horaInicio && data.FechaFinal.TimeOfDay <= horafinal)
                    || (data.FechaFinal.TimeOfDay >= horaInicio && data.FechaFinal.TimeOfDay <= horafinal)
                    || (horaInicio >= data.FechaInicio.TimeOfDay && horaInicio <= data.FechaFinal.TimeOfDay)
                    || (horafinal >= data.FechaInicio.TimeOfDay && horafinal <= data.FechaFinal.TimeOfDay))
                {
                    huboCruceHoras = true;
                }
                if (huboCruceHoras)
                    throw new Exception($"El servicio {serviciosEmpleados.Servicios.Nombre} y el empleado {serviciosEmpleados.Empleados.NombreCompleto} se cruza en fechas con la agenda de consecutivo No. {result[0].Consecutivo}");
            }
        }

        public override ProgramacionAgenda Add(ProgramacionAgenda data)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                try
                {
                    data.Consecutivo = unitOfWork.Repository<ProgramacionAgenda>().Table.Max(x => x.Consecutivo) + 1;
                }
                catch
                {
                    data.Consecutivo = 1;
                }

                data = unitOfWork.Repository<ProgramacionAgenda>().Add(data);

                List<ProgramacionDisponible> result = new List<ProgramacionDisponible>();

                if (data.ServiciosConsultorios != null && data.ServiciosConsultorios.Count > 0)
                    foreach (ServiciosConsultorios item in data.ServiciosConsultorios)
                    {
                        VerificarCruzeConsultorio(data, item, unitOfWork);
                        result.Add(new ProgramacionDisponible
                        {
                            Id = 0,
                            ProgramacionAgendaId = data.Id,
                            ServiciosId = item.ServiciosId,
                            ConsultoriosId = item.ConsultoriosId,
                            EmpleadosId = null,

                            UpdatedBy = data.UpdatedBy,
                            LastUpdate = data.LastUpdate,
                            CreatedBy = data.CreatedBy,
                            CreationDate = data.CreationDate
                        });
                    }

                if (data.ServiciosEmpleados != null && data.ServiciosEmpleados.Count > 0)
                    foreach (ServiciosEmpleados item in data.ServiciosEmpleados)
                    {
                        VerificarCruzeEmpleado(data, item, unitOfWork);
                        result.Add(new ProgramacionDisponible
                        {
                            Id = 0,
                            ProgramacionAgendaId = data.Id,
                            ServiciosId = item.ServiciosId,
                            ConsultoriosId = null,
                            EmpleadosId = item.EmpleadosId,

                            UpdatedBy = data.UpdatedBy,
                            LastUpdate = data.LastUpdate,
                            CreatedBy = data.CreatedBy,
                            CreationDate = data.CreationDate
                        });
                    }

                unitOfWork.Repository<ProgramacionDisponible>().AddRange(result);

                unitOfWork.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                data.Id = 0;
                unitOfWork.RollbackTransaction();
                throw new Exception("Error en adicionar nueva programacion. | " + e.Message);
            }
        }

        public override ProgramacionAgenda Remove(ProgramacionAgenda data)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                var programacionesDisponibles = unitOfWork.Repository<ProgramacionDisponible>().Table.Where(x => x.ProgramacionAgendaId == data.Id).ToList();
                unitOfWork.Repository<ProgramacionDisponible>().RemoveRange(programacionesDisponibles);
                data = unitOfWork.Repository<ProgramacionAgenda>().Remove(data);
                unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw new Exception("Error en eliminar nueva programacion. | " + e.Message);
            }

            return data;
        }

        public Dictionary<string, Object> GetServiciosPorConsultorios(List<long> ListConsultoriosId, long id)
        {
            Dictionary<string, Object> result = new Dictionary<string, object>();
            var unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            if (ListConsultoriosId.Count > 0)
            {
                var serviciosConsultorios = (from sc in unitOfWork.Repository<ServiciosConsultorios>().FindAll(x => ListConsultoriosId.Any(j => j == x.ConsultoriosId), true)
                                             from s in unitOfWork.Repository<Servicios>().Table
                                             where sc.ServiciosId == s.Id
                                             && !s.RequiereProfesional
                                             select sc).ToList();

                result.Add("serviciosConsultorios", serviciosConsultorios);
                if (id != 0)
                {
                    var keyServiciosConsultorios = (from pd in unitOfWork.Repository<ProgramacionDisponible>().Table.Where(x => x.ProgramacionAgendaId == id)
                                                    from sc in unitOfWork.Repository<ServiciosConsultorios>().Table
                                                    where pd.ConsultoriosId == sc.ConsultoriosId && pd.ServiciosId == sc.ServiciosId
                                                    select sc.Id).ToList();

                    result.Add("keyServiciosConsultorios", keyServiciosConsultorios);
                }
            }
            return result;
        }

        public Dictionary<string, Object> GetServiciosPorEmpleados(List<long> listEmpleadosId, long id)
        {
            Dictionary<string, Object> result = new Dictionary<string, object>();
            var unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            if (listEmpleadosId.Count > 0)
            {
                var serviciosEmpleados = unitOfWork.Repository<ServiciosEmpleados>().FindAll(x => listEmpleadosId.Any(j => j == x.EmpleadosId), true);

                result.Add("serviciosEmpleados", serviciosEmpleados);
                if (id != 0)
                {
                    var keyServiciosEmpleados = (from pd in unitOfWork.Repository<ProgramacionDisponible>().Table.Where(x => x.ProgramacionAgendaId == id)
                                                 from sc in unitOfWork.Repository<ServiciosEmpleados>().Table
                                                 where pd.EmpleadosId == sc.EmpleadosId && pd.ServiciosId == sc.ServiciosId
                                                 select sc.Id).ToList();
                    result.Add("keyServiciosEmpleados", keyServiciosEmpleados);
                }
            }
            return result;
        }

    }
}
