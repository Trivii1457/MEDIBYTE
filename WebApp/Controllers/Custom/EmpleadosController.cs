using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Backend.Application;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    public partial class EmpleadosController
    {
        [HttpPost]
        public LoadResult GetGenerosId(DataSourceLoadOptions loadOptions)
        {
            List<string> codigos = new List<string>() { "M", "F" };
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Generos>().Tabla(true).Where(x => codigos.Contains(x.Codigo)), loadOptions);
        }

        private EmpleadosModel NewModel()
        {
            EmpleadosModel model = new EmpleadosModel();
            model.Entity.IsNew = true;
            model.Entity.FirmaDigitalArchivo.IsNew = true;
            model.Entity.SelloDigitalArchivo.IsNew = true;
            model.Entity.EmpresasId = this.ActualEmpresaId();
            return model;
        }

        private EmpleadosModel EditModel(long Id)
        {
            EmpleadosModel model = new EmpleadosModel();
            model.Entity = Manager().GetBusinessLogic<Empleados>().FindById(x => x.Id == Id, true);

            if (model.Entity.FirmaDigitalArchivo == null)
                model.Entity.FirmaDigitalArchivo = new Archivos();
            else
                model.Entity.FirmaDigitalArchivo.StringToBase64 = DApp.Util.ArrayBytesToString(model.Entity.FirmaDigitalArchivo.Archivo);

            if (model.Entity.SelloDigitalArchivo == null)
                model.Entity.SelloDigitalArchivo = new Archivos();
            else
                model.Entity.SelloDigitalArchivo.StringToBase64 = DApp.Util.ArrayBytesToString(model.Entity.SelloDigitalArchivo.Archivo);

            model.Entity.IsNew = false;
            return model;
        }

        private EmpleadosModel EditModel(EmpleadosModel model)
        {
            ViewBag.Accion = "Save";
            var OnState = model.Entity.IsNew;

            if (model.Entity.TipoEmpleados == 2 && string.IsNullOrWhiteSpace(model.Entity.RegistroMedico))
            {
                ModelState.AddModelError("Entity.RegistroMedico", DApp.GetRequiredResource("Empleados.RegistroMedico"));
            }

            if (model.Entity.FechaNacimiento > DateTime.Now.AddYears(-18))
            {
                List<string> codigos = new List<string>() { "NI", "CC", "CE" };
                var resultCodigos = Manager().GetBusinessLogic<TiposIdentificacion>().Tabla(true).Where(x => codigos.Contains(x.Codigo)).ToList();
                bool existeTipo = resultCodigos.Any(x => x.Id == model.Entity.TiposIdentificacionId);
                if (existeTipo)
                {
                    ModelState.AddModelError("Entity.TiposIdentificacionId", DApp.DefaultLanguage.GetResource("Empleados.TipoIdentificacionMenorEdad"));
                }
            }
            else
            {
                List<string> codigos = new List<string>() { "TI", "RC", "MS", "CN" };
                var resultCodigos = Manager().GetBusinessLogic<TiposIdentificacion>().Tabla(true).Where(x => codigos.Contains(x.Codigo)).ToList();
                bool existeTipo = resultCodigos.Any(x => x.Id == model.Entity.TiposIdentificacionId);
                if (existeTipo)
                {
                    ModelState.AddModelError("Entity.TiposIdentificacionId", DApp.DefaultLanguage.GetResource("Empleados.TipoIdentificacionMayorEdad"));
                }
            }

            if (model.Entity.AutorizaDescuento)
            {
                if (model.Entity.IsNew)
                {
                    var empleado = Manager().GetBusinessLogic<Empleados>().Tabla(true).Where(x => x.AutorizaDescuento).FirstOrDefault();
                    if (empleado != null)
                        ModelState.AddModelError("Entity.Id", DApp.DefaultLanguage.GetResource("Empleados.ErrorAutorizaDescuento") +
                            empleado.NumeroIdentificacion + ", " + empleado.NombreCompleto);
                }
                else
                {
                    var empleado = Manager().GetBusinessLogic<Empleados>().Tabla(true).Where(x => x.AutorizaDescuento && x.Id != model.Entity.Id).FirstOrDefault();
                    if (empleado != null)
                        ModelState.AddModelError("Entity.Id", DApp.DefaultLanguage.GetResource("Empleados.ErrorAutorizaDescuento") + 
                            empleado.NumeroIdentificacion + ", " + empleado.NombreCompleto);
                }
            }

            if (model.Entity.UserId != null)
            {
                Empleados empleadoUsuario = Manager().GetBusinessLogic<Empleados>().Tabla(true).FirstOrDefault(x => x.UserId == model.Entity.UserId && x.Id != model.Entity.Id);
                if (empleadoUsuario != null)
                {
                    ModelState.AddModelError("Entity.Id", $"El usuario del sistema ya se encuentra registrado al empleado {empleadoUsuario.NombreCompleto}. Un empleado solo puede tener un solo usuario del sistema.");
                }
            }

            ModelState.Remove("Entity.NombreCompleto");
            if (ModelState.IsValid)
            {
                try
                {
                    model.SetFilesTemp();
                    model.Entity.LastUpdate = DateTime.Now;
                    model.Entity.UpdatedBy = User.Identity.Name;
                    model.Entity.NombreCompleto = $"{model.Entity.PrimerNombre} {model.Entity.SegundoNombre} {model.Entity.PrimerApellido} {model.Entity.SegundoApellido}";
                    if (model.Entity.IsNew)
                    {
                        model.Entity.CreationDate = DateTime.Now;
                        model.Entity.CreatedBy = User.Identity.Name;
                        model.Entity = Manager().EmpleadosBusinessLogic().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        model.Entity = Manager().EmpleadosBusinessLogic().Modify(model.Entity);
                    }
                    model.DeleteTempFiles();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage());
                }
            }
            return model;
        }

        [HttpPost]
        public IActionResult UploadFile(string Property)
        {
            try
            {
                IFormFile myFile = Request.Form.Files["Entity." + Property];
                // Uncomment to save the file
                string fileTemp = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + myFile.FileName);
                using (var fileStream = System.IO.File.Create(fileTemp))
                {
                    myFile.CopyTo(fileStream);
                }

                return Ok(fileTemp);
            }
            catch (Exception e) { return BadRequest(e.Message); }


        }

        public IActionResult DownloadFiles(int Id)
        {
            Archivos model = new Archivos();
            model = Manager().GetBusinessLogic<Archivos>().FindById(x => x.Id == Id, true);
            return File(model.Archivo, model.TipoContenido, model.Nombre);
        }
        [HttpPost]
        public LoadResult GetTiposIdentificacionId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposIdentificacion>().Tabla(true), loadOptions);
        }

        [HttpGet]
        public IActionResult DescargarPlantilla()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "wwwroot", "Files", "Planos", "EMPLEADOS.xlsx"));
            return File(fileBytes, "application/octet-stream", $"PlantillaEmpleados_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx");
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
                var pathFileError = Manager().EmpleadosBusinessLogic().CargarDatosPlantilla(ms, modificaRegistros, User.Identity.Name,this.ActualEmpresaId());
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
            return File(fileBytes, "application/octet-stream", $"ErroresPlantillaEmpleados.txt");
        }
    }
}
