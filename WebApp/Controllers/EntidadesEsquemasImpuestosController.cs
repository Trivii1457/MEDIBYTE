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

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class EntidadesEsquemasImpuestosController : BaseAppController
    {

        //private const string Prefix = "EntidadesEsquemasImpuestos"; 

        public EntidadesEsquemasImpuestosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<EntidadesEsquemasImpuestos>().Tabla(true), loadOptions);
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

        private EntidadesEsquemasImpuestosModel NewModel() 
        { 
            EntidadesEsquemasImpuestosModel model = new EntidadesEsquemasImpuestosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private EntidadesEsquemasImpuestosModel EditModel(long Id) 
        { 
            EntidadesEsquemasImpuestosModel model = new EntidadesEsquemasImpuestosModel();
            model.Entity = Manager().GetBusinessLogic<EntidadesEsquemasImpuestos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(EntidadesEsquemasImpuestosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private EntidadesEsquemasImpuestosModel EditModel(EntidadesEsquemasImpuestosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<EntidadesEsquemasImpuestos>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<EntidadesEsquemasImpuestos>().Modify(model.Entity); 
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
        public IActionResult Delete(EntidadesEsquemasImpuestosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private EntidadesEsquemasImpuestosModel DeleteModel(EntidadesEsquemasImpuestosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EntidadesEsquemasImpuestosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<EntidadesEsquemasImpuestos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<EntidadesEsquemasImpuestos>().Remove(model.Entity); 
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

        private EntidadesEsquemasImpuestosModel NewModelDetail(long IdFather) 
        { 
            EntidadesEsquemasImpuestosModel model = new EntidadesEsquemasImpuestosModel(); 
            model.Entity.EntidadesId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(EntidadesEsquemasImpuestosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(EntidadesEsquemasImpuestosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private EntidadesEsquemasImpuestosModel DeleteModelDetail(EntidadesEsquemasImpuestosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EntidadesEsquemasImpuestosModel newModel = NewModelDetail(model.Entity.EntidadesId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<EntidadesEsquemasImpuestos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<EntidadesEsquemasImpuestos>().Remove(model.Entity); 
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
             EntidadesEsquemasImpuestos entity = new EntidadesEsquemasImpuestos(); 
             JsonConvert.PopulateObject(values, entity); 
             EntidadesEsquemasImpuestosModel model = new EntidadesEsquemasImpuestosModel(); 
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
             EntidadesEsquemasImpuestos entity = Manager().GetBusinessLogic<EntidadesEsquemasImpuestos>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             EntidadesEsquemasImpuestosModel model = new EntidadesEsquemasImpuestosModel(); 
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
             EntidadesEsquemasImpuestos entity = Manager().GetBusinessLogic<EntidadesEsquemasImpuestos>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<EntidadesEsquemasImpuestos>().Remove(entity); 
        } 

        
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetEntidadesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Entidades>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEsquemasImpuestosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<EsquemasImpuestos>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
