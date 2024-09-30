using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Dominus.Backend.Application;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WebApp.Reportes.Notas;

namespace Blazor.WebApp.Controllers
{

    public partial class NotasController
    {

        [HttpGet]
        public async Task<IActionResult> EnviarNotasDIAN(long id)
        {
            try
            {
                var result = "";

                Notas nota = Manager().NotasBusinessLogic().FindById(x => x.Id == id, false);
                var transaction = Manager().GetBusinessLogic<Documentos>().FindById(x => x.Id == nota.DocumentosId, false);
                if (transaction != null && transaction.Transaccion == 3)
                    result = await Manager().NotasBusinessLogic().SendCreditNoteAsync(id, DApp.GetTenantService(Request.Host.Host, "Acepta"));
                else if (transaction != null && transaction.Transaccion == 4)
                    result = await Manager().NotasBusinessLogic().SendDebitNoteAsync(id, DApp.GetTenantService(Request.Host.Host, "Acepta"));

                if (!string.IsNullOrWhiteSpace(result))
                {
                    if (result.Contains("ERROR"))
                    {
                        nota.ErrorReference = result;
                        nota.UrlTracking = "";
                    }
                    else
                    {
                        nota.ErrorReference = "";
                        nota.UrlTracking = result;
                    }
                    nota = Manager().NotasBusinessLogic().Modify(nota);
                }

                return PartialView("Edit", EditModel(id));
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
                var factura = Manager().GetBusinessLogic<Notas>().FindById(x => x.Id == id, true);

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
                string fileName = $"{factura.Documentos.Prefijo}{factura.Consecutivo }.xml";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

        [HttpGet]
        public async Task<IActionResult> EnviarNota(long id)
        {
            try
            {
                Notas nota = Manager().NotasBusinessLogic().FindById(x => x.Id == id, true);
                await Manager().NotasBusinessLogic().EnviarEmail(nota, GetPdfNotaReporte(nota));
                return Ok();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

        private string GetPdfNotaReporte(Notas nota)
        {
            InformacionReporte informacionReporte = new InformacionReporte();
            informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == nota.EmpresasId, true);
            informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
            informacionReporte.Ids = new long[] { nota.Id };

            NotasReporte report = new NotasReporte();
            report.SetInformacionReporte(informacionReporte);
            XtraReport xtraReport = report;

            string pathPdf = Path.Combine(Path.GetTempPath(), $"{nota.Documentos.Prefijo}-{nota.Consecutivo}.pdf");
            PdfExportOptions pdfOptions = new PdfExportOptions();
            pdfOptions.ConvertImagesToJpeg = false;
            pdfOptions.ImageQuality = PdfJpegImageQuality.Medium;
            pdfOptions.PdfACompatibility = PdfACompatibility.PdfA2b;
            xtraReport.ExportToPdf(pathPdf, pdfOptions);
            return pathPdf;
        }
    }
}