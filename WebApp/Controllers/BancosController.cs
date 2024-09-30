using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class BancosController : BaseAppController
    {

        //private const string Prefix = "Bancos"; 

        public BancosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Bancos>().Tabla(true), loadOptions);
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

        private BancosModel NewModel()
        {
            BancosModel model = new BancosModel();
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private BancosModel EditModel(long Id)
        {
            BancosModel model = new BancosModel();
            model.Entity = Manager().GetBusinessLogic<Bancos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model;
        }

        [HttpPost]
        public IActionResult Edit(BancosModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        private BancosModel EditModel(BancosModel model)
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
                        model.Entity = Manager().GetBusinessLogic<Bancos>().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        model.Entity = Manager().GetBusinessLogic<Bancos>().Modify(model.Entity);
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
        public IActionResult Delete(BancosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private BancosModel DeleteModel(BancosModel model)
        {
            ViewBag.Accion = "Delete";
            BancosModel newModel = NewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<Bancos>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<Bancos>().Remove(model.Entity);
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

        private BancosModel NewModelDetail(long IdFather) 
        { 
            BancosModel model = new BancosModel(); 
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
        public IActionResult EditDetail(BancosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(BancosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private BancosModel DeleteModelDetail(BancosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            BancosModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Bancos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Bancos>().Remove(model.Entity); 
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
             Bancos entity = new Bancos(); 
             JsonConvert.PopulateObject(values, entity); 
             BancosModel model = new BancosModel(); 
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
             Bancos entity = Manager().GetBusinessLogic<Bancos>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             BancosModel model = new BancosModel(); 
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
             Bancos entity = Manager().GetBusinessLogic<Bancos>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Bancos>().Remove(entity); 
        } 

        */
        #endregion

        [HttpGet]
        public IActionResult DescargarPlantilla()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "wwwroot", "Files", "Planos", "BANCOS.xlsx"));
            return File(fileBytes, "application/octet-stream", $"PlantillaBancos_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx");
        }

        [HttpPost]
        [RequestSizeLimit(int.MaxValue)]
        public ActionResult CargarPlantillaDatos(bool modificaRegistros)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                IFormFile myFile = Request.Form.Files["ArchivoPlantillaDatos"];
                if (myFile == null && myFile.Length <= 0)
                {
                    result.Add("TieneErrores", true);
                    result.Add("Errores", new List<string> { $"El archivo cargado esta dañado o corrupto." });
                    return new BadRequestObjectResult(result);
                }

                var ms = new MemoryStream();
                myFile.CopyTo(ms);
                var pathFileError = Manager().BancosBusinessLogic().CargarDatosPlantilla(ms, modificaRegistros, User.Identity.Name);
                if (!string.IsNullOrEmpty(pathFileError))
                {
                    result.Add("pathFileError", pathFileError);
                }
                result.Add("TieneErrores", false);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                result.Add("TieneErrores", true);
                result.Add("Errores", new List<string> { "Error en leer la plantilla de cargue. | " + e.GetFullErrorMessage() });
                return new BadRequestObjectResult(result);
            }
        }

        [HttpGet]
        public IActionResult DescargarArchivoErrores(string path)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application/octet-stream", $"ErroresPlantillaBancos.txt");
        }

    }
}
