using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExpress.XtraReports.UI;
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
using WebApp.Reportes.FacturaDetalle;
using WebApp.Reportes.Facturas;
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class FacturasGeneracionController : BaseAppController
    {

        //private const string Prefix = "FacturasGeneracion"; 

        public FacturasGeneracionController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<FacturasGeneracion>().Tabla(true), loadOptions);
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

        private FacturasGeneracionModel NewModel() 
        { 
            FacturasGeneracionModel model = new FacturasGeneracionModel();
            model.Entity = new FacturasGeneracion();
            model.Entity.Empresas = Manager().GetBusinessLogic<Empresas>().FindAll(null)[0];
            if (model.Entity.Empresas != null)
                model.Entity.EmpresasId = model.Entity.Empresas.Id;
            //model.Entity.Entidades = Manager().GetBusinessLogic<Entidades>().FindAll(null)[0];
            //if (model.Entity.Entidades != null)
            //    model.Entity.EntidadesId = model.Entity.Entidades.Id;
            model.Entity.FechaInicio = DateTime.Now;
            model.Entity.FechaFinal = DateTime.Now;
            model.Entity.IsNew = true;
            model.Process = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private FacturasGeneracionModel EditModel(long Id) 
        { 
            FacturasGeneracionModel model = new FacturasGeneracionModel();
            model.Entity = Manager().GetBusinessLogic<FacturasGeneracion>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            model.DetalleServiciosFacturar = new List<ServiciosFacturar>();
            model.Process = !(Manager().GetBusinessLogic<ServiciosFacturar>().FindAll(x => x.FacturasGeneracionId == Id).Count > 0);
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(FacturasGeneracionModel model)
        {
            model.DetalleServiciosFacturar = GetServiciosAFacturar(model);
            if (model.DetalleServiciosFacturar == null || model.DetalleServiciosFacturar.Count == 0)
            {
                ModelState.AddModelError("Entity.Id", "No hay servicios para facturar en este periodo");
                return PartialView("Edit", model);
            }
            else
            {
                model = EditModel(model);
                foreach (var item in model.DetalleServiciosFacturar)
                {
                    item.FacturasGeneracionId = model.Entity.Id;
                }  
                return PartialView("Edit",model );
            }
          
        }

        private FacturasGeneracionModel EditModel(FacturasGeneracionModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<FacturasGeneracion>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<FacturasGeneracion>().Modify(model.Entity); 
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
        public IActionResult Delete(FacturasGeneracionModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private FacturasGeneracionModel DeleteModel(FacturasGeneracionModel model)
        { 
            ViewBag.Accion = "Delete"; 
            FacturasGeneracionModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<FacturasGeneracion>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<FacturasGeneracion>().Remove(model.Entity); 
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

        private FacturasGeneracionModel NewModelDetail(long IdFather) 
        { 
            FacturasGeneracionModel model = new FacturasGeneracionModel(); 
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
        public IActionResult EditDetail(FacturasGeneracionModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(FacturasGeneracionModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private FacturasGeneracionModel DeleteModelDetail(FacturasGeneracionModel model)
        { 
            ViewBag.Accion = "Delete"; 
            FacturasGeneracionModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<FacturasGeneracion>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<FacturasGeneracion>().Remove(model.Entity); 
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
             FacturasGeneracion entity = new FacturasGeneracion(); 
             JsonConvert.PopulateObject(values, entity); 
             FacturasGeneracionModel model = new FacturasGeneracionModel(); 
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
             FacturasGeneracion entity = Manager().GetBusinessLogic<FacturasGeneracion>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             FacturasGeneracionModel model = new FacturasGeneracionModel(); 
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
             FacturasGeneracion entity = Manager().GetBusinessLogic<FacturasGeneracion>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<FacturasGeneracion>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetEmpresasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empresas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEntidadesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Entidades>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetConveniosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Convenios>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetPacientesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Pacientes>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetSedesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Sedes>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetEstadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetDocumentosId(DataSourceLoadOptions loadOptions,long sedesId)
        {
            if (sedesId > 0)
            {
                return DataSourceLoader.Load(
                    Manager().GetBusinessLogic<SedesDocumentos>().Tabla(true)
                    .Where(x => x.Documentos.Transaccion == 0 && x.Documentos.Activo && x.SedesId == sedesId)
                    .Select(x => x.Documentos)
                    , loadOptions);
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        public LoadResult GetFilialesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Filiales>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetTiposRegimenesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposRegimenes>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetMediosPagoId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<MediosPago>().Tabla(true), loadOptions);
        }

        #endregion

        public IActionResult DescargarPlantillaCargueFE()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "wwwroot", "Files", "Planos", "PlantillaCargueFe.xlsx"));
            return File(fileBytes, "application/octet-stream", "PlantillaCargueFe.xlsx");
        }

        [HttpGet]
        public IActionResult ImprimirImprimirAnexo(int Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);

                var admisionesServiciosPrestados = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().FindById(x => x.FacturasGeneracionId == Id, false);
                if (admisionesServiciosPrestados == null)
                {
                    return new BadRequestObjectResult("No se han facturado servicios.");
                }
                else
                {
                    if (admisionesServiciosPrestados.FacturasId == null)
                    {
                        return new BadRequestObjectResult("No se han facturado servicios.");
                    }
                }

                informacionReporte.Ids = new long[] { admisionesServiciosPrestados.FacturasId.GetValueOrDefault(0) };

                FacturaDetalleReporte report = new FacturaDetalleReporte();
                report.SetInformacionReporte(informacionReporte);
                XtraReport xtraReport = report;

                return PartialView("_ViewerReport", report);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

        [HttpGet]
        public IActionResult ImprimirFacturaPorId(int Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);

                var admisionesServiciosPrestados = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().FindById(x => x.FacturasGeneracionId == Id, false);
                if (admisionesServiciosPrestados == null)
                {
                    return new BadRequestObjectResult("No se han facturado servicios.");
                }
                else
                {
                    if (admisionesServiciosPrestados.FacturasId == null)
                    {
                        return new BadRequestObjectResult("No se han facturado servicios.");
                    }
                }

                informacionReporte.Ids = new long[] { admisionesServiciosPrestados.FacturasId.GetValueOrDefault(0) };

                FacturasReporte report = new FacturasReporte();
                report.SetInformacionReporte(informacionReporte);
                XtraReport xtraReport = report;

                return PartialView("_ViewerReport", report);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }
    }
}
