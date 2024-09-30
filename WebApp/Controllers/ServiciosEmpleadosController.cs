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
    public partial class ServiciosEmpleadosController : BaseAppController
    {

        //private const string Prefix = "ServiciosEmpleados"; 

        public ServiciosEmpleadosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ServiciosEmpleados>().Tabla(true), loadOptions);
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

        private ServiciosEmpleadosModel NewModel() 
        { 
            ServiciosEmpleadosModel model = new ServiciosEmpleadosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private ServiciosEmpleadosModel EditModel(long Id) 
        { 
            ServiciosEmpleadosModel model = new ServiciosEmpleadosModel();
            model.Entity = Manager().GetBusinessLogic<ServiciosEmpleados>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(ServiciosEmpleadosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private ServiciosEmpleadosModel EditModel(ServiciosEmpleadosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<ServiciosEmpleados>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<ServiciosEmpleados>().Modify(model.Entity); 
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
        public IActionResult Delete(ServiciosEmpleadosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private ServiciosEmpleadosModel DeleteModel(ServiciosEmpleadosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ServiciosEmpleadosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ServiciosEmpleados>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ServiciosEmpleados>().Remove(model.Entity); 
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

        private ServiciosEmpleadosModel NewModelDetail(long IdFather) 
        { 
            ServiciosEmpleadosModel model = new ServiciosEmpleadosModel(); 
            model.Entity.EmpleadosId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(ServiciosEmpleadosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(ServiciosEmpleadosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private ServiciosEmpleadosModel DeleteModelDetail(ServiciosEmpleadosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ServiciosEmpleadosModel newModel = NewModelDetail(model.Entity.EmpleadosId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ServiciosEmpleados>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ServiciosEmpleados>().Remove(model.Entity); 
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
             ServiciosEmpleados entity = new ServiciosEmpleados(); 
             JsonConvert.PopulateObject(values, entity); 
             ServiciosEmpleadosModel model = new ServiciosEmpleadosModel(); 
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
             ServiciosEmpleados entity = Manager().GetBusinessLogic<ServiciosEmpleados>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             ServiciosEmpleadosModel model = new ServiciosEmpleadosModel(); 
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
             ServiciosEmpleados entity = Manager().GetBusinessLogic<ServiciosEmpleados>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<ServiciosEmpleados>().Remove(entity); 
        } 

        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetEmpleadosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empleados>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetServiciosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Servicios>().Tabla(true).Where(x=>x.EstadosId == 30 && x.RequiereProfesional), loadOptions);
        } 
       #endregion

    }
}
