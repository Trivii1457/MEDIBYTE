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
    public partial class FormasPagosController : BaseAppController
    {

        //private const string Prefix = "FormasPagos"; 

        public FormasPagosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<FormasPagos>().Tabla(true), loadOptions);
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

        private FormasPagosModel NewModel() 
        { 
            FormasPagosModel model = new FormasPagosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private FormasPagosModel EditModel(long Id) 
        { 
            FormasPagosModel model = new FormasPagosModel();
            model.Entity = Manager().GetBusinessLogic<FormasPagos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(FormasPagosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private FormasPagosModel EditModel(FormasPagosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<FormasPagos>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<FormasPagos>().Modify(model.Entity); 
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
        public IActionResult Delete(FormasPagosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private FormasPagosModel DeleteModel(FormasPagosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            FormasPagosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<FormasPagos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<FormasPagos>().Remove(model.Entity); 
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

        private FormasPagosModel NewModelDetail(long IdFather) 
        { 
            FormasPagosModel model = new FormasPagosModel(); 
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
        public IActionResult EditDetail(FormasPagosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(FormasPagosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private FormasPagosModel DeleteModelDetail(FormasPagosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            FormasPagosModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<FormasPagos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<FormasPagos>().Remove(model.Entity); 
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
             FormasPagos entity = new FormasPagos(); 
             JsonConvert.PopulateObject(values, entity); 
             FormasPagosModel model = new FormasPagosModel(); 
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
             FormasPagos entity = Manager().GetBusinessLogic<FormasPagos>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             FormasPagosModel model = new FormasPagosModel(); 
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
             FormasPagos entity = Manager().GetBusinessLogic<FormasPagos>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<FormasPagos>().Remove(entity); 
        } 

        */
        #endregion 

    }
}