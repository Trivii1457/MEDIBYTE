using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Backend.Application;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class PacientesController : BaseAppController
    {

        //private const string Prefix = "Pacientes"; 

        public PacientesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Pacientes>().Tabla(true), loadOptions);
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

        private PacientesModel NewModel() 
        { 
            PacientesModel model = new PacientesModel();
            model.Entity.IsNew = true;
            model.Entity.EmpresasId = this.ActualEmpresaId();
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private PacientesModel EditModel(long Id) 
        { 
            PacientesModel model = new PacientesModel();
            model.Entity = Manager().GetBusinessLogic<Pacientes>().FindById(x => x.Id == Id, true);
            if (model.Entity.CiudadesId > 0)
            {
                Ciudades ciudad = Manager().GetBusinessLogic<Ciudades>().FindById(x => x.Id == model.Entity.CiudadesId, true);
                model.Entity.DepartamentoId = ciudad.DepartamentosId;
                model.Entity.PaisId = ciudad.Departamentos.PaisesId;
                model.Entity.IsNew = false;
            }
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(PacientesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private PacientesModel EditModel(PacientesModel model) 
        { 
            ViewBag.Accion = "Save"; 
            var OnState = model.Entity.IsNew;

            if (model.Entity.FechaNacimiento > DateTime.Now.AddYears(-18))
            {
                List<string> codigos = new List<string>() { "NI", "CC", "CE" };
                var resultCodigos = Manager().GetBusinessLogic<TiposIdentificacion>().Tabla(true).Where(x => codigos.Contains(x.Codigo)).ToList();
                bool existeTipo = resultCodigos.Any(x => x.Id == model.Entity.TiposIdentificacionId);
                if (existeTipo)
                {
                    ModelState.AddModelError("Entity.TiposIdentificacionId", DApp.DefaultLanguage.GetResource("Pacientes.TipoIdentificacionMenorEdad"));
                }
            }
            else
            {
                List<string> codigos = new List<string>() { "TI", "RC", "MS", "CN" };
                var resultCodigos = Manager().GetBusinessLogic<TiposIdentificacion>().Tabla(true).Where(x => codigos.Contains(x.Codigo)).ToList();
                bool existeTipo = resultCodigos.Any(x => x.Id == model.Entity.TiposIdentificacionId);
                if (existeTipo)
                {
                    ModelState.AddModelError("Entity.TiposIdentificacionId", DApp.DefaultLanguage.GetResource("Pacientes.TipoIdentificacionMayorEdad"));
                }
            }

            ModelState.Remove("Entity.NombreCompleto");
            if (ModelState.IsValid) 
            { 
                try 
                {
                    model.Entity.LastUpdate = DateTime.Now; 
                    model.Entity.UpdatedBy = User.Identity.Name;
                    model.Entity.NombreCompleto = $"{model.Entity.PrimerNombre} {model.Entity.SegundoNombre} {model.Entity.PrimerApellido} {model.Entity.SegundoApellido}";
                    if (model.Entity.IsNew) 
                    { 
                        model.Entity.CreationDate = DateTime.Now; 
                        model.Entity.CreatedBy = User.Identity.Name;
                        model.Entity = Manager().GetBusinessLogic<Pacientes>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    {
                        model.Entity = Manager().GetBusinessLogic<Pacientes>().Modify(model.Entity); 
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
        public IActionResult Delete(PacientesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private PacientesModel DeleteModel(PacientesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            PacientesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Pacientes>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Pacientes>().Remove(model.Entity); 
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

        private PacientesModel NewModelDetail(long IdFather) 
        { 
            PacientesModel model = new PacientesModel(); 
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
        public IActionResult EditDetail(PacientesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(PacientesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private PacientesModel DeleteModelDetail(PacientesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            PacientesModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Pacientes>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Pacientes>().Remove(model.Entity); 
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
             Pacientes entity = new Pacientes(); 
             JsonConvert.PopulateObject(values, entity); 
             PacientesModel model = new PacientesModel(); 
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
             Pacientes entity = Manager().GetBusinessLogic<Pacientes>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             PacientesModel model = new PacientesModel(); 
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
             Pacientes entity = Manager().GetBusinessLogic<Pacientes>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Pacientes>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetCiudadesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Ciudades>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEntidadesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Entidades>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetSedesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Sedes>().Tabla(true).Where(x => x.EmpresasId == this.ActualEmpresaId()), loadOptions);
        }
        [HttpPost]
        public LoadResult GetTiposAfiliadosId(DataSourceLoadOptions loadOptions, long idRegimen)
        {
            if (idRegimen <= 0)
                return null;
            else
            {
                var regimen = Manager().GetBusinessLogic<TiposRegimenes>().Tabla(true).Where(x => x.Id == idRegimen).FirstOrDefault();
                List<string> codigos = new List<string>();
                if (regimen.Codigo.Equals("CON") || regimen.Codigo.Equals("D-C"))
                    codigos = new List<string> { "COT", "BEN", "ADI" };
                if (regimen.Codigo.Equals("SUB") || regimen.Codigo.Equals("D-S"))
                    codigos = new List<string> { "CAB", "BEN" };
                if (regimen.Codigo.Equals("VIN") || regimen.Codigo.Equals("D-N"))
                    codigos = new List<string> { "VIN" };
                if (regimen.Codigo.Equals("PAR"))
                    codigos = new List<string> { "PAR" };
                if (regimen.Codigo.Equals("OTRO"))
                    codigos = new List<string> { "OTRO" };

                return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposAfiliados>().Tabla(true).Where(x=> codigos.Contains(x.Codigo)), loadOptions);
            }
        }
        [HttpPost]
        public LoadResult GetCategoriasIngresosId(DataSourceLoadOptions loadOptions, long idRegimen)
        {
            if (idRegimen <= 0)
                return null;
            else
            {
                var regimen = Manager().GetBusinessLogic<TiposRegimenes>().Tabla(true).Where(x => x.Id == idRegimen).FirstOrDefault();
                List<string> codigos = new List<string>();
                if (regimen.Codigo.Equals("CON") || regimen.Codigo.Equals("D-C"))
                    codigos = new List<string> { "A", "B", "C" };
                if (regimen.Codigo.Equals("SUB") || regimen.Codigo.Equals("D-S"))
                    codigos = new List<string> { "1", "2" };

                return DataSourceLoader.Load(Manager().GetBusinessLogic<CategoriasIngresos>().Tabla(true).Where(x => codigos.Contains(x.Codigo)), loadOptions);
            }
        }
        [HttpPost]
        public LoadResult GetTiposRegimenesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposRegimenes>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetDepartamentosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Departamentos>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetPaisesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Paises>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetEmpresasId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empresas>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEstadosCivilesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<EstadosCiviles>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetTiposIdentificacionId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposIdentificacion>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetTiposSangreId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposSangre>().Tabla(true), loadOptions);
        }

        #endregion

        [HttpGet]
        public IActionResult BuscarPacientePorCedula(long tipoIdentificacion, string numeroIdentificacion, bool desdeCitas, bool citaNueva)
        {
            Pacientes paciente = Manager().GetBusinessLogic<Pacientes>().FindById(x => x.TiposIdentificacionId == tipoIdentificacion && x.NumeroIdentificacion == numeroIdentificacion, false);
            if (paciente != null)
            {
                PacientesModel pacientesModel = EditModel(paciente.Id);
                pacientesModel.DesdeCitas = desdeCitas;
                pacientesModel.CitaNueva = citaNueva;
                return PartialView("Edit", pacientesModel);
            }
            else
                return new OkObjectResult(false);
        }

        [HttpGet]
        public IActionResult DescargarPlantilla()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "wwwroot", "Files", "Planos", "PACIENTES.xlsx"));
            return File(fileBytes, "application/octet-stream", $"PlantillaPacientes_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx");
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
                var pathFileError = Manager().PacientesBusinessLogic().CargarDatosPlantilla(ms, modificaRegistros, User.Identity.Name, this.ActualEmpresaId());
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
            return File(fileBytes, "application/octet-stream", $"ErroresPlantillaPacientes.txt");
        }


    }
}
