using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class PacientesEntidadesController
    {

        [HttpPost]
        public LoadResult GetById(DataSourceLoadOptions loadOptions, long PacientesId)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<PacientesEntidades>().Tabla(true).Where(x => x.PacientesId == PacientesId), loadOptions);
        }


    }
}
