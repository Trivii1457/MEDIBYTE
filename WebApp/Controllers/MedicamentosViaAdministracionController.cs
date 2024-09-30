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
    public partial class MedicamentosViaAdministracionController : BaseAppController
    {

        //private const string Prefix = "MedicamentosViaAdministracion"; 

        public MedicamentosViaAdministracionController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<MedicamentosViaAdministracion>().Tabla(true), loadOptions);
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

        private MedicamentosViaAdministracionModel NewModel() 
        { 
            MedicamentosViaAdministracionModel model = new MedicamentosViaAdministracionModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private MedicamentosViaAdministracionModel EditModel(long Id) 
        { 
            MedicamentosViaAdministracionModel model = new MedicamentosViaAdministracionModel();
            model.Entity = Manager().GetBusinessLogic<MedicamentosViaAdministracion>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(MedicamentosViaAdministracionModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private MedicamentosViaAdministracionModel EditModel(MedicamentosViaAdministracionModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<MedicamentosViaAdministracion>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<MedicamentosViaAdministracion>().Modify(model.Entity); 
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
        public IActionResult Delete(MedicamentosViaAdministracionModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private MedicamentosViaAdministracionModel DeleteModel(MedicamentosViaAdministracionModel model)
        { 
            ViewBag.Accion = "Delete"; 
            MedicamentosViaAdministracionModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<MedicamentosViaAdministracion>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<MedicamentosViaAdministracion>().Remove(model.Entity); 
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

        private MedicamentosViaAdministracionModel NewModelDetail(long IdFather) 
        { 
            MedicamentosViaAdministracionModel model = new MedicamentosViaAdministracionModel(); 
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
        public IActionResult EditDetail(MedicamentosViaAdministracionModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(MedicamentosViaAdministracionModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private MedicamentosViaAdministracionModel DeleteModelDetail(MedicamentosViaAdministracionModel model)
        { 
            ViewBag.Accion = "Delete"; 
            MedicamentosViaAdministracionModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<MedicamentosViaAdministracion>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<MedicamentosViaAdministracion>().Remove(model.Entity); 
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
             MedicamentosViaAdministracion entity = new MedicamentosViaAdministracion(); 
             JsonConvert.PopulateObject(values, entity); 
             MedicamentosViaAdministracionModel model = new MedicamentosViaAdministracionModel(); 
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
             MedicamentosViaAdministracion entity = Manager().GetBusinessLogic<MedicamentosViaAdministracion>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             MedicamentosViaAdministracionModel model = new MedicamentosViaAdministracionModel(); 
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
             MedicamentosViaAdministracion entity = Manager().GetBusinessLogic<MedicamentosViaAdministracion>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<MedicamentosViaAdministracion>().Remove(entity); 
        } 

        */
        #endregion 

    }
}
