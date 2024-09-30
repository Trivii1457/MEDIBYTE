using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Backend.Application;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public class AtencionesLecturaController : BaseAppController

    {
        public AtencionesLecturaController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        public IActionResult List()
        {
            return View("List");
        }

        public IActionResult ListPartial()
        {
            return PartialView("List");
        }

        [HttpPost]
        public LoadResult GetAtencionesLectura(DataSourceLoadOptions loadOptions)
        {
            var result = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().Tabla(true)
                .Include(x=> x.Admisiones.Pacientes)
                .Where(x => !x.LecturaRealizada && x.Servicios.RequiereLecturaResultados);

            return DataSourceLoader.Load(result, loadOptions);
        }

    }

}


