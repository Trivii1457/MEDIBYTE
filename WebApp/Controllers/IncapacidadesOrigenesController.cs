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

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class IncapacidadesOrigenesController : BaseAppController
    {

        //private const string Prefix = "IncapacidadesOrigenes"; 

        public IncapacidadesOrigenesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<IncapacidadesOrigenes>().Tabla(true), loadOptions);
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

        private IncapacidadesOrigenesModel NewModel() 
        { 
            IncapacidadesOrigenesModel model = new IncapacidadesOrigenesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private IncapacidadesOrigenesModel EditModel(long Id) 
        { 
            IncapacidadesOrigenesModel model = new IncapacidadesOrigenesModel();
            model.Entity = Manager().GetBusinessLogic<IncapacidadesOrigenes>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(IncapacidadesOrigenesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private IncapacidadesOrigenesModel EditModel(IncapacidadesOrigenesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<IncapacidadesOrigenes>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<IncapacidadesOrigenes>().Modify(model.Entity); 
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
        public IActionResult Delete(IncapacidadesOrigenesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private IncapacidadesOrigenesModel DeleteModel(IncapacidadesOrigenesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            IncapacidadesOrigenesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<IncapacidadesOrigenes>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<IncapacidadesOrigenes>().Remove(model.Entity); 
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

        private IncapacidadesOrigenesModel NewModelDetail(long IdFather) 
        { 
            IncapacidadesOrigenesModel model = new IncapacidadesOrigenesModel(); 
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
        public IActionResult EditDetail(IncapacidadesOrigenesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(IncapacidadesOrigenesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private IncapacidadesOrigenesModel DeleteModelDetail(IncapacidadesOrigenesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            IncapacidadesOrigenesModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<IncapacidadesOrigenes>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<IncapacidadesOrigenes>().Remove(model.Entity); 
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
             IncapacidadesOrigenes entity = new IncapacidadesOrigenes(); 
             JsonConvert.PopulateObject(values, entity); 
             IncapacidadesOrigenesModel model = new IncapacidadesOrigenesModel(); 
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
             IncapacidadesOrigenes entity = Manager().GetBusinessLogic<IncapacidadesOrigenes>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             IncapacidadesOrigenesModel model = new IncapacidadesOrigenesModel(); 
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
             IncapacidadesOrigenes entity = Manager().GetBusinessLogic<IncapacidadesOrigenes>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<IncapacidadesOrigenes>().Remove(entity); 
        } 

        */
        #endregion 

    }
}
