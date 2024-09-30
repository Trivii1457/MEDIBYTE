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
using WidgetGallery;
using System.Collections.Generic;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class ImagenesDiagnosticasController : BaseAppController
    {

        //private const string Prefix = "ImagenesDiagnosticas"; 

        public ImagenesDiagnosticasController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ImagenesDiagnosticas>().Tabla(true), loadOptions);
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

        private ImagenesDiagnosticasModel NewModel() 
        { 
            ImagenesDiagnosticasModel model = new ImagenesDiagnosticasModel();
            model.Entity.IsNew = true;
            model.Entity.Fecha = DateTime.Now;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private ImagenesDiagnosticasModel EditModel(long Id) 
        { 
            ImagenesDiagnosticasModel model = new ImagenesDiagnosticasModel();
            model.Entity = Manager().GetBusinessLogic<ImagenesDiagnosticas>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(ImagenesDiagnosticasModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private ImagenesDiagnosticasModel EditModel(ImagenesDiagnosticasModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<ImagenesDiagnosticas>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<ImagenesDiagnosticas>().Modify(model.Entity); 
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
        public IActionResult Delete(ImagenesDiagnosticasModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private ImagenesDiagnosticasModel DeleteModel(ImagenesDiagnosticasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ImagenesDiagnosticasModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ImagenesDiagnosticas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ImagenesDiagnosticas>().Remove(model.Entity); 
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

        private ImagenesDiagnosticasModel NewModelDetail(long IdFather) 
        { 
            ImagenesDiagnosticasModel model = new ImagenesDiagnosticasModel(); 
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
        public IActionResult EditDetail(ImagenesDiagnosticasModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(ImagenesDiagnosticasModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private ImagenesDiagnosticasModel DeleteModelDetail(ImagenesDiagnosticasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ImagenesDiagnosticasModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ImagenesDiagnosticas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ImagenesDiagnosticas>().Remove(model.Entity); 
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
             ImagenesDiagnosticas entity = new ImagenesDiagnosticas(); 
             JsonConvert.PopulateObject(values, entity); 
             ImagenesDiagnosticasModel model = new ImagenesDiagnosticasModel(); 
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
             ImagenesDiagnosticas entity = Manager().GetBusinessLogic<ImagenesDiagnosticas>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             ImagenesDiagnosticasModel model = new ImagenesDiagnosticasModel(); 
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
             ImagenesDiagnosticas entity = Manager().GetBusinessLogic<ImagenesDiagnosticas>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<ImagenesDiagnosticas>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetPacientesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Pacientes>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetServiciosId(DataSourceLoadOptions loadOptions)
        {
            List<string> codes = new List<string> { "COP", "CM", "CR", "PC" };
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Servicios>().Tabla(true).Where(x=>x.EstadosId == 30 && !codes.Contains(x.Codigo)), loadOptions);
        } 
       #endregion

    }
}
