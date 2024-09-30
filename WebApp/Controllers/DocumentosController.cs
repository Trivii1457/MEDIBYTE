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
using Dominus.Backend.Data;
using System.Collections.Generic;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class DocumentosController : BaseAppController
    {

        //private const string Prefix = "Documentos"; 

        public DocumentosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Documentos>().Tabla(true), loadOptions);
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

        private DocumentosModel NewModel()
        {
            DocumentosModel model = new DocumentosModel();
            model.Entity.IsNew = true;
            model.Entity.Activo = true;
            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private DocumentosModel EditModel(long Id)
        {
            DocumentosModel model = new DocumentosModel();
            model.Entity = Manager().GetBusinessLogic<Documentos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model;
        }

        [HttpPost]
        public IActionResult Edit(DocumentosModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        private DocumentosModel EditModel(DocumentosModel model)
        {
            ViewBag.Accion = "Save";
            var OnState = model.Entity.IsNew;

            if (model.Entity.ResolucionDian != null)
            {
                if (model.Entity.ConsecutivoDesde == null || model.Entity.ConsecutivoDesde == 0 ||
                    model.Entity.ConsecutivoHasta == null || model.Entity.ConsecutivoHasta == 0 ||
                    model.Entity.FechaDesde == null || model.Entity.FechaHasta == null
                    )
                    ModelState.AddModelError("Entity.Id", "Los campos consecutivos y fechas son obligatorios si se escribe una resolucion de la DIAN.");
            }

            if (model.Entity.FechaHasta < model.Entity.FechaDesde)
            {
                ModelState.AddModelError("Entity.Id", "La Fecha Hasta debe ser superior a la Fecha Desde.");
            }

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
                        model.Entity = Manager().GetBusinessLogic<Documentos>().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        model.Entity = Manager().GetBusinessLogic<Documentos>().Modify(model.Entity);
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
        public IActionResult Delete(DocumentosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private DocumentosModel DeleteModel(DocumentosModel model)
        {
            ViewBag.Accion = "Delete";
            DocumentosModel newModel = NewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<Documentos>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<Documentos>().Remove(model.Entity);
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

        private DocumentosModel NewModelDetail(long IdFather) 
        { 
            DocumentosModel model = new DocumentosModel(); 
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
        public IActionResult EditDetail(DocumentosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(DocumentosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private DocumentosModel DeleteModelDetail(DocumentosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            DocumentosModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Documentos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Documentos>().Remove(model.Entity); 
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
             Documentos entity = new Documentos(); 
             JsonConvert.PopulateObject(values, entity); 
             DocumentosModel model = new DocumentosModel(); 
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
             Documentos entity = Manager().GetBusinessLogic<Documentos>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             DocumentosModel model = new DocumentosModel(); 
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
             Documentos entity = Manager().GetBusinessLogic<Documentos>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Documentos>().Remove(entity); 
        } 

        */
        #endregion

        [HttpPost]
        public LoadResult GetTransaccion(DataSourceLoadOptions loadOptions)
        {
            List<KeyValue> transacciones = new List<KeyValue>()
            {
                new KeyValue{ Key = 0, Value = "Facturas"},
                new KeyValue{ Key = 3, Value = "Nota credito"},
                new KeyValue{ Key = 4, Value = "Nota debito"},
            };

            return DataSourceLoader.Load(transacciones, loadOptions);
        }

    }
}
