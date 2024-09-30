using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Backend.Application;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public  class AdicionOrdenesController : BaseAppController

    {
        public AdicionOrdenesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            var empleado = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId == this.ActualUsuarioId(), false);
            if (empleado == null)
            {
                return DataSourceLoader.Load(new List<Empleados>(), loadOptions);
            }

            var result = Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(true)
                .Where(x => x.EstadosId == 19 && x.CreatedBy == User.Identity.Name);
            return DataSourceLoader.Load(result, loadOptions);
        }

        public IActionResult List()
        {
            var empleado = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId == this.ActualUsuarioId(), false);
            if (empleado == null)
            {
                ModelState.AddModelError("Error", string.Format(DApp.GetResource("AdicionOrdenes.ErrorEmpleadoUsuario"), User.Identity.Name));
            }

            return View("List");
        }
        public IActionResult ListPartial()
        {
            var empleado = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId == this.ActualUsuarioId(), false);
            if (empleado == null)
            {
                ModelState.AddModelError("Error", string.Format(DApp.GetResource("AdicionOrdenes.ErrorEmpleadoUsuario"), User.Identity.Name));
            }

            return PartialView("List");
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private HistoriasClinicasModel EditModel(long Id)
        {
            HistoriasClinicasModel model = new HistoriasClinicasModel();
            model.Entity = Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(true).FirstOrDefault(x => x.Id == Id);
            model.Entity.IsNew = false;
            model.EsMismoUsuario = model.Entity.CreatedBy == User.Identity.Name;
            return model;
        }
    }
}


