using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExpress.Data.ODataLinq.Helpers;
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
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using WebApp.Reportes.HistoriasClinicas;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class HistoriasClinicasController : BaseAppController
    {

        //private const string Prefix = "HistoriasClinicas"; 

        public HistoriasClinicasController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(true).Include(x=> x.HCTipos).Include(x=>x.HCTipos.Especialidades), loadOptions);
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

        private HistoriasClinicasModel NewModel() 
        { 
            HistoriasClinicasModel model = new HistoriasClinicasModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private HistoriasClinicasModel EditModel(long Id) 
        { 
            HistoriasClinicasModel model = new HistoriasClinicasModel();
            model.Entity = GetEntityData(Id);
            model.Preguntas = Manager().GetBusinessLogic<HCTiposPreguntas>().FindAll(x => x.HCTiposId == model.Entity.HCTiposId, true).Select(x => x.HCPreguntas).ToList();
            model.Entity.IsNew = false;
            model.EsMismoUsuario = model.Entity.CreatedBy == User.Identity.Name;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(HistoriasClinicasModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private HistoriasClinicasModel EditModel(HistoriasClinicasModel model) 
        {
            if (GetEstado().Nombre == "ABIERTA")
            {
                ViewBag.Accion = "Save";
                var OnState = model.Entity.IsNew;

                var keyPacientes = ModelState.Where(x => x.Key.StartsWith("Entity.Atenciones")).Select(x => x.Key).ToList();
                foreach (var key in keyPacientes)
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
                            model.Entity.CreationDate = DateTime.Now;
                            model.Entity.CreatedBy = User.Identity.Name;
                            model.Entity = Manager().GetBusinessLogic<HistoriasClinicas>().Add(model.Entity);
                            model.Entity.IsNew = false;
                        }
                        else
                        {
                            model.Entity = Manager().GetBusinessLogic<HistoriasClinicas>().Modify(model.Entity);
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
            }
            else
            {
                ModelState.AddModelError("Entity.Id", "La historia clinica esta cerrada, por lo tanto no se puede modificar");
            }
            model.Entity = GetEntityData(model.Entity.Id);
            model.EsMismoUsuario = model.Entity.CreatedBy == User.Identity.Name;
            model.Preguntas = Manager().GetBusinessLogic<HCTiposPreguntas>().FindAll(x => x.HCTiposId == model.Entity.HCTiposId, true).Select(x => x.HCPreguntas).ToList();
            return model; 
        } 

        [HttpPost]
        public IActionResult Delete(HistoriasClinicasModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private HistoriasClinicasModel DeleteModel(HistoriasClinicasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            HistoriasClinicasModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<HistoriasClinicas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<HistoriasClinicas>().Remove(model.Entity); 
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

        private HistoriasClinicasModel NewModelDetail(long IdFather) 
        { 
            HistoriasClinicasModel model = new HistoriasClinicasModel(); 
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
        public IActionResult EditDetail(HistoriasClinicasModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(HistoriasClinicasModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private HistoriasClinicasModel DeleteModelDetail(HistoriasClinicasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            HistoriasClinicasModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<HistoriasClinicas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<HistoriasClinicas>().Remove(model.Entity); 
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
             HistoriasClinicas entity = new HistoriasClinicas(); 
             JsonConvert.PopulateObject(values, entity); 
             HistoriasClinicasModel model = new HistoriasClinicasModel(); 
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
             HistoriasClinicas entity = Manager().GetBusinessLogic<HistoriasClinicas>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             HistoriasClinicasModel model = new HistoriasClinicasModel(); 
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
             HistoriasClinicas entity = Manager().GetBusinessLogic<HistoriasClinicas>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<HistoriasClinicas>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetAdmisionesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Admisiones>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetProfesionalId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empleados>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEstadosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().FindAll(x=> x.Tipo == "HISTORIA CLINICA"), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetHCTiposId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HCTipos>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetHistoriaClinicaPrincipal(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetPacientesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Pacientes>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetDominanciaEstadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true).Where(x=>x.Tipo.Equals("DOMINANCIA")), loadOptions);
        }
        #endregion

        [HttpGet]
        public IActionResult ImprimirHCPaciente(long PacienteId)
        {
            try
            {
                var existePaciente = Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(false).FirstOrDefault(x=>x.PacientesId == PacienteId);
                if (existePaciente == null)
                {
                    return new BadRequestObjectResult("El paciente no tiene una historia clinica registrada.");
                }

                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = Manager().GetBusinessLogic<HistoriasClinicas>().FindAll(x => x.PacientesId == PacienteId, false).Select(x=>x.Id).ToArray();
                var user = Manager().GetBusinessLogic<User>().FindById(x => x.UserName == User.Identity.Name,false);
                informacionReporte.ParametrosAdicionales.Add("P_UsuarioGenero", $"{user.UserName} | {user.Names} {user.LastNames}");

                HistoriasClinicasReporte report = new HistoriasClinicasReporte();
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
        public IActionResult ImprimirDocumentosAPacientes(int Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { Id };
                var user = Manager().GetBusinessLogic<User>().FindById(x => x.UserName == User.Identity.Name, false);
                informacionReporte.ParametrosAdicionales.Add("P_UsuarioGenero", $"{user.UserName} | {user.Names} {user.LastNames}");

                DocumentosAPacientesReporte report = new DocumentosAPacientesReporte();
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
        public IActionResult ImprimirHCPorId(int Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { Id };
                var user = Manager().GetBusinessLogic<User>().FindById(x => x.UserName == User.Identity.Name, false);
                informacionReporte.ParametrosAdicionales.Add("P_UsuarioGenero", $"{user.UserName} | {user.Names} {user.LastNames}");

                HistoriasClinicasReporte report = new HistoriasClinicasReporte();
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
        public IActionResult ModificarDatosAtencion(long id,long causaExternaId, long finalidadConsultaId, bool esControl)
        {
            try
            {
                HistoriasClinicas entity = Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(true).FirstOrDefault(x => x.Id == id);
                entity.Atenciones.CausasExternasId = causaExternaId;
                entity.Atenciones.FinalidadConsultaId = finalidadConsultaId;
                entity.Atenciones.UpdatedBy = User.Identity.Name;
                entity.Atenciones.LastUpdate = DateTime.Now;
                Manager().GetBusinessLogic<Atenciones>().Modify(entity.Atenciones);

                Admisiones admision = Manager().GetBusinessLogic<Admisiones>().FindById(x => x.Id == entity.Atenciones.AdmisionesId,false);
                admision.EsControl = esControl;
                Manager().GetBusinessLogic<Admisiones>().Modify(admision);

                entity.EsControl = admision.EsControl;
                Manager().GetBusinessLogic<HistoriasClinicas>().Modify(entity);

                return Edit(id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

    }
}
