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
    public partial class RadicacionCuentasDetallesController : BaseAppController
    {

        //private const string Prefix = "RadicacionCuentasDetalles"; 

        public RadicacionCuentasDetallesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<RadicacionCuentasDetalles>().Tabla(true), loadOptions);
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

        private RadicacionCuentasDetallesModel NewModel() 
        { 
            RadicacionCuentasDetallesModel model = new RadicacionCuentasDetallesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private RadicacionCuentasDetallesModel EditModel(long Id) 
        { 
            RadicacionCuentasDetallesModel model = new RadicacionCuentasDetallesModel();
            model.Entity = Manager().GetBusinessLogic<RadicacionCuentasDetalles>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(RadicacionCuentasDetallesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private RadicacionCuentasDetallesModel EditModel(RadicacionCuentasDetallesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<RadicacionCuentasDetalles>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<RadicacionCuentasDetalles>().Modify(model.Entity); 
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
        public IActionResult Delete(RadicacionCuentasDetallesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private RadicacionCuentasDetallesModel DeleteModel(RadicacionCuentasDetallesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            RadicacionCuentasDetallesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<RadicacionCuentasDetalles>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<RadicacionCuentasDetalles>().Remove(model.Entity); 
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

        private RadicacionCuentasDetallesModel NewModelDetail(long IdFather) 
        { 
            RadicacionCuentasDetallesModel model = new RadicacionCuentasDetallesModel(); 
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
        public IActionResult EditDetail(RadicacionCuentasDetallesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(RadicacionCuentasDetallesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private RadicacionCuentasDetallesModel DeleteModelDetail(RadicacionCuentasDetallesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            RadicacionCuentasDetallesModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<RadicacionCuentasDetalles>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<RadicacionCuentasDetalles>().Remove(model.Entity); 
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
             RadicacionCuentasDetalles entity = new RadicacionCuentasDetalles(); 
             JsonConvert.PopulateObject(values, entity); 
             RadicacionCuentasDetallesModel model = new RadicacionCuentasDetallesModel(); 
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
             RadicacionCuentasDetalles entity = Manager().GetBusinessLogic<RadicacionCuentasDetalles>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             RadicacionCuentasDetallesModel model = new RadicacionCuentasDetallesModel(); 
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
             RadicacionCuentasDetalles entity = Manager().GetBusinessLogic<RadicacionCuentasDetalles>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<RadicacionCuentasDetalles>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetFacurasId(DataSourceLoadOptions loadOptions)
        {
            var result = Manager().GetBusinessLogic<Facturas>().Tabla(true).ToList();
            return DataSourceLoader.Load(result, loadOptions);
        } 
        [HttpPost]
        public LoadResult GetRadicacionCuentasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<RadicacionCuentas>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
