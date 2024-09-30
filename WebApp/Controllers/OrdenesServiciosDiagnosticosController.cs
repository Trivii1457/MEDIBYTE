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
    public partial class OrdenesServiciosDiagnosticosController : BaseAppController
    {

        //private const string Prefix = "OrdenesServiciosDiagnosticos"; 

        public OrdenesServiciosDiagnosticosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<OrdenesServiciosDiagnosticos>().Tabla(true), loadOptions);
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

        private OrdenesServiciosDiagnosticosModel NewModel() 
        { 
            OrdenesServiciosDiagnosticosModel model = new OrdenesServiciosDiagnosticosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private OrdenesServiciosDiagnosticosModel EditModel(long Id) 
        { 
            OrdenesServiciosDiagnosticosModel model = new OrdenesServiciosDiagnosticosModel();
            model.Entity = Manager().GetBusinessLogic<OrdenesServiciosDiagnosticos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(OrdenesServiciosDiagnosticosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private OrdenesServiciosDiagnosticosModel EditModel(OrdenesServiciosDiagnosticosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<OrdenesServiciosDiagnosticos>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<OrdenesServiciosDiagnosticos>().Modify(model.Entity); 
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
        public IActionResult Delete(OrdenesServiciosDiagnosticosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private OrdenesServiciosDiagnosticosModel DeleteModel(OrdenesServiciosDiagnosticosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            OrdenesServiciosDiagnosticosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<OrdenesServiciosDiagnosticos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<OrdenesServiciosDiagnosticos>().Remove(model.Entity); 
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

        private OrdenesServiciosDiagnosticosModel NewModelDetail(long IdFather) 
        { 
            OrdenesServiciosDiagnosticosModel model = new OrdenesServiciosDiagnosticosModel(); 
            model.Entity.OrdenesServiciosId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(OrdenesServiciosDiagnosticosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(OrdenesServiciosDiagnosticosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private OrdenesServiciosDiagnosticosModel DeleteModelDetail(OrdenesServiciosDiagnosticosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            OrdenesServiciosDiagnosticosModel newModel = NewModelDetail(model.Entity.OrdenesServiciosId.GetValueOrDefault()); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<OrdenesServiciosDiagnosticos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<OrdenesServiciosDiagnosticos>().Remove(model.Entity); 
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
             OrdenesServiciosDiagnosticos entity = new OrdenesServiciosDiagnosticos(); 
             JsonConvert.PopulateObject(values, entity); 
             OrdenesServiciosDiagnosticosModel model = new OrdenesServiciosDiagnosticosModel(); 
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
             OrdenesServiciosDiagnosticos entity = Manager().GetBusinessLogic<OrdenesServiciosDiagnosticos>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             OrdenesServiciosDiagnosticosModel model = new OrdenesServiciosDiagnosticosModel(); 
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
             OrdenesServiciosDiagnosticos entity = Manager().GetBusinessLogic<OrdenesServiciosDiagnosticos>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<OrdenesServiciosDiagnosticos>().Remove(entity); 
        } 

        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetDiagnosticosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Diagnosticos>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetOrdenesServiciosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<OrdenesServicios>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetTiposDiagnosticosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposDiagnosticos>().Tabla(true), loadOptions);
        }
        #endregion

    }
}
