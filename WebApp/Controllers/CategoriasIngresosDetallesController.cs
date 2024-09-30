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
    public partial class CategoriasIngresosDetallesController : BaseAppController
    {

        //private const string Prefix = "CategoriasIngresosDetalles"; 

        public CategoriasIngresosDetallesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<CategoriasIngresosDetalles>().Tabla(true), loadOptions);
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

        private CategoriasIngresosDetallesModel NewModel() 
        { 
            CategoriasIngresosDetallesModel model = new CategoriasIngresosDetallesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private CategoriasIngresosDetallesModel EditModel(long Id) 
        { 
            CategoriasIngresosDetallesModel model = new CategoriasIngresosDetallesModel();
            model.Entity = Manager().GetBusinessLogic<CategoriasIngresosDetalles>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(CategoriasIngresosDetallesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private CategoriasIngresosDetallesModel EditModel(CategoriasIngresosDetallesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<CategoriasIngresosDetalles>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<CategoriasIngresosDetalles>().Modify(model.Entity); 
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
        public IActionResult Delete(CategoriasIngresosDetallesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private CategoriasIngresosDetallesModel DeleteModel(CategoriasIngresosDetallesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            CategoriasIngresosDetallesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<CategoriasIngresosDetalles>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<CategoriasIngresosDetalles>().Remove(model.Entity); 
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

        private CategoriasIngresosDetallesModel NewModelDetail(long IdFather) 
        { 
            CategoriasIngresosDetallesModel model = new CategoriasIngresosDetallesModel(); 
            model.Entity.CategoriasIngresosId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(CategoriasIngresosDetallesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(CategoriasIngresosDetallesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private CategoriasIngresosDetallesModel DeleteModelDetail(CategoriasIngresosDetallesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            CategoriasIngresosDetallesModel newModel = NewModelDetail(model.Entity.CategoriasIngresosId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<CategoriasIngresosDetalles>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<CategoriasIngresosDetalles>().Remove(model.Entity); 
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
             CategoriasIngresosDetalles entity = new CategoriasIngresosDetalles(); 
             JsonConvert.PopulateObject(values, entity); 
             CategoriasIngresosDetallesModel model = new CategoriasIngresosDetallesModel(); 
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
             CategoriasIngresosDetalles entity = Manager().GetBusinessLogic<CategoriasIngresosDetalles>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             CategoriasIngresosDetallesModel model = new CategoriasIngresosDetallesModel(); 
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
             CategoriasIngresosDetalles entity = Manager().GetBusinessLogic<CategoriasIngresosDetalles>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<CategoriasIngresosDetalles>().Remove(entity); 
        } 

        
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetCategoriasIngresosid(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<CategoriasIngresos>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
