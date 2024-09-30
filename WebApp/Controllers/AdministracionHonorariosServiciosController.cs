using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class AdministracionHonorariosServiciosController : BaseAppController
    {

        //private const string Prefix = "AdministracionHonorariosServicios"; 

        public AdministracionHonorariosServiciosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<AdministracionHonorariosServicios>().Tabla(true), loadOptions);
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

        private AdministracionHonorariosServiciosModel NewModel() 
        { 
            AdministracionHonorariosServiciosModel model = new AdministracionHonorariosServiciosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private AdministracionHonorariosServiciosModel EditModel(long Id) 
        { 
            AdministracionHonorariosServiciosModel model = new AdministracionHonorariosServiciosModel();
            model.Entity = Manager().GetBusinessLogic<AdministracionHonorariosServicios>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(AdministracionHonorariosServiciosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private AdministracionHonorariosServiciosModel EditModel(AdministracionHonorariosServiciosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<AdministracionHonorariosServicios>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<AdministracionHonorariosServicios>().Modify(model.Entity); 
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
        public IActionResult Delete(AdministracionHonorariosServiciosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private AdministracionHonorariosServiciosModel DeleteModel(AdministracionHonorariosServiciosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AdministracionHonorariosServiciosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<AdministracionHonorariosServicios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<AdministracionHonorariosServicios>().Remove(model.Entity); 
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
        

        [HttpGet]
        public IActionResult NewDetail(long IdFather)
        {
            return PartialView("EditDetail", NewModelDetail(IdFather));
        }

        private AdministracionHonorariosServiciosModel NewModelDetail(long IdFather) 
        { 
            AdministracionHonorariosServiciosModel model = new AdministracionHonorariosServiciosModel(); 
            model.Entity.AdministracionHonorariosId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(AdministracionHonorariosServiciosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(AdministracionHonorariosServiciosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private AdministracionHonorariosServiciosModel DeleteModelDetail(AdministracionHonorariosServiciosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AdministracionHonorariosServiciosModel newModel = NewModelDetail(model.Entity.AdministracionHonorariosId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<AdministracionHonorariosServicios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<AdministracionHonorariosServicios>().Remove(model.Entity); 
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
             AdministracionHonorariosServicios entity = new AdministracionHonorariosServicios(); 
             JsonConvert.PopulateObject(values, entity); 
             AdministracionHonorariosServiciosModel model = new AdministracionHonorariosServiciosModel(); 
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
             AdministracionHonorariosServicios entity = Manager().GetBusinessLogic<AdministracionHonorariosServicios>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             AdministracionHonorariosServiciosModel model = new AdministracionHonorariosServiciosModel(); 
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
             AdministracionHonorariosServicios entity = Manager().GetBusinessLogic<AdministracionHonorariosServicios>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<AdministracionHonorariosServicios>().Remove(entity); 
        } 

        
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetAdministracionHonorariosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<AdministracionHonorarios>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetServiciosId(DataSourceLoadOptions loadOptions)
        {
            List<string> codes = new List<string> { "COP", "CM", "CR", "PC" };
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Servicios>().Tabla(true).Where(x=>x.EstadosId == 30 && !codes.Contains(x.Codigo)), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEstadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        }
        #endregion

    }
}
