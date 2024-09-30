using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExpress.XtraReports.UI;
using Dominus.Backend.Application;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using WebApp.Reportes.General.RadicacionCuentasReporte;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class RadicacionCuentasController
    {

        //private const string Prefix = "RadicacionCuentas"; 

        protected List<Facturas> GetFacturasPorRadicar(RadicacionCuentas model)
        {
            var result = Manager().GetBusinessLogic<Facturas>().FindAll(x => x.EmpresasId == model.EmpresasId && x.SedesId == model.SedesId && x.EntidadesId == model.EntidadesId && x.Estadosid == 14, true);
            return result;
        }

        [HttpPost]
        public IActionResult RadicarFacturas([FromBody]List<Facturas> models)
        {
            RadicacionCuentas factGen = Manager().GetBusinessLogic<RadicacionCuentas>().FindById(x => x.Id == models[0].RadicacionFacturasId, false);
            Estados estado = Manager().GetBusinessLogic<Estados>().FindById(x => x.Tipo == "FACTURA" && x.Nombre == "RADICADA", false);
            List<RadicacionCuentasDetalles> errores = new List<RadicacionCuentasDetalles>();
            List<RadicacionCuentasDetalles> radicacionDetalles = new List<RadicacionCuentasDetalles>();
            foreach (var item in models)
            {
                radicacionDetalles.Add(new RadicacionCuentasDetalles { FacurasId = item.Id, RadicacionCuentasId = item.RadicacionFacturasId, CreatedBy = User.Identity.Name, CreationDate = DateTime.Now, LastUpdate = DateTime.Now, UpdatedBy = User.Identity.Name });
            }
            foreach (var item in radicacionDetalles)
            {
                try
                {
                    var radicado = Manager().GetBusinessLogic<RadicacionCuentasDetalles>().Add(item);
                    var factura = Manager().GetBusinessLogic<Facturas>().FindById(x => x.Id == radicado.FacurasId, false);
                    factura.Estadosid = estado.Id;
                    Manager().GetBusinessLogic<Facturas>().Modify(factura);
                }
                catch(Exception)
                {
                    errores.Add(item);
                }
            }
            RadicacionCuentasModel model = new RadicacionCuentasModel();
            model.Entity = factGen;
            model.Process = false;
            return PartialView("Edit", model);
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

        [HttpGet]
        public IActionResult ImprimirRadicacionCuentasPorId(int Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { Id };
                XtraReport report = new RadicacionCuentasReporte(informacionReporte);
                return PartialView("_ViewerReport", report);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
            
        }

    }
}
