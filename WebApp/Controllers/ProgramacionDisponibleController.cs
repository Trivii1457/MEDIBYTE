using DevExtreme.AspNet.Data;
using WidgetGallery;
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

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class ProgramacionDisponibleController : BaseAppController
    {

        //private const string Prefix = "ProgramacionDisponible"; 

        public ProgramacionDisponibleController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ProgramacionDisponible>().Tabla(true), loadOptions);
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

        private ProgramacionDisponibleModel NewModel() 
        { 
            ProgramacionDisponibleModel model = new ProgramacionDisponibleModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private ProgramacionDisponibleModel EditModel(long Id) 
        { 
            ProgramacionDisponibleModel model = new ProgramacionDisponibleModel();
            model.Entity = Manager().GetBusinessLogic<ProgramacionDisponible>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(ProgramacionDisponibleModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private ProgramacionDisponibleModel EditModel(ProgramacionDisponibleModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<ProgramacionDisponible>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<ProgramacionDisponible>().Modify(model.Entity); 
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
        public IActionResult Delete(ProgramacionDisponibleModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private ProgramacionDisponibleModel DeleteModel(ProgramacionDisponibleModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ProgramacionDisponibleModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ProgramacionDisponible>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ProgramacionDisponible>().Remove(model.Entity); 
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

        private ProgramacionDisponibleModel NewModelDetail(long IdFather) 
        { 
            ProgramacionDisponibleModel model = new ProgramacionDisponibleModel(); 
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
        public IActionResult EditDetail(ProgramacionDisponibleModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(ProgramacionDisponibleModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private ProgramacionDisponibleModel DeleteModelDetail(ProgramacionDisponibleModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ProgramacionDisponibleModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ProgramacionDisponible>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ProgramacionDisponible>().Remove(model.Entity); 
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
             ProgramacionDisponible entity = new ProgramacionDisponible(); 
             JsonConvert.PopulateObject(values, entity); 
             ProgramacionDisponibleModel model = new ProgramacionDisponibleModel(); 
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
             ProgramacionDisponible entity = Manager().GetBusinessLogic<ProgramacionDisponible>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             ProgramacionDisponibleModel model = new ProgramacionDisponibleModel(); 
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
             ProgramacionDisponible entity = Manager().GetBusinessLogic<ProgramacionDisponible>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<ProgramacionDisponible>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetEmpleadosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empleados>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetDiponibleEstadosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetProgramacionAgendaId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ProgramacionAgenda>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetServiciosId(DataSourceLoadOptions loadOptions)
        {
            List<string> codes = new List<string> { "COP", "CM", "CR", "PC" };
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Servicios>().Tabla(true).Where(x=>x.EstadosId == 30 && !codes.Contains(x.Codigo)), loadOptions);
        } 
       #endregion

    }
}
