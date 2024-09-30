using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Dominus.Backend.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.BusinessLogic
{
    public class EntregaResultadosBusinessLogic : GenericBusinessLogic<EntregaResultados>
    {
        public EntregaResultadosBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public EntregaResultadosBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public override EntregaResultados Add(EntregaResultados data)
        {
            var logicaData = new GenericBusinessLogic<EntregaResultados>(this.UnitOfWork.Settings);
            var logicaArchivo = new GenericBusinessLogic<Archivos>(logicaData.UnitOfWork);
            var logicaEntregaResultadosDetalles = new GenericBusinessLogic<EntregaResultadosDetalles>(logicaData.UnitOfWork);
            var logicaAtencionesResultado = new GenericBusinessLogic<AtencionesResultado>(logicaData.UnitOfWork);
            logicaData.BeginTransaction();
            try
            {
                if (data.ContanciaArchivos != null)
                {
                    data.ContanciaArchivosId = ManageArchivo(data.ContanciaArchivos, data.ContanciaArchivosId, logicaArchivo);
                    data.ContanciaArchivos.Id = data.ContanciaArchivosId.GetValueOrDefault(0);
                }

                data = logicaData.Add(data);

                foreach (var item in data.ListAtencionesResultadoId)
                {
                    EntregaResultadosDetalles entregaResultadosDetalles = new EntregaResultadosDetalles();
                    entregaResultadosDetalles.Id = 0;
                    entregaResultadosDetalles.CreatedBy =data.CreatedBy;
                    entregaResultadosDetalles.UpdatedBy =data.UpdatedBy;
                    entregaResultadosDetalles.LastUpdate =data.LastUpdate;
                    entregaResultadosDetalles.CreationDate =data.CreationDate;

                    entregaResultadosDetalles.EntregaResultadosId = data.Id;
                    entregaResultadosDetalles.AtencionesResultadoId = item;
                    logicaEntregaResultadosDetalles.Add(entregaResultadosDetalles);

                    AtencionesResultado atencionesResultado = logicaAtencionesResultado.FindById(x=>x.Id == item, false);
                    atencionesResultado.Entregado = true;
                    logicaAtencionesResultado.Modify(atencionesResultado);

                }

                
                logicaData.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                logicaData.RollbackTransaction();
                throw e;
            }
        }

        public override EntregaResultados Modify(EntregaResultados data)
        {
            var logicaData = new GenericBusinessLogic<EntregaResultados>(this.UnitOfWork.Settings);
            var logicaArchivo = new GenericBusinessLogic<Archivos>(logicaData.UnitOfWork);
            logicaData.BeginTransaction();
            try
            {
                if (data.ContanciaArchivos != null)
                {
                    data.ContanciaArchivosId = ManageArchivo(data.ContanciaArchivos, data.ContanciaArchivosId, logicaArchivo);
                    data.ContanciaArchivos.Id = data.ContanciaArchivosId.GetValueOrDefault(0);
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

        public override EntregaResultados Remove(EntregaResultados data)
        {
            var unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                var detalles = unitOfWork.Repository<EntregaResultadosDetalles>().FindAll(x => x.EntregaResultadosId == data.Id, false);
                unitOfWork.Repository<EntregaResultadosDetalles>().RemoveRange(detalles);

                data = unitOfWork.Repository<EntregaResultados>().Remove(data);
                EliminarArchivoDeMaestro(data.ContanciaArchivosId, unitOfWork);

                unitOfWork.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw e;
            }
        }
    }
}
