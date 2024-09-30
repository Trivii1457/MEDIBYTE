using Blazor.BusinessLogic;
using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Blazor.Infrastructure.Models;
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
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Reportes.AtencionNotaProcedimientos;
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class AtencionesController : BaseAppController
    {

        //private const string Prefix = "Atenciones"; 

        public AtencionesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Atenciones>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetAdmisiones(DataSourceLoadOptions loadOptions)
        {
            var empleado = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId == this.ActualUsuarioId(), false);
            if (empleado == null)
            {
                return DataSourceLoader.Load(new List<Admisiones>(), loadOptions);
            }

            IQueryable<Admisiones> result = Manager().GetBusinessLogic<Admisiones>().Tabla(true)
                .Include(x => x.ProgramacionCitas.Consultorios)
                .Include(x => x.ProgramacionCitas.Sedes)
                .Include(x => x.ProgramacionCitas.Entidades)
                .Include(x => x.ProgramacionCitas.TiposCitas)
                .Include(x => x.ProgramacionCitas.Servicios)
                ;

            ParametrosGenerales parametros = Manager().GetBusinessLogic<ParametrosGenerales>().FindById(x => x.Id == 1, false);

            if (parametros.HabilitarAnteciones)
            {
                result = result
                    .Where(x => x.ProgramacionCitas.FechaInicio >= parametros.FechaDesdeAtenciones && x.ProgramacionCitas.FechaInicio <= parametros.FechaHastaAtenciones);
            }
            else
            {
                
                result = result
                    .Where(x => x.ProgramacionCitas.FechaInicio.Year == DateTime.Now.Year && x.ProgramacionCitas.FechaInicio.Month == DateTime.Now.Month && x.ProgramacionCitas.FechaInicio.Day == DateTime.Now.Day)
                    .Where(x => x.EstadosId == 10066 || x.EstadosId == 10068);
            }

            if (empleado.TipoEmpleados == 2)
            {
                result = result.Where(x => x.ProgramacionCitas.EmpleadosId == empleado.Id);
            }
            else
            {
                result = result.Where(x => x.ProgramacionCitas.EmpleadosId == null);
            }

            
            return DataSourceLoader.Load(result, loadOptions);
        }

        public IActionResult List()
        {
            var empleado = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId == this.ActualUsuarioId(), false);
            if (empleado == null)
            {
                ViewBag.Error = string.Format(DApp.GetResource("BLL.Atenciones.ErrorEmpleadoUsuario"), User.Identity.Name);
            }
            return View("List");
        }

        public IActionResult ListPartial()
        {
            var empleado = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId == this.ActualUsuarioId(), false);
            if (empleado == null)
            {
                ViewBag.Error = string.Format(DApp.GetResource("BLL.Atenciones.ErrorEmpleadoUsuario"), User.Identity.Name);
            }
            return PartialView("List");
        }

        [HttpGet]
        public IActionResult New()
        {
            return PartialView("Edit", NewModel());
        }

        private AtencionesModel NewModel() 
        { 
            AtencionesModel model = new AtencionesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private AtencionesModel EditModel(long Id) 
        { 

            var admision = Manager().GetBusinessLogic<Admisiones>().Tabla(true)
                .Include(x=>x.ProgramacionCitas.Entidades)
                .Include(x=>x.Pacientes.TiposIdentificacion)
                .Include(x=>x.Pacientes.Generos)
                .Include(x=>x.ProgramacionCitas.Servicios)
                .Include(x=>x.ProgramacionCitas.Servicios.TiposServicios)
                .FirstOrDefault(x => x.Id == Id);

            AtencionesModel model = new AtencionesModel();
            model.Entity.AdmisionesId = admision.Id;

            model.Entity.Empleados = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId == this.ActualUsuarioId(), false);
            if (model.Entity.Empleados == null)
            {
                throw new Exception("El usuario actual no tiene asociado un empleado. Por favor configurarlo en el maestro de empleados.");
            }

            model.Entity.EmpleadosId = model.Entity.Empleados.Id;

            if (admision.EstadosId == 10066)
            {
                model.Entity.IsNew = true;

                var atencion = Manager().GetBusinessLogic<Atenciones>().FindById(x => x.AdmisionesId == admision.Id, false);
                var cita = Manager().GetBusinessLogic<ProgramacionCitas>().FindById(x => x.Id == admision.ProgramacionCitasId, false);
                cita.EstadosId = 5;
                Manager().GetBusinessLogic<ProgramacionCitas>().Modify(cita);

                admision.EstadosId = 10068;
                Manager().GetBusinessLogic<Admisiones>().Modify(admision);

                model.Entity.EstadosId = 10073;

                if (atencion == null)
                {
                    model.Entity.FechaAtencion = DateTime.Now;
                    model.Entity.DetalleAnulacion = null;
                    model.Entity.CreationDate = DateTime.Now;
                    model.Entity.CreatedBy = User.Identity.Name;
                    model.Entity.LastUpdate = DateTime.Now;
                    model.Entity.UpdatedBy = User.Identity.Name;
                    model.Entity = Manager().GetBusinessLogic<Atenciones>().Add(model.Entity);
                    model.Entity.IsNew = false;
                }
            }
            else
            {
                model.Entity = Manager().GetBusinessLogic<Atenciones>().Tabla(true)
                .Include(x => x.Admisiones.ProgramacionCitas.Servicios)
                .Include(x => x.Admisiones.ProgramacionCitas.Servicios.TiposServicios)
                .Include(x => x.Empleados)
                .FirstOrDefault(x => x.AdmisionesId == admision.Id);

                model.Entity.IsNew = false;
            }

            model.Entity.Admisiones = admision;

            return model;
        } 

        [HttpPost]
        public IActionResult Edit(AtencionesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private AtencionesModel EditModel(AtencionesModel model) 
        { 
            ViewBag.Accion = "Save"; 
            var OnState = model.Entity.IsNew;

            var llaves = ModelState.Where(x => x.Key.Contains("Entity.Admisiones.")).Select(x => x.Key).ToList();
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
                    model.Entity.CreationDate = DateTime.Now;
                    model.Entity.CreatedBy = User.Identity.Name;
                    model.Entity = Manager().AtencionesBusinessLogic().AddAtencion(model.Entity);
                    model.Entity.IsNew = false;
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

            model.Entity = Manager().GetBusinessLogic<Atenciones>().Tabla(true)
                .Include(x => x.Admisiones.ProgramacionCitas.Entidades)
                .Include(x => x.Admisiones.Pacientes.TiposIdentificacion)
                .Include(x => x.Admisiones.Pacientes.Generos)
                .Include(x => x.Admisiones.ProgramacionCitas.Servicios)
                .Include(x => x.Admisiones.ProgramacionCitas.Servicios.TiposServicios)
                .FirstOrDefault(x => x.Id == model.Entity.Id);

            return model; 
        } 

        [HttpPost]
        public IActionResult Delete(AtencionesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private AtencionesModel DeleteModel(AtencionesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AtencionesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Atenciones>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Atenciones>().Remove(model.Entity); 
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

        private AtencionesModel NewModelDetail(long IdFather) 
        { 
            AtencionesModel model = new AtencionesModel(); 
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
        public IActionResult EditDetail(AtencionesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(AtencionesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private AtencionesModel DeleteModelDetail(AtencionesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AtencionesModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Atenciones>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Atenciones>().Remove(model.Entity); 
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
             Atenciones entity = new Atenciones(); 
             JsonConvert.PopulateObject(values, entity); 
             AtencionesModel model = new AtencionesModel(); 
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
             Atenciones entity = Manager().GetBusinessLogic<Atenciones>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             AtencionesModel model = new AtencionesModel(); 
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
             Atenciones entity = Manager().GetBusinessLogic<Atenciones>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Atenciones>().Remove(entity); 
        } 

        */
        #endregion

        #region Datasource Combobox Foraneos 
        [HttpPost]
        public LoadResult GetHCTiposId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HCTipos>().Tabla(), loadOptions);
        }
        [HttpPost]
        public LoadResult GetAdmisionesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Admisiones>().Tabla().Where(x=>x.EstadosId == 10066), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEmpleadosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empleados>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEstadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true).Where(x=>x.Tipo == "ATENCION"), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEstadosAdmisionesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetPacientesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Pacientes>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetCitasId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ProgramacionCitas>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetConveniosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Convenios>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetFilialesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Filiales>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetDiagnosticosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Diagnosticos>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetFinalidadConsultaId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<FinalidadConsulta>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetFinalidadProcedimientoId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<FinalidadProcedimiento>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetCausasExternasId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<CausasExternas>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetAmbitoRealizacionProcedimientoId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<AmbitoRealizacionProcedimiento>().Tabla(true), loadOptions);
        }
        #endregion

        [HttpPost]
        public IActionResult AnularAtencion(long Id,string DetalleAnulacion)
        {
            AtencionesModel model = new AtencionesModel();
            model.Entity = Manager().GetBusinessLogic<Atenciones>().FindById(x => x.Id == Id, true);
            model.Entity.DetalleAnulacion = DetalleAnulacion;
            model.Entity.LastUpdate = DateTime.Now;
            model.Entity.UpdatedBy = User.Identity.Name;
            try
            {
                model.Entity = Manager().AtencionesBusinessLogic().AnularAtencion(model.Entity);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage());
            }

            model.Entity.Admisiones = Manager().GetBusinessLogic<Admisiones>().Tabla(true)
                .Include(x => x.ProgramacionCitas)
                .Include(x => x.ProgramacionCitas.Servicios)
                .Include(x => x.ProgramacionCitas.Servicios.TiposServicios)
                .FirstOrDefault(x => x.Id == model.Entity.AdmisionesId);

            return PartialView("Edit", model);
        }

        [HttpPost]
        public LoadResult GetPersonas(DataSourceLoadOptions loadOptions)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(Manager().settings);
            var aten = unitOfWork.Repository<Atenciones>().GetTable(true);

            var data = from a in aten
                       select new Atenciones_Personas { Atenciones = a };

            return DataSourceLoader.Load(data, loadOptions);
        }

        [HttpGet]
        public ActionResult VerCitasProgramadas()
        {
            try
            {
                Empleados empleado = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId == this.ActualUsuarioId(), false);
                if (empleado == null)
                    return new BadRequestObjectResult(DApp.GetResource("BLL.Atenciones.ErrorEmpleadoUsuario"));
                SchedulerModel schedulerModel = Manager().AtencionesBusinessLogic().VerCitasProgramadas(empleado);
                return PartialView("SchedulerVerCitas", schedulerModel);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

        [HttpGet]
        public ActionResult VerListaCitasProgramadas()
        {
            try
            {
                Empleados empleado = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId == this.ActualUsuarioId(), false);
                if (empleado == null)
                    return new BadRequestObjectResult(DApp.GetResource("BLL.Atenciones.ErrorEmpleadoUsuario"));
                return PartialView("ListProgramacionCitas");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

        [HttpPost]
        public LoadResult GetListaCitasProgramadas(DataSourceLoadOptions loadOptions)
        {
            Empleados empleado = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId == this.ActualUsuarioId(), false);
            if (empleado == null)
                throw new Exception(DApp.GetResource("BLL.Atenciones.ErrorEmpleadoUsuario"));
            List<long> estados = new List<long> { 3 };
            var result = Manager().GetBusinessLogic<ProgramacionCitas>().Tabla(true).Where(x => x.EmpleadosId == empleado.Id && estados.Contains(x.EstadosId));
            return DataSourceLoader.Load(result, loadOptions);
        }

        [HttpGet]
        public IActionResult ImprimirNotaProcedimiento(long id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { id };

                AtencionNotaProcedimientosReporte report = new AtencionNotaProcedimientosReporte();
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
