using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Dominus.Backend.Security;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;

namespace Blazor.BusinessLogic
{
    public class ArchivosBusinessLogic : GenericBusinessLogic<Archivos>
    {
        public ArchivosBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public ArchivosBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public override Archivos FindById(Expression<Func<Archivos, bool>> predicate, bool include = false)
        {
            var data = base.FindById(predicate, include);
            if (data != null)
            {
                data.StringToBase64 = DApp.Util.ArrayBytesToString(data.Archivo);
            }
            return data;
        }

        public override List<Archivos> FindAll(Expression<Func<Archivos, bool>> predicate, bool include = false)
        {
            var datas = base.FindAll(predicate, include);
            datas.ForEach(x => {
                if (x != null)
                {
                    x.StringToBase64 = DApp.Util.ArrayBytesToString(x.Archivo);
                }
            });
            return datas;
        }

    }
}

