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

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class CategoriasIngresosController : BaseAppController
    {

        //private const string Prefix = "CategoriasIngresos"; 

        public CategoriasIngresosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<CategoriasIngresos>().Tabla(true), loadOptions);
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

        private CategoriasIngresosModel NewModel() 
        { 
            CategoriasIngresosModel model = new CategoriasIngresosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private CategoriasIngresosModel EditModel(long Id) 
        { 
            CategoriasIngresosModel model = new CategoriasIngresosModel();
            model.Entity = Manager().GetBusinessLogic<CategoriasIngresos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(CategoriasIngresosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private CategoriasIngresosModel EditModel(CategoriasIngresosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<CategoriasIngresos>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<CategoriasIngresos>().Modify(model.Entity); 
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
        public IActionResult Delete(CategoriasIngresosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private CategoriasIngresosModel DeleteModel(CategoriasIngresosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            CategoriasIngresosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<CategoriasIngresos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<CategoriasIngresos>().Remove(model.Entity); 
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

        private CategoriasIngresosModel NewModelDetail(long IdFather) 
        { 
            CategoriasIngresosModel model = new CategoriasIngresosModel(); 
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
        public IActionResult EditDetail(CategoriasIngresosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(CategoriasIngresosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private CategoriasIngresosModel DeleteModelDetail(CategoriasIngresosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            CategoriasIngresosModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<CategoriasIngresos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<CategoriasIngresos>().Remove(model.Entity); 
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
             CategoriasIngresos entity = new CategoriasIngresos(); 
             JsonConvert.PopulateObject(values, entity); 
             CategoriasIngresosModel model = new CategoriasIngresosModel(); 
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
             CategoriasIngresos entity = Manager().GetBusinessLogic<CategoriasIngresos>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             CategoriasIngresosModel model = new CategoriasIngresosModel(); 
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
             CategoriasIngresos entity = Manager().GetBusinessLogic<CategoriasIngresos>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<CategoriasIngresos>().Remove(entity); 
        } 

        */
        #endregion 

    }
}
