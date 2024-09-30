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

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class EmpleadosEspecialidadesController : BaseAppController
    {

        //private const string Prefix = "EmpleadosEspecialidades"; 

        public EmpleadosEspecialidadesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<EmpleadosEspecialidades>().Tabla(true), loadOptions);
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

        private EmpleadosEspecialidadesModel NewModel() 
        { 
            EmpleadosEspecialidadesModel model = new EmpleadosEspecialidadesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private EmpleadosEspecialidadesModel EditModel(long Id) 
        { 
            EmpleadosEspecialidadesModel model = new EmpleadosEspecialidadesModel();
            model.Entity = Manager().GetBusinessLogic<EmpleadosEspecialidades>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(EmpleadosEspecialidadesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private EmpleadosEspecialidadesModel EditModel(EmpleadosEspecialidadesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<EmpleadosEspecialidades>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<EmpleadosEspecialidades>().Modify(model.Entity); 
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
        public IActionResult Delete(EmpleadosEspecialidadesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private EmpleadosEspecialidadesModel DeleteModel(EmpleadosEspecialidadesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EmpleadosEspecialidadesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<EmpleadosEspecialidades>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<EmpleadosEspecialidades>().Remove(model.Entity); 
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
             EmpleadosEspecialidades entity = new EmpleadosEspecialidades(); 
             JsonConvert.PopulateObject(values, entity); 
             EmpleadosEspecialidadesModel model = new EmpleadosEspecialidadesModel(); 
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
             EmpleadosEspecialidades entity = Manager().GetBusinessLogic<EmpleadosEspecialidades>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             EmpleadosEspecialidadesModel model = new EmpleadosEspecialidadesModel(); 
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
             EmpleadosEspecialidades entity = Manager().GetBusinessLogic<EmpleadosEspecialidades>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<EmpleadosEspecialidades>().Remove(entity); 
        } 

        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetEmpleadosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empleados>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEspecialidadesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Especialidades>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
