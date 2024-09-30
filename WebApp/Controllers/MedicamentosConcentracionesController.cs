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
    public partial class MedicamentosConcentracionesController : BaseAppController
    {

        //private const string Prefix = "MedicamentosConcentraciones"; 

        public MedicamentosConcentracionesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<MedicamentosConcentraciones>().Tabla(true), loadOptions);
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

        private MedicamentosConcentracionesModel NewModel() 
        { 
            MedicamentosConcentracionesModel model = new MedicamentosConcentracionesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private MedicamentosConcentracionesModel EditModel(long Id) 
        { 
            MedicamentosConcentracionesModel model = new MedicamentosConcentracionesModel();
            model.Entity = Manager().GetBusinessLogic<MedicamentosConcentraciones>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(MedicamentosConcentracionesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private MedicamentosConcentracionesModel EditModel(MedicamentosConcentracionesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<MedicamentosConcentraciones>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<MedicamentosConcentraciones>().Modify(model.Entity); 
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
        public IActionResult Delete(MedicamentosConcentracionesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private MedicamentosConcentracionesModel DeleteModel(MedicamentosConcentracionesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            MedicamentosConcentracionesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<MedicamentosConcentraciones>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<MedicamentosConcentraciones>().Remove(model.Entity); 
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

        private MedicamentosConcentracionesModel NewModelDetail(long IdFather) 
        { 
            MedicamentosConcentracionesModel model = new MedicamentosConcentracionesModel(); 
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
        public IActionResult EditDetail(MedicamentosConcentracionesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(MedicamentosConcentracionesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private MedicamentosConcentracionesModel DeleteModelDetail(MedicamentosConcentracionesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            MedicamentosConcentracionesModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<MedicamentosConcentraciones>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<MedicamentosConcentraciones>().Remove(model.Entity); 
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
             MedicamentosConcentraciones entity = new MedicamentosConcentraciones(); 
             JsonConvert.PopulateObject(values, entity); 
             MedicamentosConcentracionesModel model = new MedicamentosConcentracionesModel(); 
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
             MedicamentosConcentraciones entity = Manager().GetBusinessLogic<MedicamentosConcentraciones>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             MedicamentosConcentracionesModel model = new MedicamentosConcentracionesModel(); 
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
             MedicamentosConcentraciones entity = Manager().GetBusinessLogic<MedicamentosConcentraciones>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<MedicamentosConcentraciones>().Remove(entity); 
        } 

        */
        #endregion 

    }
}
