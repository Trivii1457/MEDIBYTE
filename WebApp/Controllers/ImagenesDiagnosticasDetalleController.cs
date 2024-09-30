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
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Blazor.BusinessLogic.Models;
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class ImagenesDiagnosticasDetalleController : BaseAppController
    {

        //private const string Prefix = "ImagenesDiagnosticasDetalle"; 

        public ImagenesDiagnosticasDetalleController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().Tabla(true), loadOptions);
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

        private ImagenesDiagnosticasDetalleModel NewModel() 
        { 
            ImagenesDiagnosticasDetalleModel model = new ImagenesDiagnosticasDetalleModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private ImagenesDiagnosticasDetalleModel EditModel(long Id) 
        { 
            ImagenesDiagnosticasDetalleModel model = new ImagenesDiagnosticasDetalleModel();
            model.Entity = Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(ImagenesDiagnosticasDetalleModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private ImagenesDiagnosticasDetalleModel EditModel(ImagenesDiagnosticasDetalleModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().Modify(model.Entity); 
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
        public IActionResult Delete(ImagenesDiagnosticasDetalleModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private ImagenesDiagnosticasDetalleModel DeleteModel(ImagenesDiagnosticasDetalleModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ImagenesDiagnosticasDetalleModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().Remove(model.Entity); 
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

        private ImagenesDiagnosticasDetalleModel NewModelDetail(long IdFather) 
        { 
            ImagenesDiagnosticasDetalleModel model = new ImagenesDiagnosticasDetalleModel(); 
            model.Entity.ImagenesDiagnosticasId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(ImagenesDiagnosticasDetalleModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(ImagenesDiagnosticasDetalleModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private ImagenesDiagnosticasDetalleModel DeleteModelDetail(ImagenesDiagnosticasDetalleModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ImagenesDiagnosticasDetalleModel newModel = NewModelDetail(model.Entity.ImagenesDiagnosticasId.GetValueOrDefault()); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().Remove(model.Entity); 
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
             ImagenesDiagnosticasDetalle entity = new ImagenesDiagnosticasDetalle(); 
             JsonConvert.PopulateObject(values, entity); 
             ImagenesDiagnosticasDetalleModel model = new ImagenesDiagnosticasDetalleModel(); 
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
             ImagenesDiagnosticasDetalle entity = Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             ImagenesDiagnosticasDetalleModel model = new ImagenesDiagnosticasDetalleModel(); 
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
             ImagenesDiagnosticasDetalle entity = Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().Remove(entity); 
        } 

        
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetImagenesDiagnosticasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ImagenesDiagnosticas>().Tabla(true), loadOptions);
        }
        #endregion

        [HttpPost]
        [RequestSizeLimit(int.MaxValue)]
        public ActionResult CargarImagenDiagnostica(long imagenesDiagnosticasId)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                IFormFile myFile = Request.Form.Files["ArchivoImagenDiagnostica"];
                AzureFileModel azureFileModel = new AzureFileModel();
                azureFileModel.ContentType = myFile.ContentType;
                azureFileModel.UsuarioCreador = User.Identity.Name;
                azureFileModel.Peso = myFile.Length;
                using (var ms = new MemoryStream())
                {
                    myFile.CopyTo(ms);
                    azureFileModel.Archivo = ms.ToArray();
                }
                var Errores = Manager().ImagenesDiagnosticasDetalleBusinessLogic().CargarImagenDiagnostica(azureFileModel, imagenesDiagnosticasId);

                if (Errores.Count > 0)
                {
                    Response.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Add("Errores", Errores);
                    result.Add("TieneErrores", true);
                    return new BadRequestObjectResult(result);
                }

                result.Add("TieneErrores", false);
                return new OkObjectResult(result);
            }
            catch(Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                result.Add("TieneErrores", true);
                result.Add("Errores", new List<string> { "Error en subir imagen diagnostica. | " + e.Message });
                return new BadRequestObjectResult(result);
            }

        }

        [HttpPost]
        [RequestSizeLimit(int.MaxValue)]
        public ActionResult CargarImagenDiagnosticaDesdeAdmision(long admisionesServiciosPrestadosId)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                IFormFile myFile = Request.Form.Files["ArchivoImagenDiagnostica"];
                AzureFileModel azureFileModel = new AzureFileModel();
                azureFileModel.ContentType = myFile.ContentType;
                azureFileModel.UsuarioCreador = User.Identity.Name;
                azureFileModel.Peso = myFile.Length;
                using (var ms = new MemoryStream())
                {
                    myFile.CopyTo(ms);
                    azureFileModel.Archivo = ms.ToArray();
                }
                var Errores = Manager().ImagenesDiagnosticasDetalleBusinessLogic().CargarImagenDiagnosticaDesdeAdmisionServicioPrestado(azureFileModel, admisionesServiciosPrestadosId);

                if (Errores.Count > 0)
                {
                    Response.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Add("Errores", Errores);
                    result.Add("TieneErrores", true);
                    return new BadRequestObjectResult(result);
                }

                result.Add("TieneErrores", false);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                result.Add("TieneErrores", true);
                result.Add("Errores", new List<string> { "Error en subir imagen diagnostica. | " + e.Message });
                return new BadRequestObjectResult(result);
            }
        }

        [HttpGet]
        public ActionResult DescargarImagenDiagnostica(long id)
        {
            AzureFileModel azureFileModel = Manager().ImagenesDiagnosticasDetalleBusinessLogic().DescargarImagenDiagnostica(id);
            if (string.IsNullOrWhiteSpace(azureFileModel.ContentType) || azureFileModel.Archivo.Length <= 0 || azureFileModel.Archivo == null)
            {
                return new BadRequestObjectResult("Error obteniendo archivo.");
            }
            else
                return File(azureFileModel.Archivo, azureFileModel.ContentType, azureFileModel.Nombre);
        }

        [HttpGet]
        public ActionResult EliminarImagenDiagnostica(long id)
        {
            try
            {
                Manager().ImagenesDiagnosticasDetalleBusinessLogic().EliminarImagenDiagnostica(id);
                return Ok();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
            
        }

    }
}
