using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExpress.XtraReports.UI;
using Dominus.Backend.Application;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApp.Reportes.EntregaAdmisiones;
using WebApp.Reportes.Facturas;
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class AdmisionesController
    {
        private AdmisionesModel EditModel(long Id)
        {
            AdmisionesModel model = new AdmisionesModel();
            model.Entity = Manager().GetBusinessLogic<Admisiones>().FindById(x => x.Id == Id, true);
            model.EntidadesConvenio = GetEntidadesConvenios(model.Entity.ConveniosId);
            model.EmpleadosId = model.Entity.ProgramacionCitas.EmpleadosId;
            model.ConsultoriosId = model.Entity.ProgramacionCitas.ConsultoriosId;
            model.Entity.IsNew = false;

            var serviciosDetalle = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().FindById(x => x.AdmisionesId == Id && x.Facturado,false);
            if (serviciosDetalle != null)
                model.TieneServiciosFacturadosAEntidad = true;

            model.TieneServiciosFacturadosAEntidad = false;

            if (model.Entity.ExoneracionArchivo == null)
                model.Entity.ExoneracionArchivo = new Archivos();
            else
                model.Entity.ExoneracionArchivo.StringToBase64 = DApp.Util.ArrayBytesToString(model.Entity.ExoneracionArchivo.Archivo);

            return model;
        }
        private AdmisionesModel EditModel(AdmisionesModel model)
        {
            ViewBag.Accion = "Save";
            var OnState = model.Entity.IsNew;

            if (!string.IsNullOrWhiteSpace(model.Entity.NroAutorizacion))
            {
                var citaSeleccionada = Manager().GetBusinessLogic<ProgramacionCitas>().FindById(x => x.Id == model.Entity.ProgramacionCitasId, true);
                var admisionautorizacion = Manager().GetBusinessLogic<Admisiones>()
                    .FindById(x => x.NroAutorizacion == model.Entity.NroAutorizacion && x.EstadosId != 72 && x.Id != model.Entity.Id && x.ProgramacionCitas.ServiciosId == citaSeleccionada.ServiciosId, true);
                if (admisionautorizacion != null)
                {
                    ModelState.AddModelError("Entity.Id", $"El numero de autorizacion {model.Entity.NroAutorizacion} ya fue registrado en la admision con consecutivo {admisionautorizacion.Id} para el servicio {citaSeleccionada.Servicios.Nombre}.");
                }
            }

            if (model.Entity.FechaAutorizacion > DateTime.Now)
            {
                ModelState.AddModelError("Entity.Id", "La fecha de autorizacion no puede ser mayor que la fecha actual.");
            }

            ModelState.Remove("CategoriasIngresosDetalles.CategoriasIngresosId");

            var llaves = ModelState.Where(x => x.Key.Contains("Entity.ProgramacionCitas")).Select(x => x.Key).ToList();
            foreach (var key in llaves)
            {
                ModelState.Remove(key);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity.LastUpdate = DateTime.Now;
                    model.Entity.UpdatedBy = User.Identity.Name;

                    if (model.Entity.IsNew)
                    {
                        if (model.Entity.PorcDescAutorizado == 0)
                            model.Entity.EstadosId = 10066;
                        if (model.Entity.PorcDescAutorizado > 0)
                        {
                            Manager().AdmisionesBusinessLogic().configSendEmail(model.Entity.PacientesId);
                            model.Entity.EstadosId = 61;
                        }

                        model.Entity.CreationDate = DateTime.Now;
                        model.Entity.CreatedBy = User.Identity.Name;
                        model.Entity = Manager().GetBusinessLogic<Admisiones>().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        model.Entity = Manager().GetBusinessLogic<Admisiones>().Modify(model.Entity);
                    }

                    var serviciosDetalle = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().FindById(x => x.AdmisionesId == model.Entity.Id && x.Facturado, false);
                    if (serviciosDetalle != null)
                        model.TieneServiciosFacturadosAEntidad = true;
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
        public String GetTipoId(int id)
        {
            return Manager().GetBusinessLogic<TiposIdentificacion>().FindById(x => x.Id == id, false).Codigo;
        }

        [HttpPost]
        public long? GetConvenio(long id)
        {
            return Manager().GetBusinessLogic<ProgramacionCitas>().FindById(x => x.Id == id, false).ConveniosId;
        }
        [HttpPost]
        public long? GetEntidadesConvenios(long? id)
        {
            if (id == null)
                return null;
            else
                return Manager().GetBusinessLogic<Convenios>().FindById(x => x.Id == id, false).EntidadesId; ;
        }

        public long GetCitaCercana(long IdPaciente)
        {
            var dateActual = DateTime.Now;
            var citas = Manager().GetBusinessLogic<ProgramacionCitas>().FindAll(x => x.FechaInicio <= dateActual && x.PacientesId == IdPaciente, false).OrderByDescending(x => x.FechaInicio).FirstOrDefault();
            if (citas != null)
                return citas.Id;
            else
                return 0;
        }

        public AdmisionesModel GetValorCategoriaIngreso(long PacienteId, long CitasId)
        {
            AdmisionesModel admisiones = new AdmisionesModel();
            Pacientes paciente = Manager().GetBusinessLogic<Pacientes>().FindById(x => x.Id == PacienteId, true);
            if (paciente != null)
            {
                admisiones.CategoriasIngresosDetalles = Manager().GetBusinessLogic<CategoriasIngresosDetalles>().FindById(x => x.CategoriasIngresosId == paciente.CategoriasIngresosId && x.FechaInicial <= DateTime.Now && x.FechaFinal >= DateTime.Now, false);
                admisiones.valorServicio = ObtenerValorServicio(paciente, admisiones.CategoriasIngresosDetalles, CitasId);
            }
            return admisiones;
        }

        private decimal ObtenerValorServicio(Pacientes paciente, CategoriasIngresosDetalles categoriasIngresosDetalles, long CitasId)
        {
            try
            {
                if (paciente == null || categoriasIngresosDetalles == null || CitasId <= 0)
                    return 0;

                if (paciente.TiposRegimenes != null)
                {
                    if (paciente.TiposAfiliados != null)
                    {
                        if (paciente.TiposRegimenes.Codigo.Equals("CON") && paciente.TiposAfiliados.Codigo.Equals("COT"))
                            return 0;
                        else if (paciente.TiposRegimenes.Codigo.Equals("SUB") && paciente.CategoriasIngresos != null && paciente.CategoriasIngresos.Codigo.Equals("1"))
                            return 0;
                        else
                            return Manager().AdmisionesBusinessLogic().ValorCopago(categoriasIngresosDetalles.PorcentajeCopago, categoriasIngresosDetalles.CopagoMaximoEvento, CitasId);
                    }
                }
            }
            catch (Exception) { }

            return 0;
        }

        [HttpPost]
        public IActionResult FacturaIndividual(AdmisionesModel model)
        {
            try
            {
                ViewBag.FacturaConsecutivo = Manager().FacturasBusinessLogic().FacturaIndividual(model.Entity.Id, this.ActualEmpresaId(), this.ActualUsuarioId());
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Entity.Id", e.Message);
                return PartialView("Edit", model);
            }

            model = EditModel(model.Entity.Id);
            ViewBag.Accion = "Facturado";

            return PartialView("Edit", model);
        }

        [HttpGet]
        public IActionResult ImprimirFacturaIndividual(long Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { Id };

                FacturasParticularReporte report = new FacturasParticularReporte();
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
        public IActionResult AnularAdmision(long admisionId, string detalleAnulacion)
        {
            try
            {
                Manager().AdmisionesBusinessLogic().AnularAtencion(admisionId, detalleAnulacion);
                return New();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }

        }

        [HttpPost]
        public IActionResult ImprimirReporteEntregaProduccion(long sedeId, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                if (sedeId <= 0 || fechaDesde.Year < 1900 || fechaHasta.Year < 1900)
                    throw new Exception("Los parametros Fecha Desde, Fecha Hasta y Sede no fueron enviados correctamente al servidor.");

                fechaDesde = new DateTime(fechaDesde.Year, fechaDesde.Month, fechaDesde.Day, 0, 0, 0);
                fechaHasta = new DateTime(fechaHasta.Year, fechaHasta.Month, fechaHasta.Day, 23, 59, 59);

                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.ParametrosAdicionales.Add("p_FechaDesde", fechaDesde);
                informacionReporte.ParametrosAdicionales.Add("p_FechaHasta", fechaHasta);
                informacionReporte.ParametrosAdicionales.Add("p_SedeId", sedeId);
                informacionReporte.ParametrosAdicionales.Add("p_UsuarioGenero", User.Identity.Name);

                EntregaAdmisionesReporte report = new EntregaAdmisionesReporte();
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
        public IActionResult XLSXReporteEntregaProduccion(long sedeId, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                if (sedeId <= 0 || fechaDesde.Year < 1900 || fechaHasta.Year < 1900)
                    throw new Exception("Los parametros Fecha Desde, Fecha Hasta y Sede no fueron enviados correctamente al servidor.");

                fechaDesde = new DateTime(fechaDesde.Year, fechaDesde.Month, fechaDesde.Day, 0, 0, 0);
                fechaHasta = new DateTime(fechaHasta.Year, fechaHasta.Month, fechaHasta.Day, 23, 59, 59);
                byte[] book = Manager().AdmisionesBusinessLogic().ExcelEntregaAdmisiones(sedeId, fechaDesde, fechaHasta);
                return File(book, "application/octet-stream", $"Admisiones_{sedeId}_{fechaDesde.ToString("ddMMyyyy")}_{fechaHasta.ToString("ddMMyyyy")}.xlsx");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

    }
}
