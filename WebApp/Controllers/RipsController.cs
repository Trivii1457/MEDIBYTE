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
using Blazor.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class RipsController : BaseAppController
    {

        //private const string Prefix = "Rips"; 

        public RipsController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Rips>().Tabla(true), loadOptions);
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

        private RipsModel NewModel() 
        { 
            RipsModel model = new RipsModel();
            model.Entity.IsNew = true;
            model.Entity.FechaRemision = DateTime.Now;
            model.Entity.Periodo = DateTime.Now;
            model.Entity.EmpresasId = this.ActualEmpresaId();
            model.Entity.GenararCT = true;
            //model.Entity.GenerarAF = true;
            //model.Entity.GenerarUS = true;
            //model.Entity.GenerarAC = true;
            //model.Entity.GenerarAP = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private RipsModel EditModel(long Id) 
        { 
            RipsModel model = new RipsModel();
            model.Entity = Manager().GetBusinessLogic<Rips>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(RipsModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private RipsModel EditModel(RipsModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<Rips>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<Rips>().Modify(model.Entity); 
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
        public IActionResult Delete(RipsModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private RipsModel DeleteModel(RipsModel model)
        { 
            ViewBag.Accion = "Delete"; 
            RipsModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Rips>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Rips>().Remove(model.Entity); 
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

        private RipsModel NewModelDetail(long IdFather) 
        { 
            RipsModel model = new RipsModel(); 
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
        public IActionResult EditDetail(RipsModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(RipsModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private RipsModel DeleteModelDetail(RipsModel model)
        { 
            ViewBag.Accion = "Delete"; 
            RipsModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Rips>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Rips>().Remove(model.Entity); 
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
             Rips entity = new Rips(); 
             JsonConvert.PopulateObject(values, entity); 
             RipsModel model = new RipsModel(); 
             model.Entity = entity; 
             model.Entity.IsNew = true; 
             this.EditModel(model); 
             if(ModelState.IsValid) 
                 return Ok(ModelState); 
             else 
                 return BadRequest(ModelState); 
        } 

        [HttpPost] 
        public IActionResult ModifyInGrid(int key, string values) 
        { 
             Rips entity = Manager().GetBusinessLogic<Rips>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             RipsModel model = new RipsModel(); 
             model.Entity = entity; 
             model.Entity.IsNew = false; 
             this.EditModel(model); 
             if(ModelState.IsValid) 
                 return Ok(ModelState); 
             else 
                 return BadRequest(ModelState); 
        } 

        [HttpPost]
        public void DeleteInGrid(int key)
        { 
             Rips entity = Manager().GetBusinessLogic<Rips>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Rips>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetEntidadesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Entidades>().Tabla(true), loadOptions);
        } 
       #endregion

        public IActionResult GenerarArchivos(long id)
        {
            ArchivoDescargaModel result = Manager().RipsBusinessLogic().GenerarArchivos(id);
            return File(result.Archivo, result.ContentType, result.Nombre);
        }

    }
}
