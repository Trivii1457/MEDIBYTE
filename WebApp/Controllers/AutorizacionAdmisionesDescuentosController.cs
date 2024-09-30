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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public  class AutorizacionAdmisionesDescuentosController : BaseAppController

    {
        public AutorizacionAdmisionesDescuentosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
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

            var result = Manager().GetBusinessLogic<Admisiones>().Tabla(true).Where(x => x.Estados.Nombre == "EN AUTORIZACION")
                .Include(x => x.ProgramacionCitas.Servicios);
            return DataSourceLoader.Load(result, loadOptions);
        }

        public IActionResult List()
        {
            AutorizacionAdmisionesDescuentosModel autorizacionAdmisionesDescuentosModel = new AutorizacionAdmisionesDescuentosModel();

            autorizacionAdmisionesDescuentosModel.AutorizaDes = false;
            var empleado = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId == this.ActualUsuarioId(), false);
            if (empleado == null)
            {
                autorizacionAdmisionesDescuentosModel.Error = string.Format(DApp.GetResource("BLL.AutorizacionAdmisionesDescuentos.ErrorEmpleadoUsuario"), User.Identity.Name);
            }
            else
            {
                if (!empleado.AutorizaDescuento)
                    autorizacionAdmisionesDescuentosModel.Error = string.Format(DApp.GetResource("BLL.AutorizacionAdmisionesDescuentos.ErrorPermisoAutorizacion"), empleado.NombreCompleto);
                else
                {
                    autorizacionAdmisionesDescuentosModel.ListAdmisiones = Manager().GetBusinessLogic<Admisiones>().Tabla(true).Where(x => x.Estados.Nombre == "EN AUTORIZACION")
                            .Include(x => x.ProgramacionCitas.Servicios).ToList();
                    autorizacionAdmisionesDescuentosModel.AutorizaDes = true;
                }
                    
            }
            return View("List", autorizacionAdmisionesDescuentosModel);
        }
        public IActionResult ListPartial()
        {
            AutorizacionAdmisionesDescuentosModel autorizacionAdmisionesDescuentosModel = new AutorizacionAdmisionesDescuentosModel();

            autorizacionAdmisionesDescuentosModel.AutorizaDes = false;
            var empleado = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId == this.ActualUsuarioId(), false);
            if (empleado == null)
            {
                autorizacionAdmisionesDescuentosModel.Error = string.Format(DApp.GetResource("BLL.AutorizacionAdmisionesDescuentos.ErrorEmpleadoUsuario"), User.Identity.Name);
            }
            else
            {
                if (!empleado.AutorizaDescuento)
                    autorizacionAdmisionesDescuentosModel.Error = string.Format(DApp.GetResource("BLL.AutorizacionAdmisionesDescuentos.ErrorPermisoAutorizacion"), empleado.NombreCompleto);
                else
                {
                    autorizacionAdmisionesDescuentosModel.ListAdmisiones = Manager().GetBusinessLogic<Admisiones>().Tabla(true).Where(x => x.Estados.Nombre == "EN AUTORIZACION")
                            .Include(x => x.ProgramacionCitas.Servicios).ToList();
                    autorizacionAdmisionesDescuentosModel.AutorizaDes = true;
                }
            }
            return PartialView("List", autorizacionAdmisionesDescuentosModel);
        }

        [HttpPost]
        public JsonResult AutorizarDescuento(string admisiones)
        {
            Dictionary<String, object> Result = new Dictionary<string, object>();
            try
            {
                var models = JsonConvert.DeserializeObject<List<Admisiones>>(admisiones);
                foreach (Admisiones model in models)
                {
                    model.EstadosId = Manager().GetBusinessLogic<Estados>().FindById(x => x.Tipo == "ADMISION" && x.Nombre == "ADMITIDA", false).Id;
                    model.FechaAprobacion = DateTime.Now;
                    model.UserAproboId = this.ActualUsuarioId();
                    var valorTarifa = model.ProgramacionCitas.Servicios.TarifaPlena;
                    var valorDescuento = (model.PorcDescAutorizado / 100) * (model.ProgramacionCitas.Servicios.TarifaPlena);
                    model.ValorPagarParticular = valorTarifa - valorDescuento;
                    Manager().GetBusinessLogic<Admisiones>().Modify(model);
                }
                Result.Add("Result", "Autorización realizada correctamente.");
                var resultData = Manager().GetBusinessLogic<Admisiones>().Tabla(true).Where(x => x.Estados.Nombre == "EN AUTORIZACION")
                            .Include(x => x.ProgramacionCitas.Servicios).ToList();
                Result.Add("resultData", resultData);
                return Json(Result);
            }
            catch (Exception e)
            {
                Result.Add("Error", e.Message);
                return Json(Result);
            }

            
        }

    }

}


