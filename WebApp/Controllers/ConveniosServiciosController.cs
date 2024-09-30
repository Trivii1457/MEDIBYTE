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
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class ConveniosServiciosController : BaseAppController
    {

        //private const string Prefix = "ConveniosServicios"; 

        public ConveniosServiciosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ConveniosServicios>().Tabla(true).Include(x=>x.Servicios.Cups), loadOptions);
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

        private ConveniosServiciosModel NewModel() 
        { 
            ConveniosServiciosModel model = new ConveniosServiciosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private ConveniosServiciosModel EditModel(long Id) 
        { 
            ConveniosServiciosModel model = new ConveniosServiciosModel();
            model.Entity = Manager().GetBusinessLogic<ConveniosServicios>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(ConveniosServiciosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private ConveniosServiciosModel EditModel(ConveniosServiciosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<ConveniosServicios>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<ConveniosServicios>().Modify(model.Entity); 
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
        public IActionResult Delete(ConveniosServiciosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private ConveniosServiciosModel DeleteModel(ConveniosServiciosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ConveniosServiciosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ConveniosServicios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ConveniosServicios>().Remove(model.Entity); 
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

        private ConveniosServiciosModel NewModelDetail(long IdFather) 
        { 
            ConveniosServiciosModel model = new ConveniosServiciosModel(); 
            model.Entity.ConveniosId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(ConveniosServiciosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(ConveniosServiciosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private ConveniosServiciosModel DeleteModelDetail(ConveniosServiciosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ConveniosServiciosModel newModel = NewModelDetail(model.Entity.ConveniosId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ConveniosServicios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ConveniosServicios>().Remove(model.Entity); 
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

        //#region Funcions Detail Edit in Grid 

        //[HttpPost] 
        //public IActionResult AddInGrid(string values) 
        //{ 
        //     ConveniosServicios entity = new ConveniosServicios(); 
        //     JsonConvert.PopulateObject(values, entity); 
        //     ConveniosServiciosModel model = new ConveniosServiciosModel(); 
        //     model.Entity = entity; 
        //     model.Entity.IsNew = true; 
        //     this.EditModel(model); 
        //     if(ModelState.IsValid) 
        //         return Ok(ModelState); 
        //     else 
        //         return BadRequest(ModelState.GetFullErrorMessage()); 
        //} 

        //[HttpPost] 
        //public IActionResult ModifyInGrid(int key, string values) 
        //{ 
        //     ConveniosServicios entity = Manager().GetBusinessLogic<ConveniosServicios>().FindById(x => x.Id == key, false); 
        //     JsonConvert.PopulateObject(values, entity); 
        //     ConveniosServiciosModel model = new ConveniosServiciosModel(); 
        //     model.Entity = entity; 
        //     model.Entity.IsNew = false; 
        //     this.EditModel(model); 
        //     if(ModelState.IsValid) 
        //         return Ok(ModelState); 
        //     else 
        //         return BadRequest(ModelState.GetFullErrorMessage()); 
        //} 

        //[HttpPost]
        //public void DeleteInGrid(int key)
        //{ 
        //     ConveniosServicios entity = Manager().GetBusinessLogic<ConveniosServicios>().FindById(x => x.Id == key, false); 
        //     Manager().GetBusinessLogic<ConveniosServicios>().Remove(entity); 
        //} 

        
        //#endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetConveniosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Convenios>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetServiciosId(DataSourceLoadOptions loadOptions)
        {
            List<string> codes = new List<string> { "COP", "CM", "CR", "PC" };
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Servicios>().Tabla(true).Where(x=>x.EstadosId == 30 && !codes.Contains(x.Codigo)), loadOptions);
        }
        #endregion


        [HttpGet]
        public IActionResult DescargarPlantilla()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "wwwroot", "Files", "Planos", "CONVENIOS_SERVICIOS.xlsx"));
            return File(fileBytes, "application/octet-stream", $"PlantillaConvenioServicios_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx");
        }

        [HttpPost]
        [RequestSizeLimit(int.MaxValue)]
        public ActionResult CargarPlantillaDatos(bool modificaRegistros, long idConvenio)
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
                var pathFileError = Manager().ConveniosServiciosBusinessLogic().CargarDatosPlantilla(ms, modificaRegistros, User.Identity.Name, idConvenio);
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
            return File(fileBytes, "application/octet-stream", $"ErroresPlantillaConvenioServicios.txt");
        }
    }
}
