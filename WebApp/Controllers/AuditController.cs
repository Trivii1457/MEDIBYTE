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
using Newtonsoft.Json.Converters;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class AuditController : BaseAppController
    {

        //private const string Prefix = "Audit"; 

        public AuditController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Audit>().Tabla(true), loadOptions);
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

        private AuditModel NewModel() 
        { 
            AuditModel model = new AuditModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private AuditModel EditModel(long Id) 
        { 
            AuditModel model = new AuditModel();
            model.Entity = Manager().GetBusinessLogic<Audit>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            if( !string.IsNullOrWhiteSpace( model.Entity.OldValues))
            {
                model.Entity.ListOldValues = JsonConvert.DeserializeObject<Dictionary<string, object>>(model.Entity.OldValues)
                    .Select(x=> new TableColumns { Column = x.Key, Value = x.Value }).ToList();

            }
            if (!string.IsNullOrWhiteSpace(model.Entity.NewValues))
            {
                model.Entity.ListNewValues = JsonConvert.DeserializeObject<Dictionary<string, object>>(model.Entity.NewValues)
                    .Select(x => new TableColumns { Column = x.Key, Value = x.Value }).ToList();
            }
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(AuditModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private AuditModel EditModel(AuditModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<Audit>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<Audit>().Modify(model.Entity); 
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
        public IActionResult Delete(AuditModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private AuditModel DeleteModel(AuditModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AuditModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Audit>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Audit>().Remove(model.Entity); 
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

        private AuditModel NewModelDetail(long IdFather) 
        { 
            AuditModel model = new AuditModel(); 
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
        public IActionResult EditDetail(AuditModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(AuditModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private AuditModel DeleteModelDetail(AuditModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AuditModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Audit>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Audit>().Remove(model.Entity); 
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
             Audit entity = new Audit(); 
             JsonConvert.PopulateObject(values, entity); 
             AuditModel model = new AuditModel(); 
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
             Audit entity = Manager().GetBusinessLogic<Audit>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             AuditModel model = new AuditModel(); 
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
             Audit entity = Manager().GetBusinessLogic<Audit>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Audit>().Remove(entity); 
        } 

        */
        #endregion 

    }
}
