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
    public partial class EntidadesController : BaseAppController
    {

        //private const string Prefix = "Entidades"; 

        public EntidadesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Entidades>().Tabla(true), loadOptions);
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

        private EntidadesModel NewModel() 
        { 
            EntidadesModel model = new EntidadesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        //private EntidadesModel EditModel(long Id) 
        //{ 
        //    EntidadesModel model = new EntidadesModel();
        //    model.Entity = Manager().GetBusinessLogic<Entidades>().FindById(x => x.Id == Id, false);
        //    model.Entity.IsNew = false;
        //    return model; 
        //} 

        [HttpPost]
        public IActionResult Edit(EntidadesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private EntidadesModel EditModel(EntidadesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<Entidades>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<Entidades>().Modify(model.Entity); 
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
        public IActionResult Delete(EntidadesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private EntidadesModel DeleteModel(EntidadesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EntidadesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Entidades>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Entidades>().Remove(model.Entity); 
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

        private EntidadesModel NewModelDetail(long IdFather) 
        { 
            EntidadesModel model = new EntidadesModel(); 
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
        public IActionResult EditDetail(EntidadesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(EntidadesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private EntidadesModel DeleteModelDetail(EntidadesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EntidadesModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Entidades>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Entidades>().Remove(model.Entity); 
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
             Entidades entity = new Entidades(); 
             JsonConvert.PopulateObject(values, entity); 
             EntidadesModel model = new EntidadesModel(); 
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
             Entidades entity = Manager().GetBusinessLogic<Entidades>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             EntidadesModel model = new EntidadesModel(); 
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
             Entidades entity = Manager().GetBusinessLogic<Entidades>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Entidades>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetCiudadesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Ciudades>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEstadosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetTipoEntidadesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TipoEntidades>().Tabla(true), loadOptions);
        } 
        //[HttpPost]
        //public LoadResult GetTiposIdentificacionId(DataSourceLoadOptions loadOptions)
        //{ 
        //    return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposIdentificacion>().Tabla(true), loadOptions);
        //} 
        [HttpPost]
        public LoadResult GetTiposPersonasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposPersonas>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetEntidadesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Entidades>().Tabla(true), loadOptions);
        }
        #endregion

    }
}
