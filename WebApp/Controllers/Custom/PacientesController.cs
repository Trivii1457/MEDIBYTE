using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.WebApp.Controllers
{

    public partial class PacientesController
    {
        [HttpPost]
        public LoadResult GetGenerosId(DataSourceLoadOptions loadOptions)
        {
            List<string> codigos = new List<string>() { "M", "F" };
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Generos>().Tabla(true).Where(x => codigos.Contains(x.Codigo)), loadOptions);
        }
    }
}
