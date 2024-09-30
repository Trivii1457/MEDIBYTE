using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Backend.Application;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class PacientesEntidadesController : BaseAppController
    {

        //private const string Prefix = "PacientesEntidades"; 

        public PacientesEntidadesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<PacientesEntidades>().Tabla(true), loadOptions);
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

        private PacientesEntidadesModel NewModel() 
        { 
            PacientesEntidadesModel model = new PacientesEntidadesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private PacientesEntidadesModel EditModel(long Id) 
        { 
            PacientesEntidadesModel model = new PacientesEntidadesModel();
            model.Entity = Manager().GetBusinessLogic<PacientesEntidades>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(PacientesEntidadesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private PacientesEntidadesModel EditModel(PacientesEntidadesModel model) 
        { 
            ViewBag.Accion = "Save"; 
            var OnState = model.Entity.IsNew;

            bool esEntidadEPS = Manager().GetBusinessLogic<Entidades>().FindById(x => x.Id == model.Entity.EntidadesId, true).TipoEntidades.Codigo == "EPS";
            if (esEntidadEPS)
            {
                var data = (from P in Manager().GetBusinessLogic<PacientesEntidades>().FindAll(x => x.PacientesId == model.Entity.PacientesId, true)
                            join T in Manager().GetBusinessLogic<TipoEntidades>().FindAll(null) on P.Entidades.TipoEntidadesId equals T.Id
                            where T.Codigo == "EPS"
                            select P.Entidades).ToList();
                if(data != null && data.Count > 0)
                    ModelState.AddModelError("Error: ", DApp.DefaultLanguage.GetResource("Pacientes.YAEXISTEENTIDADEPS") + " " + data.First().Alias);
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
                        model.Entity = Manager().GetBusinessLogic<PacientesEntidades>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<PacientesEntidades>().Modify(model.Entity); 
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
        public IActionResult Delete(PacientesEntidadesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private PacientesEntidadesModel DeleteModel(PacientesEntidadesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            PacientesEntidadesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<PacientesEntidades>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<PacientesEntidades>().Remove(model.Entity); 
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

        private PacientesEntidadesModel NewModelDetail(long IdFather) 
        { 
            PacientesEntidadesModel model = new PacientesEntidadesModel(); 
            model.Entity.PacientesId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(PacientesEntidadesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(PacientesEntidadesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private PacientesEntidadesModel DeleteModelDetail(PacientesEntidadesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            PacientesEntidadesModel newModel = NewModelDetail(model.Entity.PacientesId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<PacientesEntidades>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<PacientesEntidades>().Remove(model.Entity); 
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
             PacientesEntidades entity = new PacientesEntidades(); 
             JsonConvert.PopulateObject(values, entity); 
             PacientesEntidadesModel model = new PacientesEntidadesModel(); 
             model.Entity = entity; 
             model.Entity.IsNew = true; 
             this.EditModel(model); 
             if(ModelState.IsValid) 
                 return Ok(ModelState); 
             else 
                 return BadRequest(ModelState.GetFullErrorMessage()); 
        } 

        [HttpPost] 
        public IActionResult ModifyInGrid(int key, string values) 
        { 
             PacientesEntidades entity = Manager().GetBusinessLogic<PacientesEntidades>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             PacientesEntidadesModel model = new PacientesEntidadesModel(); 
             model.Entity = entity; 
             model.Entity.IsNew = false; 
             this.EditModel(model); 
             if(ModelState.IsValid) 
                 return Ok(ModelState); 
             else 
                 return BadRequest(ModelState.GetFullErrorMessage()); 
        } 

        [HttpPost]
        public void DeleteInGrid(int key)
        { 
             PacientesEntidades entity = Manager().GetBusinessLogic<PacientesEntidades>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<PacientesEntidades>().Remove(entity); 
        } 

        
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetEntidadesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Entidades>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetPacientesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Pacientes>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetTipoEntidadesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TipoEntidades>().Tabla(true), loadOptions);
        }
        #endregion

    }
}
