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
    public partial class HistoriasClinicasDiagnosticosController : BaseAppController
    {

        //private const string Prefix = "HistoriasClinicasDiagnosticos"; 

        public HistoriasClinicasDiagnosticosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().Tabla(true), loadOptions);
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

        private HistoriasClinicasDiagnosticosModel NewModel() 
        { 
            HistoriasClinicasDiagnosticosModel model = new HistoriasClinicasDiagnosticosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private HistoriasClinicasDiagnosticosModel EditModel(long Id) 
        { 
            HistoriasClinicasDiagnosticosModel model = new HistoriasClinicasDiagnosticosModel();
            model.Entity = Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(HistoriasClinicasDiagnosticosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private HistoriasClinicasDiagnosticosModel EditModel(HistoriasClinicasDiagnosticosModel model) 
        { 
            ViewBag.Accion = "Save"; 
            var OnState = model.Entity.IsNew;

            if (model.Entity.Principal)
            {
                var total = Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().Tabla(false).Count(x => x.Principal && x.HistoriasClinicasId == model.Entity.HistoriasClinicasId);
                if (total > 0)
                {
                    ModelState.AddModelError("Entity.Principal", "Ya existe un diagnostico principal.");
                }
            }

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
                        model.Entity = Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().Modify(model.Entity); 
                    } 
                } 
                catch (Exception e) 
                { 
                    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage()); 
                } 
            }  
            return model; 
        } 

        [HttpPost]
        public IActionResult Delete(HistoriasClinicasDiagnosticosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private HistoriasClinicasDiagnosticosModel DeleteModel(HistoriasClinicasDiagnosticosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            HistoriasClinicasDiagnosticosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().Remove(model.Entity); 
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

        private HistoriasClinicasDiagnosticosModel NewModelDetail(long IdFather)
        {
            HistoriasClinicasDiagnosticosModel model = new HistoriasClinicasDiagnosticosModel();
            model.Entity.HistoriasClinicasId = IdFather;
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(HistoriasClinicasDiagnosticosModel model)
        {
            return PartialView("EditDetail", EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(HistoriasClinicasDiagnosticosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private HistoriasClinicasDiagnosticosModel DeleteModelDetail(HistoriasClinicasDiagnosticosModel model)
        {
            ViewBag.Accion = "Delete";
            HistoriasClinicasDiagnosticosModel newModel = NewModelDetail(model.Entity.HistoriasClinicasId);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().Remove(model.Entity);
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
            HistoriasClinicasDiagnosticos entity = new HistoriasClinicasDiagnosticos();
            JsonConvert.PopulateObject(values, entity);
            HistoriasClinicasDiagnosticosModel model = new HistoriasClinicasDiagnosticosModel();
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
            HistoriasClinicasDiagnosticos entity = Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().FindById(x => x.Id == key, false);
            JsonConvert.PopulateObject(values, entity);
            HistoriasClinicasDiagnosticosModel model = new HistoriasClinicasDiagnosticosModel();
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
            HistoriasClinicasDiagnosticos entity = Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().FindById(x => x.Id == key, false);
            Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().Remove(entity);
        }


        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetDiagnosticosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Diagnosticos>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetHistoriasClinicasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetTiposDiagnosticosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposDiagnosticos>().Tabla(true), loadOptions);
        }
        #endregion

    }
}
