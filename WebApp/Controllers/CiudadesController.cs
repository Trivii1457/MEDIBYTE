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
    public partial class CiudadesController : BaseAppController
    {

        //private const string Prefix = "Ciudades"; 

        public CiudadesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Ciudades>().Tabla(true), loadOptions);
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

        private CiudadesModel NewModel() 
        { 
            CiudadesModel model = new CiudadesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private CiudadesModel EditModel(long Id) 
        { 
            CiudadesModel model = new CiudadesModel();
            model.Entity = Manager().GetBusinessLogic<Ciudades>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(CiudadesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private CiudadesModel EditModel(CiudadesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<Ciudades>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<Ciudades>().Modify(model.Entity); 
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
        public IActionResult Delete(CiudadesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private CiudadesModel DeleteModel(CiudadesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            CiudadesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Ciudades>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Ciudades>().Remove(model.Entity); 
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

        private CiudadesModel NewModelDetail(long IdFather)
        {
            CiudadesModel model = new CiudadesModel();
            model.Entity.DepartamentosId = IdFather;
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(CiudadesModel model)
        {
            return PartialView("EditDetail", EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(CiudadesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private CiudadesModel DeleteModelDetail(CiudadesModel model)
        {
            ViewBag.Accion = "Delete";
            CiudadesModel newModel = NewModelDetail(model.Entity.DepartamentosId);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<Ciudades>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<Ciudades>().Remove(model.Entity);
                    return newModel;
                }
                catch (Exception e)
                {
                    while (e != null)
                    {
                        ModelState.AddModelError("Error: ", e.Message);
                        e = e.InnerException;
                    }
                }
            }
            return model;
        }

        #endregion

        #region Funcions Detail Edit in Grid 

        [HttpPost]
        public IActionResult AddInGrid(string values)
        {
            Ciudades entity = new Ciudades();
            JsonConvert.PopulateObject(values, entity);
            CiudadesModel model = new CiudadesModel();
            model.Entity = entity;
            model.Entity.IsNew = true;
            this.EditModel(model);
            if (ModelState.IsValid)
                return Ok(ModelState);
            else
                return BadRequest(ModelState.GetFullErrorMessage());
        }

        [HttpPost]
        public IActionResult ModifyInGrid(int key, string values)
        {
            Ciudades entity = Manager().GetBusinessLogic<Ciudades>().FindById(x => x.Id == key, false);
            JsonConvert.PopulateObject(values, entity);
            CiudadesModel model = new CiudadesModel();
            model.Entity = entity;
            model.Entity.IsNew = false;
            this.EditModel(model);
            if (ModelState.IsValid)
                return Ok(ModelState);
            else
                return BadRequest(ModelState.GetFullErrorMessage());
        }

        [HttpPost]
        public void DeleteInGrid(int key)
        {
            Ciudades entity = Manager().GetBusinessLogic<Ciudades>().FindById(x => x.Id == key, false);
            Manager().GetBusinessLogic<Ciudades>().Remove(entity);
        }


        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetDepartamentosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Departamentos>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
