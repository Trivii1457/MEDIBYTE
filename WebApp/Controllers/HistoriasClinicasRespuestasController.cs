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
using Microsoft.EntityFrameworkCore;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class HistoriasClinicasRespuestasController : BaseAppController
    {

        //private const string Prefix = "HistoriasClinicasRespuestas"; 

        public HistoriasClinicasRespuestasController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions, long idGrupo, long idHC)
        {
            return DataSourceLoader.Load(
                Manager().GetBusinessLogic<HistoriasClinicasRespuestas>().Tabla(true)
                .Include(x => x.HCRespuestas)
                .Include(x=>x.HCRespuestas.HCPregunta)
                .Where(x=>x.HCRespuestas.HCPreguntaId == idGrupo && x.HIstoriasClinicasId == idHC)
                , loadOptions) ;
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

        private HistoriasClinicasRespuestasModel NewModel() 
        { 
            HistoriasClinicasRespuestasModel model = new HistoriasClinicasRespuestasModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private HistoriasClinicasRespuestasModel EditModel(long Id) 
        { 
            HistoriasClinicasRespuestasModel model = new HistoriasClinicasRespuestasModel();
            model.Entity = Manager().GetBusinessLogic<HistoriasClinicasRespuestas>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(HistoriasClinicasRespuestasModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private HistoriasClinicasRespuestasModel EditModel(HistoriasClinicasRespuestasModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<HistoriasClinicasRespuestas>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<HistoriasClinicasRespuestas>().Modify(model.Entity); 
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
        public IActionResult Delete(HistoriasClinicasRespuestasModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private HistoriasClinicasRespuestasModel DeleteModel(HistoriasClinicasRespuestasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            HistoriasClinicasRespuestasModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<HistoriasClinicasRespuestas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<HistoriasClinicasRespuestas>().Remove(model.Entity); 
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

        private HistoriasClinicasRespuestasModel NewModelDetail(long IdFather)
        {
            HistoriasClinicasRespuestasModel model = new HistoriasClinicasRespuestasModel();
            model.Entity.HIstoriasClinicasId = IdFather;
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(HistoriasClinicasRespuestasModel model)
        {
            return PartialView("EditDetail", EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(HistoriasClinicasRespuestasModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private HistoriasClinicasRespuestasModel DeleteModelDetail(HistoriasClinicasRespuestasModel model)
        {
            ViewBag.Accion = "Delete";
            HistoriasClinicasRespuestasModel newModel = NewModelDetail(model.Entity.HIstoriasClinicasId);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<HistoriasClinicasRespuestas>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<HistoriasClinicasRespuestas>().Remove(model.Entity);
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
            HistoriasClinicasRespuestas entity = new HistoriasClinicasRespuestas();
            JsonConvert.PopulateObject(values, entity);
            HistoriasClinicasRespuestasModel model = new HistoriasClinicasRespuestasModel();
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
            HistoriasClinicasRespuestas entity = Manager().GetBusinessLogic<HistoriasClinicasRespuestas>().FindById(x => x.Id == key, false);
            JsonConvert.PopulateObject(values, entity);
            HistoriasClinicasRespuestasModel model = new HistoriasClinicasRespuestasModel();
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
            HistoriasClinicasRespuestas entity = Manager().GetBusinessLogic<HistoriasClinicasRespuestas>().FindById(x => x.Id == key, false);
            Manager().GetBusinessLogic<HistoriasClinicasRespuestas>().Remove(entity);
        }


        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetHCRespuestasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HCRespuestas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetHIstoriasClinicasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
