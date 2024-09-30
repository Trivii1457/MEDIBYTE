using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Dominus.Backend.Application;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WebApp.Reportes.Facturas;

namespace Blazor.WebApp.Controllers
{
    public partial class FacturasController
    {

        [HttpGet]
        public IActionResult ImprimirFacturaPorId(int Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { Id };

                Facturas factura = Manager().GetBusinessLogic<Facturas>().FindById(x => x.Id == Id, true);
                if (factura.AdmisionesId != null)
                {
                    FacturasParticularReporte report = new FacturasParticularReporte();
                    report.SetInformacionReporte(informacionReporte);
                    XtraReport xtraReport = report;
                    return PartialView("_ViewerReport", report);
                }
                else
                {
                    FacturasReporte report = new FacturasReporte();
                    report.SetInformacionReporte(informacionReporte);
                    XtraReport xtraReport = report;
                    return PartialView("_ViewerReport", report);
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }

        }

        public string serverUrl;

        [HttpGet]
        public async Task<IActionResult> EnviarFacturaDIAN(long id)
        {
            try
            {
                Facturas factura = Manager().FacturasBusinessLogic().FindById(x => x.Id == id, false);

                var result = await Manager().FacturasBusinessLogic().SendEInvioceAsync(id, DApp.GetTenantService(Request.Host.Host, "Acepta"));

                if (result.Contains("ERROR"))
                {
                    factura.ErrorReference = result;
                    factura.UrlTracking = "";
                }
                else
                {
                    factura.ErrorReference = "";
                    factura.UrlTracking = result;
                }
                factura = Manager().FacturasBusinessLogic().Modify(factura);

                return PartialView("Edit", EditModel(id));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

        [HttpGet]
        public async Task<IActionResult> EnviarFactura(long id)
        {
            try
            {
                Facturas factura = Manager().FacturasBusinessLogic().FindById(x => x.Id == id, true);
                await Manager().FacturasBusinessLogic().EnviarEmail(factura, GetPdfReporte(factura));
                return Ok();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

        private string GetPdfReporte(Facturas factura)
        {
            InformacionReporte informacionReporte = new InformacionReporte();
            informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == factura.EmpresasId, true);
            informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
            informacionReporte.Ids = new long[] { factura.Id };

            XtraReport xtraReport = null;
            if (factura.AdmisionesId != null)
            {
                FacturasParticularReporte report = new FacturasParticularReporte();
                report.SetInformacionReporte(informacionReporte);
                xtraReport = report;
            }
            else
            {
                FacturasReporte report = new FacturasReporte();
                report.SetInformacionReporte(informacionReporte);
                xtraReport = report;
            }

            string pathPdf = Path.Combine(Path.GetTempPath(), $"{factura.Documentos.Prefijo}-{factura.NroConsecutivo}.pdf");
            PdfExportOptions pdfOptions = new PdfExportOptions();
            pdfOptions.ConvertImagesToJpeg = false;
            pdfOptions.ImageQuality = PdfJpegImageQuality.Medium;
            pdfOptions.PdfACompatibility = PdfACompatibility.PdfA2b;
            xtraReport.ExportToPdf(pathPdf, pdfOptions);
            return pathPdf;
        }

        [HttpPost]
        public IActionResult ModificarDatosFactura(long id, string cambioOrdenCompra, string cambioReferenciaFactura, string cambioObservaciones)
        {
            try
            {
                FacturasModel facturasModel = new FacturasModel();
                var factura = Manager().GetBusinessLogic<Facturas>().FindById(x => x.Id == id, true);
                if (factura == null)
                {
                    throw new Exception("Factura no encontrada.");
                }
                factura.OrdenCompra = cambioOrdenCompra;
                factura.ReferenciaFactura = cambioReferenciaFactura;
                factura.Observaciones = cambioObservaciones;
                facturasModel.Entity = Manager().GetBusinessLogic<Facturas>().Modify(factura);
                return Edit(id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }

        }

        [HttpGet]
        public async Task<ActionResult> DownloadInvoiceFileXML(long id)
        {
            try
            {
                var data = Request.Host.ToString();
                var factura = Manager().GetBusinessLogic<Facturas>().FindById(x => x.Id == id, true);

                byte[] contentarray = null;
                HttpClient http = new HttpClient();
                var response = await http.GetAsync(factura.XmlUrl);
                if (response.IsSuccessStatusCode)
                    contentarray = await response.Content.ReadAsByteArrayAsync();
                else
                    throw new Exception($"Error en descargar XMl desde acepta. | {response.StatusCode} - {response.ReasonPhrase}");
                string content = Encoding.UTF8.GetString(contentarray);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);
                content = @"<?xml version='1.0' encoding='UTF-8'?>";
                content += doc.DocumentElement.ChildNodes[3].InnerXml;

                byte[] fileBytes = Encoding.UTF8.GetBytes(content);
                string fileName = $"{factura.Documentos.Prefijo}{factura.NroConsecutivo }.xml";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

        public IActionResult ListInterface()
        {
            return View("ListInterface");
        }

        [HttpPost]
        public IActionResult GenerateFileToCobol(List<long> ids)
        {
            try
            {
                string path = Manager().FacturasBusinessLogic().GenerateFileToCobol(ids);
                return Ok(path);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

        [HttpGet]
        public IActionResult DownloadZipFileToCobol(string path)
        {
            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                return File(fileBytes, "application/octet-stream", $"ArchivosSiesa85_{DateTime.Now.ToString("dd-MM-yyyy_HHmmss")}.zip");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }
    }
}
