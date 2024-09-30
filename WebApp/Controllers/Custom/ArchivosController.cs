using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using Dominus.Backend.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class ArchivosController 
    {

        [HttpGet]
        public IActionResult DescargarArchivo(long idArchivo)
        {
            var archivo = Manager().GetBusinessLogic<Archivos>().FindById(x=>x.Id == idArchivo, false);
            if (archivo != null)
                return File(archivo.Archivo, archivo.TipoContenido, $"{archivo.Maestro}_{archivo.Nombre}");
            else
                return BadRequest("No existe archivo con el Id = " + idArchivo);
        }

        [HttpPost]
        public IActionResult ProcesarArchivo(IFormFile file,string maestro, string nombreCampoForanea)
        {
            Archivos archivo = new Archivos();
            if (file.Length > 0)
            {
                archivo.Maestro = maestro;
                archivo.NombreCampoForanea = nombreCampoForanea;
                archivo.EliminarArchivo = false;

                archivo.IsNew = true;
                archivo.Nombre = file.FileName;
                archivo.TipoContenido = file.ContentType;
                archivo.CreatedBy = User.Identity.Name;
                archivo.UpdatedBy = User.Identity.Name;

                using (MemoryStream ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    archivo.StringToBase64 = DApp.Util.ArrayBytesToString(ms.ToArray());
                }
            }

            return PartialView("DataArchivo", archivo);
        }
    }
}
