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
    public partial class SedesDocumentosController : BaseAppController
    {

        //private const string Prefix = "SedesDocumentos"; 

        public SedesDocumentosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<SedesDocumentos>().Tabla(true), loadOptions);
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

        private SedesDocumentosModel NewModel() 
        { 
            SedesDocumentosModel model = new SedesDocumentosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private SedesDocumentosModel EditModel(long Id) 
        { 
            SedesDocumentosModel model = new SedesDocumentosModel();
            model.Entity = Manager().GetBusinessLogic<SedesDocumentos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(SedesDocumentosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private SedesDocumentosModel EditModel(SedesDocumentosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<SedesDocumentos>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<SedesDocumentos>().Modify(model.Entity); 
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
        public IActionResult Delete(SedesDocumentosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private SedesDocumentosModel DeleteModel(SedesDocumentosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            SedesDocumentosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<SedesDocumentos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<SedesDocumentos>().Remove(model.Entity); 
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

        private SedesDocumentosModel NewModelDetail(long IdFather) 
        { 
            SedesDocumentosModel model = new SedesDocumentosModel(); 
            model.Entity.DocumentosId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(SedesDocumentosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(SedesDocumentosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private SedesDocumentosModel DeleteModelDetail(SedesDocumentosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            SedesDocumentosModel newModel = NewModelDetail(model.Entity.DocumentosId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<SedesDocumentos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<SedesDocumentos>().Remove(model.Entity); 
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
             SedesDocumentos entity = new SedesDocumentos(); 
             JsonConvert.PopulateObject(values, entity); 
             SedesDocumentosModel model = new SedesDocumentosModel(); 
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
             SedesDocumentos entity = Manager().GetBusinessLogic<SedesDocumentos>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             SedesDocumentosModel model = new SedesDocumentosModel(); 
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
             SedesDocumentos entity = Manager().GetBusinessLogic<SedesDocumentos>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<SedesDocumentos>().Remove(entity); 
        } 

        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetDocumentosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Documentos>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetSedesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Sedes>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
