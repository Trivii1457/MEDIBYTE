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

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class ServiciosConsultoriosController : BaseAppController
    {

        //private const string Prefix = "ServiciosConsultorios"; 

        public ServiciosConsultoriosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ServiciosConsultorios>().Tabla(true), loadOptions);
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

        private ServiciosConsultoriosModel NewModel()
        {
            ServiciosConsultoriosModel model = new ServiciosConsultoriosModel();
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private ServiciosConsultoriosModel EditModel(long Id)
        {
            ServiciosConsultoriosModel model = new ServiciosConsultoriosModel();
            model.Entity = Manager().GetBusinessLogic<ServiciosConsultorios>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model;
        }

        [HttpPost]
        public IActionResult Edit(ServiciosConsultoriosModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        private ServiciosConsultoriosModel EditModel(ServiciosConsultoriosModel model)
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
                        model.Entity = Manager().GetBusinessLogic<ServiciosConsultorios>().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        model.Entity = Manager().GetBusinessLogic<ServiciosConsultorios>().Modify(model.Entity);
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
        public IActionResult Delete(ServiciosConsultoriosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private ServiciosConsultoriosModel DeleteModel(ServiciosConsultoriosModel model)
        {
            ViewBag.Accion = "Delete";
            ServiciosConsultoriosModel newModel = NewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<ServiciosConsultorios>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<ServiciosConsultorios>().Remove(model.Entity);
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

        private ServiciosConsultoriosModel NewModelDetail(long IdFather)
        {
            ServiciosConsultoriosModel model = new ServiciosConsultoriosModel();
            model.Entity.ConsultoriosId = IdFather;
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(ServiciosConsultoriosModel model)
        {
            return PartialView("EditDetail", EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(ServiciosConsultoriosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private ServiciosConsultoriosModel DeleteModelDetail(ServiciosConsultoriosModel model)
        {
            ViewBag.Accion = "Delete";
            ServiciosConsultoriosModel newModel = NewModelDetail(model.Entity.ConsultoriosId);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<ServiciosConsultorios>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<ServiciosConsultorios>().Remove(model.Entity);
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
            ServiciosConsultorios entity = new ServiciosConsultorios();
            JsonConvert.PopulateObject(values, entity);
            ServiciosConsultoriosModel model = new ServiciosConsultoriosModel();
            model.Entity = entity;
            model.Entity.IsNew = true;
            this.EditModel(model);
            if (ModelState.IsValid)
                return Ok(ModelState);
            else
                return BadRequest(ModelState.GetFullErrorMessage());
        }

        [HttpPost]
        public IActionResult ModifyInGrid(int key, string values)
        {
            ServiciosConsultorios entity = Manager().GetBusinessLogic<ServiciosConsultorios>().FindById(x => x.Id == key, false);
            JsonConvert.PopulateObject(values, entity);
            ServiciosConsultoriosModel model = new ServiciosConsultoriosModel();
            model.Entity = entity;
            model.Entity.IsNew = false;
            this.EditModel(model);
            if (ModelState.IsValid)
                return Ok(ModelState);
            else
                return BadRequest(ModelState.GetFullErrorMessage());
        }

        [HttpPost]
        public void DeleteInGrid(int key)
        {
            ServiciosConsultorios entity = Manager().GetBusinessLogic<ServiciosConsultorios>().FindById(x => x.Id == key, false);
            Manager().GetBusinessLogic<ServiciosConsultorios>().Remove(entity);
        }

        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetConsultoriosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Consultorios>().Tabla(true), loadOptions);
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
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "wwwroot", "Files", "Planos", "CONSULTORIOS_SERVICIOS.xlsx"));
            return File(fileBytes, "application/octet-stream", $"PlantillaConsultoriosServicios_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx");
        }

        [HttpPost]
        [RequestSizeLimit(int.MaxValue)]
        public ActionResult CargarPlantillaDatos(bool modificaRegistros, long idConsultorio)
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
                var pathFileError = Manager().ServiciosConsultoriosBusinessLogic().CargarDatosPlantilla(ms, modificaRegistros, User.Identity.Name, idConsultorio);
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
            return File(fileBytes, "application/octet-stream", $"ErroresPlantillaConsultoriosServicios.txt");
        }

    }
}
