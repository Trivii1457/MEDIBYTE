using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using WidgetGallery;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Blazor.WebApp.Models;
using System.Text;

namespace Blazor.WebApp.Controllers
{
    [Authorize]
    public class HomeController : BaseAppController
    {
        public HomeController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        public IActionResult Index()
        {
            ViewBag.VersionApp = DApp.InfoApp.VersionApp;
            ViewBag.ParcheApp = DApp.InfoApp.ParcheApp;
            return View();
        }

        public IActionResult DicomViewer()
        {
            DicomViewerModel dicomViewer = new DicomViewerModel();
            return View("_ViewerDicom", dicomViewer);
        }

        public IActionResult DicomViewerImageDisco(long id)
        {
            DicomViewerModel dicomViewer = new DicomViewerModel();
            var data = Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().FindById(x => x.Id == id, false);
            var db = DApp.GetTenantConnection(Request.Host.Value);
            if (data != null)
            {
                dicomViewer.EsDesdeDisco = true;
                dicomViewer.TieneImagen = true;
                dicomViewer.Contenedor = db.InitialCatalog.ToLower();
                dicomViewer.NombreArchivoAzureBlob = data.NombreArchivoAzureBlob;
            }
            return View("_ViewerDicom", dicomViewer);
        }

        public IActionResult DicomViewerImageAzure(long id)
        {
            DicomViewerModel dicomViewer = new DicomViewerModel();
            var data = Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().FindById(x => x.Id == id, false);
            var db = DApp.GetTenantConnection(Request.Host.Value);
            if (data != null)
            {
                dicomViewer.TieneImagen = true;
                dicomViewer.Contenedor = db.InitialCatalog.ToLower();
                dicomViewer.NombreArchivoAzureBlob = data.NombreArchivoAzureBlob;
            }
            return View("_ViewerDicom", dicomViewer);
        }

        [HttpPost]
        public Dictionary<string, object> ObtenerPreferenciasUsuario(bool bloqueoPantalla)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("BloqueoPantalla", bloqueoPantalla);
            return data;
        }

        [HttpPost]
        public bool DesBloqueoPantalla(string password)
        {
            var usuario = Manager().UserBusinessLogic().SingIn(User.Identity.Name, password, Request.Host.Value);
            if (usuario == null)
                return false;
            else
            {
                return true;
            }
        }

    }
}
