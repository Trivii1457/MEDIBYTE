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
    public partial class HCTiposPreguntasController : BaseAppController
    {

        //private const string Prefix = "HCTiposPreguntas"; 

        public HCTiposPreguntasController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HCTiposPreguntas>().Tabla(true), loadOptions);
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

        private HCTiposPreguntasModel NewModel() 
        { 
            HCTiposPreguntasModel model = new HCTiposPreguntasModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private HCTiposPreguntasModel EditModel(long Id) 
        { 
            HCTiposPreguntasModel model = new HCTiposPreguntasModel();
            model.Entity = Manager().GetBusinessLogic<HCTiposPreguntas>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(HCTiposPreguntasModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private HCTiposPreguntasModel EditModel(HCTiposPreguntasModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<HCTiposPreguntas>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<HCTiposPreguntas>().Modify(model.Entity); 
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
        public IActionResult Delete(HCTiposPreguntasModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private HCTiposPreguntasModel DeleteModel(HCTiposPreguntasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            HCTiposPreguntasModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<HCTiposPreguntas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<HCTiposPreguntas>().Remove(model.Entity); 
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

        private HCTiposPreguntasModel NewModelDetail(long IdFather)
        {
            HCTiposPreguntasModel model = new HCTiposPreguntasModel();
            model.Entity.HCTiposId = IdFather;
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(HCTiposPreguntasModel model)
        {
            return PartialView("EditDetail", EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(HCTiposPreguntasModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private HCTiposPreguntasModel DeleteModelDetail(HCTiposPreguntasModel model)
        {
            ViewBag.Accion = "Delete";
            HCTiposPreguntasModel newModel = NewModelDetail(model.Entity.HCTiposId);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<HCTiposPreguntas>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<HCTiposPreguntas>().Remove(model.Entity);
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
            HCTiposPreguntas entity = new HCTiposPreguntas();
            JsonConvert.PopulateObject(values, entity);
            HCTiposPreguntasModel model = new HCTiposPreguntasModel();
            model.Entity = entity;
            model.Entity.IsNew = true;
            this.EditModel(model);
            if (ModelState.IsValid)
                return Ok(ModelState);
            else
                return BadRequest(ModelState);
        }

        [HttpPost]
        public IActionResult ModifyInGrid(int key, string values)
        {
            HCTiposPreguntas entity = Manager().GetBusinessLogic<HCTiposPreguntas>().FindById(x => x.Id == key, false);
            JsonConvert.PopulateObject(values, entity);
            HCTiposPreguntasModel model = new HCTiposPreguntasModel();
            model.Entity = entity;
            model.Entity.IsNew = false;
            this.EditModel(model);
            if (ModelState.IsValid)
                return Ok(ModelState);
            else
                return BadRequest(ModelState);
        }

        [HttpPost]
        public void DeleteInGrid(int key)
        {
            HCTiposPreguntas entity = Manager().GetBusinessLogic<HCTiposPreguntas>().FindById(x => x.Id == key, false);
            Manager().GetBusinessLogic<HCTiposPreguntas>().Remove(entity);
        }


        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetHCPreguntasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HCPreguntas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetHCTiposId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HCTipos>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
