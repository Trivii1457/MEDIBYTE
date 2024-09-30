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
    public class RadicacionCuentasBusinessLogic : GenericBusinessLogic<RadicacionCuentas>
    {
        public RadicacionCuentasBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public RadicacionCuentasBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public override RadicacionCuentas Add(RadicacionCuentas data)
        {
            var logicaData = new GenericBusinessLogic<RadicacionCuentas>(this.UnitOfWork.Settings);
            var logicaArchivo = new GenericBusinessLogic<Archivos>(logicaData.UnitOfWork);
            logicaData.BeginTransaction();
            try
            {
                if (data.RadicacionArchivos != null)
                {
                    data.RadicacionArchivosId = ManageArchivo(data.RadicacionArchivos, data.RadicacionArchivosId, logicaArchivo);
                    data.RadicacionArchivos.Id = data.RadicacionArchivosId.GetValueOrDefault(0);
                }

                data = logicaData.Add(data);
                logicaData.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                logicaData.RollbackTransaction();
                throw e;
            }
        }

        public override RadicacionCuentas Modify(RadicacionCuentas data)
        {
            var logicaData = new GenericBusinessLogic<RadicacionCuentas>(this.UnitOfWork.Settings);
            var logicaArchivo = new GenericBusinessLogic<Archivos>(logicaData.UnitOfWork);
            logicaData.BeginTransaction();
            try
            {
                if (data.RadicacionArchivos != null)
                {
                    data.RadicacionArchivosId = ManageArchivo(data.RadicacionArchivos, data.RadicacionArchivosId, logicaArchivo);
                    data.RadicacionArchivos.Id = data.RadicacionArchivosId.GetValueOrDefault(0);
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

        public override RadicacionCuentas Remove(RadicacionCuentas data)
        {
            var unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                data = unitOfWork.Repository<RadicacionCuentas>().Remove(data);
                EliminarArchivoDeMaestro(data.RadicacionArchivosId, unitOfWork);
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
