using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Blazor.Infrastructure.Models;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Dominus.Backend.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.BusinessLogic
{
    public class AtencionesBusinessLogic : GenericBusinessLogic<Atenciones>
    {
        public AtencionesBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public AtencionesBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public Atenciones AddAtencion(Atenciones data)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                data.EstadosId = 10076; // Estado atentida
                data = unitOfWork.Repository<Atenciones>().Modify(data);

                var admision = unitOfWork.Repository<Admisiones>().FindById(x => x.Id == data.AdmisionesId, false);
                admision.EstadosId = 62;
                unitOfWork.Repository<Admisiones>().Modify(admision);
                
                var cita = unitOfWork.Repository<ProgramacionCitas>().FindById(x => x.Id == admision.ProgramacionCitasId, false);
                cita.EstadosId = 6;
                unitOfWork.Repository<ProgramacionCitas>().Modify(cita);

                var admisionesServiciosPrestados = unitOfWork.Repository<AdmisionesServiciosPrestados>().FindAll(x => x.AdmisionesId == admision.Id, false);
                admisionesServiciosPrestados.ForEach(x => {
                    x.AtencionesId = data.Id;
                    unitOfWork.Repository<AdmisionesServiciosPrestados>().Modify(x);
                });
                
                unitOfWork.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public Atenciones AnularAtencion(Atenciones data)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                data.EstadosId = 10077;// Estado anulada
                data = unitOfWork.Repository<Atenciones>().Modify(data);

                var admision = unitOfWork.Repository<Admisiones>().FindById(x => x.Id == data.AdmisionesId, false);
                admision.EstadosId = 10079;
                unitOfWork.Repository<Admisiones>().Modify(admision);

                var cita = unitOfWork.Repository<ProgramacionCitas>().FindById(x => x.Id == admision.ProgramacionCitasId, false);
                cita.EstadosId = 10078;
                unitOfWork.Repository<ProgramacionCitas>().Modify(cita);

                var admisionesServiciosPrestados = unitOfWork.Repository<AdmisionesServiciosPrestados>().FindAll(x => x.AdmisionesId == admision.Id, false);
                admisionesServiciosPrestados.ForEach(x => {
                    x.AtencionesId = data.Id;
                    unitOfWork.Repository<AdmisionesServiciosPrestados>().Modify(x);
                });

                unitOfWork.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw e;
            }

        }

        public SchedulerModel VerCitasProgramadas(Empleados empleados)
        {
            SchedulerModel schedulerModel = new SchedulerModel();
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            List<long> estados = new List<long> { 3, 4, 5, 6 };
            schedulerModel.Data = (unitOfWork.Repository<ProgramacionCitas>().GetTable(true).Where(x => x.EmpleadosId == empleados.Id && estados.Contains(x.EstadosId))).AsEnumerable<ProgramacionCitas>();
            schedulerModel.FechaMinima = DateTime.Now;
            schedulerModel.FechaMaxima = DateTime.Now.AddYears(1);
            return schedulerModel;
        }
    }
}
