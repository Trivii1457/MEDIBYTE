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
using System.IO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class PreciosServiciosController : BaseAppController
    {

        //private const string Prefix = "PreciosServicios"; 

        public PreciosServiciosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<PreciosServicios>().Tabla(true).Include(x=>x.Servicios.Cups), loadOptions);
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

        private PreciosServiciosModel NewModel() 
        { 
            PreciosServiciosModel model = new PreciosServiciosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private PreciosServiciosModel EditModel(long Id) 
        { 
            PreciosServiciosModel model = new PreciosServiciosModel();
            model.Entity = Manager().GetBusinessLogic<PreciosServicios>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(PreciosServiciosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private PreciosServiciosModel EditModel(PreciosServiciosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<PreciosServicios>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<PreciosServicios>().Modify(model.Entity); 
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
        public IActionResult Delete(PreciosServiciosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private PreciosServiciosModel DeleteModel(PreciosServiciosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            PreciosServiciosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<PreciosServicios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<PreciosServicios>().Remove(model.Entity); 
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

        private PreciosServiciosModel NewModelDetail(long IdFather) 
        { 
            PreciosServiciosModel model = new PreciosServiciosModel(); 
            model.Entity.ListaPreciosId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(PreciosServiciosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(PreciosServiciosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private PreciosServiciosModel DeleteModelDetail(PreciosServiciosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            PreciosServiciosModel newModel = NewModelDetail(model.Entity.ListaPreciosId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<PreciosServicios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<PreciosServicios>().Remove(model.Entity); 
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
             PreciosServicios entity = new PreciosServicios(); 
             JsonConvert.PopulateObject(values, entity); 
             PreciosServiciosModel model = new PreciosServiciosModel(); 
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
             PreciosServicios entity = Manager().GetBusinessLogic<PreciosServicios>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             PreciosServiciosModel model = new PreciosServiciosModel(); 
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
             PreciosServicios entity = Manager().GetBusinessLogic<PreciosServicios>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<PreciosServicios>().Remove(entity); 
        } 

        
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetListaPreciosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ListaPrecios>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetServiciosId(DataSourceLoadOptions loadOptions)
        {
            List<string> codes = new List<string> { "COP", "CM", "CR", "PC" };
            var result = Manager().GetBusinessLogic<Servicios>().Tabla(false).Where(x=>x.EstadosId == 30 && !codes.Contains(x.Codigo));
            return DataSourceLoader.Load(result, loadOptions);
        }
        #endregion

        [HttpGet]
        public IActionResult DescargarPlantilla()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "wwwroot", "Files", "Planos", "LISTAPRECIOS_SERVICIOS.xlsx"));
            return File(fileBytes, "application/octet-stream", $"PlantillaListaPreciosServicios_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx");
        }

        [HttpPost]
        [RequestSizeLimit(int.MaxValue)]
        public ActionResult CargarPlantillaDatos(bool modificaRegistros,long idListaPrecios)
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
                var pathFileError = Manager().PreciosServiciosLogic().CargarDatosPlantilla(ms, modificaRegistros, User.Identity.Name, idListaPrecios);
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
            return File(fileBytes, "application/octet-stream", $"ErroresPlantillaListaPrecioServicios.txt");
        }

    }
}
