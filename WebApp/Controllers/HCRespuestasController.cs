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
    public partial class HCRespuestasController : BaseAppController
    {

        //private const string Prefix = "HCRespuestas"; 

        public HCRespuestasController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HCRespuestas>().Tabla(true), loadOptions);
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

        private HCRespuestasModel NewModel() 
        { 
            HCRespuestasModel model = new HCRespuestasModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private HCRespuestasModel EditModel(long Id) 
        { 
            HCRespuestasModel model = new HCRespuestasModel();
            model.Entity = Manager().GetBusinessLogic<HCRespuestas>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(HCRespuestasModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private HCRespuestasModel EditModel(HCRespuestasModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<HCRespuestas>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<HCRespuestas>().Modify(model.Entity); 
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
        public IActionResult Delete(HCRespuestasModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private HCRespuestasModel DeleteModel(HCRespuestasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            HCRespuestasModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<HCRespuestas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<HCRespuestas>().Remove(model.Entity); 
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

        private HCRespuestasModel NewModelDetail(long IdFather)
        {
            HCRespuestasModel model = new HCRespuestasModel();
            model.Entity.HCPreguntaId = IdFather;
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(HCRespuestasModel model)
        {
            return PartialView("EditDetail", EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(HCRespuestasModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private HCRespuestasModel DeleteModelDetail(HCRespuestasModel model)
        {
            ViewBag.Accion = "Delete";
            HCRespuestasModel newModel = NewModelDetail(model.Entity.HCPreguntaId);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<HCRespuestas>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<HCRespuestas>().Remove(model.Entity);
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
            HCRespuestas entity = new HCRespuestas();
            JsonConvert.PopulateObject(values, entity);
            HCRespuestasModel model = new HCRespuestasModel();
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
            HCRespuestas entity = Manager().GetBusinessLogic<HCRespuestas>().FindById(x => x.Id == key, false);
            JsonConvert.PopulateObject(values, entity);
            HCRespuestasModel model = new HCRespuestasModel();
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
            HCRespuestas entity = Manager().GetBusinessLogic<HCRespuestas>().FindById(x => x.Id == key, false);
            Manager().GetBusinessLogic<HCRespuestas>().Remove(entity);
        }


        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetHCPreguntaId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HCPreguntas>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
