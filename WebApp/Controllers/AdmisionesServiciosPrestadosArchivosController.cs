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
using System.Collections.Generic;
using Blazor.BusinessLogic.Models;
using System.IO;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class AdmisionesServiciosPrestadosArchivosController : BaseAppController
    {

        //private const string Prefix = "AdmisionesServiciosPrestadosArchivos"; 

        public AdmisionesServiciosPrestadosArchivosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().Tabla(true), loadOptions);
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

        private AdmisionesServiciosPrestadosArchivosModel NewModel() 
        { 
            AdmisionesServiciosPrestadosArchivosModel model = new AdmisionesServiciosPrestadosArchivosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private AdmisionesServiciosPrestadosArchivosModel EditModel(long Id) 
        { 
            AdmisionesServiciosPrestadosArchivosModel model = new AdmisionesServiciosPrestadosArchivosModel();
            model.Entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(AdmisionesServiciosPrestadosArchivosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private AdmisionesServiciosPrestadosArchivosModel EditModel(AdmisionesServiciosPrestadosArchivosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().Modify(model.Entity); 
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
        public IActionResult Delete(AdmisionesServiciosPrestadosArchivosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private AdmisionesServiciosPrestadosArchivosModel DeleteModel(AdmisionesServiciosPrestadosArchivosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AdmisionesServiciosPrestadosArchivosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().Remove(model.Entity); 
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

        private AdmisionesServiciosPrestadosArchivosModel NewModelDetail(long IdFather) 
        { 
            AdmisionesServiciosPrestadosArchivosModel model = new AdmisionesServiciosPrestadosArchivosModel(); 
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
        public IActionResult EditDetail(AdmisionesServiciosPrestadosArchivosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(AdmisionesServiciosPrestadosArchivosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private AdmisionesServiciosPrestadosArchivosModel DeleteModelDetail(AdmisionesServiciosPrestadosArchivosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AdmisionesServiciosPrestadosArchivosModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().Remove(model.Entity); 
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
             AdmisionesServiciosPrestadosArchivos entity = new AdmisionesServiciosPrestadosArchivos(); 
             JsonConvert.PopulateObject(values, entity); 
             AdmisionesServiciosPrestadosArchivosModel model = new AdmisionesServiciosPrestadosArchivosModel(); 
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
             AdmisionesServiciosPrestadosArchivos entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             AdmisionesServiciosPrestadosArchivosModel model = new AdmisionesServiciosPrestadosArchivosModel(); 
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
             AdmisionesServiciosPrestadosArchivos entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetAdmisionesServiciosPrestadosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().Tabla(true), loadOptions);
        }
        #endregion

        [HttpPost]
        [RequestSizeLimit(int.MaxValue)]
        public ActionResult CargarArchivoResultado(long admisionesServiciosPrestadosId)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                IFormFile myFile = Request.Form.Files["ArchivoResultado"];
                AdmisionesServiciosPrestadosArchivos admisionesServiciosPrestadosArchivos = new AdmisionesServiciosPrestadosArchivos();
                admisionesServiciosPrestadosArchivos.AdmisionesServiciosPrestadosId = admisionesServiciosPrestadosId;
                admisionesServiciosPrestadosArchivos.Nombre = myFile.FileName.Substring(myFile.FileName.Length > 100 ? myFile.FileName.Length - 99 : 0);
                admisionesServiciosPrestadosArchivos.TipoContenido = myFile.ContentType;
                admisionesServiciosPrestadosArchivos.UpdatedBy = User.Identity.Name;
                admisionesServiciosPrestadosArchivos.CreatedBy = User.Identity.Name;
                admisionesServiciosPrestadosArchivos.CreationDate = DateTime.Now;
                admisionesServiciosPrestadosArchivos.LastUpdate = DateTime.Now;
                using (var ms = new MemoryStream())
                {
                    myFile.CopyTo(ms);
                    admisionesServiciosPrestadosArchivos.ResultadoArchivo = ms.ToArray();
                }

                Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().Add(admisionesServiciosPrestadosArchivos);

                result.Add("TieneErrores", false);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                result.Add("TieneErrores", true);
                result.Add("Errores", new List<string> { "Error en subir imagen diagnostica a la cuenta de alamcenamiento de azure. | " + e.GetFullErrorMessage() });
                return new BadRequestObjectResult(result);
            }
        }

        [HttpGet]
        public IActionResult DescargarArchivoResultado(long id)
        {
            try
            {
                AdmisionesServiciosPrestadosArchivos admisionesServiciosPrestadosArchivos = Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().FindById(x => x.Id == id, false);
                if (admisionesServiciosPrestadosArchivos != null &&
                    admisionesServiciosPrestadosArchivos.ResultadoArchivo.Length > 0 &&
                    !string.IsNullOrWhiteSpace(admisionesServiciosPrestadosArchivos.TipoContenido) &&
                    !string.IsNullOrWhiteSpace(admisionesServiciosPrestadosArchivos.Nombre)
                    )
                {
                    return File(admisionesServiciosPrestadosArchivos.ResultadoArchivo, admisionesServiciosPrestadosArchivos.TipoContenido, admisionesServiciosPrestadosArchivos.Nombre);
                }
                else
                {
                    return new BadRequestObjectResult("Error obteniendo archivo de resultado. Verificar Base de datos.");
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error en servidor. " + e.GetFullErrorMessage());
            }
            
        }

        [HttpPost]
        public IActionResult EliminarArchivoResultado(long id)
        {
            try
            {
                AdmisionesServiciosPrestadosArchivos admisionesServiciosPrestadosArchivos = Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().FindById(x => x.Id == id, false);
                if (admisionesServiciosPrestadosArchivos != null)
                {
                    Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().Remove(admisionesServiciosPrestadosArchivos);
                    return Ok();
                }
                else
                {
                    return new BadRequestObjectResult("Error eliminando archivo de resultado. Verificar Base de datos.");
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error en servidor. " + e.GetFullErrorMessage());
            }
        }

    }
}
