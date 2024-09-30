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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Reportes.Notas;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class NotasController : BaseAppController
    {

        //private const string Prefix = "Notas"; 

        public NotasController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Notas>().Tabla(true).Include(x=>x.Facturas.Documentos), loadOptions);
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

        private NotasModel NewModel()
        {
            NotasModel model = new NotasModel();
            model.Entity.Fecha = DateTime.Now;
            model.Entity.Estados = Manager().GetBusinessLogic<Estados>().FindById(x => x.Tipo == "NOTAS" && x.Nombre == "EN ELABORACION", false);
            if (model.Entity.Estados != null)
                model.Entity.Estadosid = model.Entity.Estados.Id;
            model.Entity.Consecutivo = 0;
            model.Entity.IsNew = true;
            model.Entity.Documentos = Manager().GetBusinessLogic<Documentos>().FindById(x => x.Transaccion == 3, false);
            if (model.Entity.Documentos != null)
                model.Entity.DocumentosId = model.Entity.Documentos.Id;
            model.Entity.FechaVencimiento = DateTime.Now.AddDays(1);

            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private NotasModel EditModel(long Id)
        {
            NotasModel model = new NotasModel();
            model.Entity = Manager().GetBusinessLogic<Notas>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model;
        }

        [HttpPost]
        public IActionResult Edit(NotasModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        private NotasModel EditModel(NotasModel model)
        {
            ViewBag.Accion = "Save";
            var OnState = model.Entity.IsNew;

            if (model.Entity.FechaVencimiento.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("Entity.FechaVencimiento", "La fecha vencimiento no puede ser inferior a la fecha actual.");
            }

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
                        model.Entity.MontoEscrito = DApp.Util.NumeroEnLetras(model.Entity.ValorTotal);
                        
                        model.Entity = Manager().GetBusinessLogic<Notas>().Add(model.Entity);

                        model.Entity.Documentos = Manager().GetBusinessLogic<Documentos>().FindById(x => x.Id == model.Entity.DocumentosId, false);
                        model.Entity.NotasConceptos = Manager().GetBusinessLogic<NotasConceptos>().FindById(x => x.Id == model.Entity.NotasConceptosId, false);

                        if (model.Entity.Documentos.Transaccion == 3 && model.Entity.NotasConceptos.Codigo == "1")
                        {
                            var pacientes = JsonConvert.DeserializeObject<List<Pacientes>>(model.SerializedPacientes);
                            if (pacientes != null && pacientes.Count > 0)
                            {
                                var pacientesId = pacientes.Select(x => x.Id).ToList();
                                var serviciosFacturados = Manager().GetBusinessLogic<ServiciosFacturar>().Tabla(false).Where(x => x.FacturasId == model.Entity.FacturasId && pacientesId.Contains(x.PacientesId.GetValueOrDefault(0))).ToList();
                                for (int i = 0; i < serviciosFacturados.Count; i++)
                                {
                                    NotasDetalles notasDetalles = new NotasDetalles();
                                    notasDetalles.Id = 0;
                                    notasDetalles.NotasId = model.Entity.Id;
                                    notasDetalles.IsNew = true;
                                    notasDetalles.UpdatedBy = User.Identity.Name;
                                    notasDetalles.CreatedBy = User.Identity.Name;
                                    notasDetalles.LastUpdate = DateTime.Now;
                                    notasDetalles.CreationDate = DateTime.Now;

                                    notasDetalles.NroLinea = i+1;
                                    notasDetalles.PacientesId = serviciosFacturados[i].PacientesId;
                                    notasDetalles.ServiciosId = serviciosFacturados[i].ServiciosId.GetValueOrDefault(0);
                                    notasDetalles.Cantidad = serviciosFacturados[i].Cantidad;
                                    notasDetalles.PorcDescuento = 0;
                                    notasDetalles.PorcImpuesto = 0;
                                    notasDetalles.ValorServicio = serviciosFacturados[i].ValorServicio;
                                    notasDetalles.SubTotal = serviciosFacturados[i].SubTotal;
                                    notasDetalles.ValorTotal = serviciosFacturados[i].Total;
                                    Manager().GetBusinessLogic<NotasDetalles>().Add(notasDetalles);
                                }

                            }
                        }

                        if (model.Entity.Documentos.Transaccion==3 && model.Entity.NotasConceptos.Codigo=="2")
                        {
                            List<NotasDetalles> detalles = Manager().GetBusinessLogic<FacturasDetalles>().Tabla().Where(x => x.FacturasId == model.Entity.FacturasId).Select(x => new NotasDetalles { NotasId= model.Entity.Id, Cantidad = x.Cantidad, NroLinea = x.NroLinea, PorcDescuento = x.PorcDescuento, PorcImpuesto = x.PorcImpuesto, ServiciosId = x.ServiciosId, SubTotal = x.SubTotal, ValorServicio = x.ValorServicio, ValorTotal = x.ValorTotal, CreatedBy = model.Entity.CreatedBy, CreationDate = model.Entity.CreationDate, LastUpdate = model.Entity.LastUpdate, UpdatedBy = model.Entity.UpdatedBy }).ToList();
                            foreach (var item in detalles)
                            {
                                Manager().GetBusinessLogic<NotasDetalles>().Add(item);
                            }
                        }
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        model.Entity.MontoEscrito = DApp.Util.NumeroEnLetras(model.Entity.ValorTotal);
                        model.Entity = Manager().GetBusinessLogic<Notas>().Modify(model.Entity);
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
        public IActionResult Delete(NotasModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private NotasModel DeleteModel(NotasModel model)
        {
            ViewBag.Accion = "Delete";
            NotasModel newModel = NewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<Notas>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<Notas>().Remove(model.Entity);
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

        private NotasModel NewModelDetail(long IdFather) 
        { 
            NotasModel model = new NotasModel(); 
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
        public IActionResult EditDetail(NotasModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(NotasModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private NotasModel DeleteModelDetail(NotasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            NotasModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Notas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Notas>().Remove(model.Entity); 
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
             Notas entity = new Notas(); 
             JsonConvert.PopulateObject(values, entity); 
             NotasModel model = new NotasModel(); 
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
             Notas entity = Manager().GetBusinessLogic<Notas>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             NotasModel model = new NotasModel(); 
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
             Notas entity = Manager().GetBusinessLogic<Notas>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Notas>().Remove(entity); 
        } 

        */
        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetDocumentosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Documentos>().Tabla(true).Where(x => (x.Transaccion == 3 || x.Transaccion == 4)), loadOptions);
        }
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
        public LoadResult GetEstadosid(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true).Where(x => x.Tipo == "NOTAS"), loadOptions);
        }
        [HttpPost]
        public LoadResult GetFacturasId(DataSourceLoadOptions loadOptions)
        {
            //return DataSourceLoader.Load(Manager().GetBusinessLogic<Facturas>().Tabla(true).Where(x => x.CUFE != null), loadOptions);
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Facturas>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetNotasConceptosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<NotasConceptos>().Tabla(true), loadOptions);
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
        public LoadResult GetFormasPagosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<FormasPagos>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetMediosPagoId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<MediosPago>().Tabla(true), loadOptions);
        }
        #endregion

        [HttpGet]
        public IActionResult ImprimirNotaPorId(int Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { Id };

                NotasReporte report = new NotasReporte();
                report.SetInformacionReporte(informacionReporte);
                XtraReport xtraReport = report;

                return PartialView("_ViewerReport", report);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

        [HttpPost]
        public IActionResult AprobarNota(NotasModel model)
        {
            try
            {
                var detalle = Manager().GetBusinessLogic<NotasDetalles>().FindById(x => x.NotasId == model.Entity.Id, false);
                if (detalle == null)
                {
                    ModelState.AddModelError("Entity.Id", "Debe agregar al menos un detalle a la nota.");
                }
                else
                {
                    model.Entity = Manager().NotasBusinessLogic().AprobarNota(model.Entity);
                }
                return PartialView("Edit", model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage());
                return PartialView("Edit", model);
            }

            

        }

        [HttpPost]
        public IActionResult AnularNota(NotasModel model)
        {
            try
            {
                model.Entity = Manager().NotasBusinessLogic().AnularNota(model.Entity);
                return PartialView("Edit", model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage());
                return PartialView("Edit", model);
            }
        }

        [HttpPost]
        public LoadResult GetPacientesASeleccionar(DataSourceLoadOptions loadOptions, long facturaId)
        {
            List<long?> idPacientes = Manager().GetBusinessLogic<ServiciosFacturar>().Tabla(true).Where(x => x.FacturasId == facturaId).Select(x=>x.PacientesId).Distinct().ToList();
            var pacientes = Manager().GetBusinessLogic<Pacientes>().Tabla(true).Where(x => idPacientes.Contains(x.Id));
            return DataSourceLoader.Load(pacientes, loadOptions);
        }
    }
}
