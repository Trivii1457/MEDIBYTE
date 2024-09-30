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
    public partial class EmpleadosController : BaseAppController
    {

        //private const string Prefix = "Empleados"; 

        public EmpleadosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empleados>().Tabla(true), loadOptions);
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

        //private EmpleadosModel NewModel() 
        //{ 
        //    EmpleadosModel model = new EmpleadosModel();
        //    model.Entity.IsNew = true;
        //    return model; 
        //} 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        [HttpPost]
        public IActionResult Edit(EmpleadosModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        [HttpPost]
        public IActionResult Delete(EmpleadosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private EmpleadosModel DeleteModel(EmpleadosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EmpleadosModel newModel = NewModel();

            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Empleados>().FindById(x => x.Id == model.Entity.Id, true); 
                    Manager().GetBusinessLogic<Empleados>().Remove(model.Entity); 
                    return newModel;
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

        #endregion 

        #region Functions Detail 
        /*

        [HttpGet]
        public IActionResult NewDetail(long IdFather)
        {
            return PartialView("EditDetail", NewModelDetail(IdFather));
        }

        private EmpleadosModel NewModelDetail(long IdFather) 
        { 
            EmpleadosModel model = new EmpleadosModel(); 
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
        public IActionResult EditDetail(EmpleadosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(EmpleadosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private EmpleadosModel DeleteModelDetail(EmpleadosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EmpleadosModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Empleados>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Empleados>().Remove(model.Entity); 
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
             Empleados entity = new Empleados(); 
             JsonConvert.PopulateObject(values, entity); 
             EmpleadosModel model = new EmpleadosModel(); 
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
             Empleados entity = Manager().GetBusinessLogic<Empleados>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             EmpleadosModel model = new EmpleadosModel(); 
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
             Empleados entity = Manager().GetBusinessLogic<Empleados>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Empleados>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetEmpresasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empresas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetTipoEmpleados(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TipoEmpleados>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetUserId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<User>().Tabla(true).Where(x=>x.UserName != "SUPERCEO"), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEstadosCivilesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<EstadosCiviles>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetFirmaDigitalArchivoId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Archivos>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetSelloDigitalArchivoId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Archivos>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetTiposSangreId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposSangre>().Tabla(true), loadOptions);
        }
        #endregion

    }
}
