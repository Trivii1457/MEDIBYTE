using DevExtreme.AspNet.Data;
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
    public partial class AdministracionHonorariosLecturasController : BaseAppController
    {

        //private const string Prefix = "AdministracionHonorariosLecturas"; 

        public AdministracionHonorariosLecturasController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<AdministracionHonorariosLecturas>().Tabla(true), loadOptions);
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

        private AdministracionHonorariosLecturasModel NewModel() 
        { 
            AdministracionHonorariosLecturasModel model = new AdministracionHonorariosLecturasModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private AdministracionHonorariosLecturasModel EditModel(long Id) 
        { 
            AdministracionHonorariosLecturasModel model = new AdministracionHonorariosLecturasModel();
            model.Entity = Manager().GetBusinessLogic<AdministracionHonorariosLecturas>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(AdministracionHonorariosLecturasModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private AdministracionHonorariosLecturasModel EditModel(AdministracionHonorariosLecturasModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<AdministracionHonorariosLecturas>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<AdministracionHonorariosLecturas>().Modify(model.Entity); 
                    } 
                } 
                catch (Exception e) 
                { 
                    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage()); 
                } 
            } 
            else 
            { 
                 ModelState.AddModelError("Entity.Id", "Error de codigo, el objeto a guardar tiene campos diferentes a los de la entidad."); 
            } 
            return model; 
        } 

        [HttpPost]
        public IActionResult Delete(AdministracionHonorariosLecturasModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private AdministracionHonorariosLecturasModel DeleteModel(AdministracionHonorariosLecturasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AdministracionHonorariosLecturasModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<AdministracionHonorariosLecturas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<AdministracionHonorariosLecturas>().Remove(model.Entity); 
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

        private AdministracionHonorariosLecturasModel NewModelDetail(long IdFather) 
        { 
            AdministracionHonorariosLecturasModel model = new AdministracionHonorariosLecturasModel(); 
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
        public IActionResult EditDetail(AdministracionHonorariosLecturasModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(AdministracionHonorariosLecturasModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private AdministracionHonorariosLecturasModel DeleteModelDetail(AdministracionHonorariosLecturasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AdministracionHonorariosLecturasModel newModel = NewModelDetail(model.Entity.AdministracionHonorariosId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<AdministracionHonorariosLecturas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<AdministracionHonorariosLecturas>().Remove(model.Entity); 
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
             AdministracionHonorariosLecturas entity = new AdministracionHonorariosLecturas(); 
             JsonConvert.PopulateObject(values, entity); 
             AdministracionHonorariosLecturasModel model = new AdministracionHonorariosLecturasModel(); 
             model.Entity = entity; 
             model.Entity.IsNew = true; 
             this.EditModel(model); 
             if(ModelState.IsValid) 
                 return Ok(ModelState); 
             else 
                 return BadRequest(ModelState); 
        } 

        [HttpPost] 
        public IActionResult ModifyInGrid(int key, string values) 
        { 
             AdministracionHonorariosLecturas entity = Manager().GetBusinessLogic<AdministracionHonorariosLecturas>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             AdministracionHonorariosLecturasModel model = new AdministracionHonorariosLecturasModel(); 
             model.Entity = entity; 
             model.Entity.IsNew = false; 
             this.EditModel(model); 
             if(ModelState.IsValid) 
                 return Ok(ModelState); 
             else 
                 return BadRequest(ModelState); 
        } 

        [HttpPost]
        public void DeleteInGrid(int key)
        { 
             AdministracionHonorariosLecturas entity = Manager().GetBusinessLogic<AdministracionHonorariosLecturas>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<AdministracionHonorariosLecturas>().Remove(entity); 
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
