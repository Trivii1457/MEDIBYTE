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
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class EmpresasEsquemasImpuestosController : BaseAppController
    {

        //private const string Prefix = "EmpresasEsquemasImpuestos"; 

        public EmpresasEsquemasImpuestosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<EmpresasEsquemasImpuestos>().Tabla(true), loadOptions);
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

        private EmpresasEsquemasImpuestosModel NewModel() 
        { 
            EmpresasEsquemasImpuestosModel model = new EmpresasEsquemasImpuestosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private EmpresasEsquemasImpuestosModel EditModel(long Id) 
        { 
            EmpresasEsquemasImpuestosModel model = new EmpresasEsquemasImpuestosModel();
            model.Entity = Manager().GetBusinessLogic<EmpresasEsquemasImpuestos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(EmpresasEsquemasImpuestosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private EmpresasEsquemasImpuestosModel EditModel(EmpresasEsquemasImpuestosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<EmpresasEsquemasImpuestos>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<EmpresasEsquemasImpuestos>().Modify(model.Entity); 
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
        public IActionResult Delete(EmpresasEsquemasImpuestosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private EmpresasEsquemasImpuestosModel DeleteModel(EmpresasEsquemasImpuestosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EmpresasEsquemasImpuestosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<EmpresasEsquemasImpuestos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<EmpresasEsquemasImpuestos>().Remove(model.Entity); 
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

        private EmpresasEsquemasImpuestosModel NewModelDetail(long IdFather) 
        { 
            EmpresasEsquemasImpuestosModel model = new EmpresasEsquemasImpuestosModel(); 
            model.Entity.EmpresasId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(EmpresasEsquemasImpuestosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(EmpresasEsquemasImpuestosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private EmpresasEsquemasImpuestosModel DeleteModelDetail(EmpresasEsquemasImpuestosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EmpresasEsquemasImpuestosModel newModel = NewModelDetail(model.Entity.EmpresasId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<EmpresasEsquemasImpuestos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<EmpresasEsquemasImpuestos>().Remove(model.Entity); 
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
             EmpresasEsquemasImpuestos entity = new EmpresasEsquemasImpuestos(); 
             JsonConvert.PopulateObject(values, entity); 
             EmpresasEsquemasImpuestosModel model = new EmpresasEsquemasImpuestosModel(); 
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
             EmpresasEsquemasImpuestos entity = Manager().GetBusinessLogic<EmpresasEsquemasImpuestos>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             EmpresasEsquemasImpuestosModel model = new EmpresasEsquemasImpuestosModel(); 
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
             EmpresasEsquemasImpuestos entity = Manager().GetBusinessLogic<EmpresasEsquemasImpuestos>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<EmpresasEsquemasImpuestos>().Remove(entity); 
        } 

        
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetEmpresasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empresas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEsquemasImpuestosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<EsquemasImpuestos>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
