using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using System;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;

namespace Blazor.BusinessLogic
{
    public class EmpresasBusinessLogic : GenericBusinessLogic<Empresas>
    {
        public EmpresasBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public EmpresasBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public override Empresas FindById(Expression<Func<Empresas, bool>> predicate, bool include = false)
        {
            var data = base.FindById(predicate, true);
            if (data != null)
            {
                data.Base64Files[0] = DApp.GetImageString(data?.LogoArchivos?.Archivo);
            }
            return data;
        }

        public override Empresas Add(Empresas data)
        {
            var logicaData = new GenericBusinessLogic<Empresas>(this.UnitOfWork.Settings);
            var logicaArchivo = new GenericBusinessLogic<Archivos>(logicaData.UnitOfWork);
            logicaData.BeginTransaction();
            try
            {
                if (data.LogoArchivos != null)
                {
                    data.LogoArchivosId = ManageArchivo(data.LogoArchivos, data.LogoArchivosId, logicaArchivo);
                    data.LogoArchivos.Id = data.LogoArchivosId.GetValueOrDefault(0);
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

        public override Empresas Modify(Empresas data)
        {
            var logicaData = new GenericBusinessLogic<Empresas>(this.UnitOfWork.Settings);
            var logicaArchivo = new GenericBusinessLogic<Archivos>(logicaData.UnitOfWork);
            logicaData.BeginTransaction();
            try
            {
                if (data.LogoArchivos != null)
                {
                    data.LogoArchivosId = ManageArchivo(data.LogoArchivos, data.LogoArchivosId, logicaArchivo);
                    data.LogoArchivos.Id = data.LogoArchivosId.GetValueOrDefault(0);
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
    }
}
