using DevExtreme.AspNet.Data;
using WidgetGallery;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Frontend.Controllers;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Newtonsoft.Json;
using Blazor.BusinessLogic;
using System.Collections.Generic;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class ConfiguracionEnvioEmailController : BaseAppController
    {

        //private const string Prefix = "ConfiguracionEnvioEmail"; 

        public ConfiguracionEnvioEmailController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ConfiguracionEnvioEmail>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetLog(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<EnviaEmailLog>().Tabla(true), loadOptions);
        }

        public IActionResult List()
        {
            return View("List");
        }

        public IActionResult ListPartial()
        {
            return PartialView("List");
        }

        [HttpGet]
        public IActionResult New()
        {
            return PartialView("Edit", NewModel());
        }

        private ConfiguracionEnvioEmailModel NewModel() 
        { 
            ConfiguracionEnvioEmailModel model = new ConfiguracionEnvioEmailModel();
            var entity = Manager().GetBusinessLogic<ConfiguracionEnvioEmail>().FindAll(null, false).FirstOrDefault();
            if (entity != null)
            {
                model.Entity = entity;
                model.Entity.IsNew = false;
            }
            else
            {
                model.Entity.IsNew = true;
                model.Entity.Origen = "DEFAULT";
            }
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private ConfiguracionEnvioEmailModel EditModel(long Id) 
        { 
            ConfiguracionEnvioEmailModel model = new ConfiguracionEnvioEmailModel();
            model.Entity = Manager().GetBusinessLogic<ConfiguracionEnvioEmail>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(ConfiguracionEnvioEmailModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private ConfiguracionEnvioEmailModel EditModel(ConfiguracionEnvioEmailModel model) 
        { 
            ViewBag.Accion = "Save"; 
            var OnState = model.Entity.IsNew; 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity.LastUpdate = DateTime.Now; 
                    model.Entity.UpdatedBy = User.Identity.Name; 
                    if (model.Entity.IsNew) 
                    { 
                        model.Entity.CreationDate = DateTime.Now; 
                        model.Entity.CreatedBy = User.Identity.Name; 
                        model.Entity = Manager().GetBusinessLogic<ConfiguracionEnvioEmail>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<ConfiguracionEnvioEmail>().Modify(model.Entity); 
                    } 
                } 
                catch (Exception e) 
                { 
                    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage()); 
                } 
            } 
            else 
            { 
                 ModelState.AddModelError("Entity.Id", $"Error en vista, diferencia con base de datos. | " + ModelState.GetFullErrorMessage()); 
            } 
            return model; 
        } 

        [HttpPost]
        public IActionResult Delete(ConfiguracionEnvioEmailModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private ConfiguracionEnvioEmailModel DeleteModel(ConfiguracionEnvioEmailModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ConfiguracionEnvioEmailModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ConfiguracionEnvioEmail>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ConfiguracionEnvioEmail>().Remove(model.Entity); 
                    return newModel;
                } 
                catch (Exception e) 
                { 
                    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage()); 
                } 
            } 
            return model; 
        } 

        #endregion 

        #region Functions Detail 
        /*

        [HttpGet]
        public IActionResult NewDetail(long IdFather)
        {
            return PartialView("EditDetail", NewModelDetail(IdFather));
        }

        private ConfiguracionEnvioEmailModel NewModelDetail(long IdFather) 
        { 
            ConfiguracionEnvioEmailModel model = new ConfiguracionEnvioEmailModel(); 
            model.Entity.IdFather = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(ConfiguracionEnvioEmailModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(ConfiguracionEnvioEmailModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private ConfiguracionEnvioEmailModel DeleteModelDetail(ConfiguracionEnvioEmailModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ConfiguracionEnvioEmailModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ConfiguracionEnvioEmail>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ConfiguracionEnvioEmail>().Remove(model.Entity); 
                    return newModel;
                } 
                catch (Exception e) 
                { 
                    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage()); 
                } 
            } 
            return model; 
        } 

        #endregion 

        #region Funcions Detail Edit in Grid 

        [HttpPost] 
        public IActionResult AddInGrid(string values) 
        { 
             ConfiguracionEnvioEmail entity = new ConfiguracionEnvioEmail(); 
             JsonConvert.PopulateObject(values, entity); 
             ConfiguracionEnvioEmailModel model = new ConfiguracionEnvioEmailModel(); 
             model.Entity = entity; 
             model.Entity.IsNew = true; 
             this.EditModel(model); 
             if(ModelState.IsValid) 
                 return Ok(ModelState); 
             else 
                 return BadRequest(ModelState.GetFullErrorMessage()); 
        } 

        [HttpPost] 
        public IActionResult ModifyInGrid(int key, string values) 
        { 
             ConfiguracionEnvioEmail entity = Manager().GetBusinessLogic<ConfiguracionEnvioEmail>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             ConfiguracionEnvioEmailModel model = new ConfiguracionEnvioEmailModel(); 
             model.Entity = entity; 
             model.Entity.IsNew = false; 
             this.EditModel(model); 
             if(ModelState.IsValid) 
                 return Ok(ModelState); 
             else 
                 return BadRequest(ModelState.GetFullErrorMessage()); 
        } 

        [HttpPost]
        public void DeleteInGrid(int key)
        { 
             ConfiguracionEnvioEmail entity = Manager().GetBusinessLogic<ConfiguracionEnvioEmail>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<ConfiguracionEnvioEmail>().Remove(entity); 
        } 

        */
        #endregion 

        [HttpPost]
        public IActionResult ProbarEnvioCorreo(ConfiguracionEnvioEmailModel model)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                Manager().ConfiguracionEnvioEmailBusinessLogic().ProbarEnvioCorreo(model.Entity);
                model = EditModel(model);
                result.Add("TieneError", false);
                result.Add("Msg", $"La prueba del envio de correos fue satisfactorio. Revisar la bandeja de entrada de {model.Entity.CorreoElectronico}");
            }
            catch (Exception e)
            {
                result.Add("TieneError", true);
                result.Add("Error", e.GetFullErrorMessage());
            }

            return new OkObjectResult(result);
        }
    }
}
