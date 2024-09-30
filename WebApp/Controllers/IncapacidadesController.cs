using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Frontend.Controllers;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Newtonsoft.Json;
using Blazor.BusinessLogic;
using DevExpress.Office.Crypto;
using Dominus.Backend.Application;
using DevExpress.XtraReports.UI;
using WebApp.Reportes.Incapacidades;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class IncapacidadesController : BaseAppController
    {

        //private const string Prefix = "Incapacidades"; 

        public IncapacidadesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Incapacidades>().Tabla(true), loadOptions);
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

        private IncapacidadesModel NewModel() 
        { 
            IncapacidadesModel model = new IncapacidadesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private IncapacidadesModel EditModel(long Id) 
        { 
            IncapacidadesModel model = new IncapacidadesModel();
            model.Entity = Manager().GetBusinessLogic<Incapacidades>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(IncapacidadesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private IncapacidadesModel EditModel(IncapacidadesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<Incapacidades>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<Incapacidades>().Modify(model.Entity); 
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
        public IActionResult Delete(IncapacidadesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private IncapacidadesModel DeleteModel(IncapacidadesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            IncapacidadesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Incapacidades>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Incapacidades>().Remove(model.Entity); 
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

        [HttpGet]
        public IActionResult NewDetail(long IdFather)
        {
            return PartialView("EditDetail", NewModelDetail(IdFather));
        }

        private IncapacidadesModel NewModelDetail(long IdFather) 
        { 
            IncapacidadesModel model = new IncapacidadesModel(); 
            model.Entity.HIstoriasClinicasId = IdFather;
            var hc = Manager().GetBusinessLogic<HistoriasClinicas>().FindById(x => x.Id == IdFather, true);
            model.Entity.PacientesId = hc.PacientesId;
            model.Entity.ProfesionalId = hc.ProfesionalId;
            model.Entity.NroOrden = long.Parse(DateTime.Now.ToString("yyyyMMddHH24mmss"));
            model.Entity.IsNew = true;
            model.Entity.Fecha = DateTime.Now;
            model.Entity.FechaInicio = DateTime.Now;
            model.Entity.FechaFinalizacion = DateTime.Now;
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(IncapacidadesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(IncapacidadesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private IncapacidadesModel DeleteModelDetail(IncapacidadesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            IncapacidadesModel newModel = NewModelDetail(model.Entity.HIstoriasClinicasId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Incapacidades>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Incapacidades>().Remove(model.Entity); 
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
             Incapacidades entity = new Incapacidades(); 
             JsonConvert.PopulateObject(values, entity); 
             IncapacidadesModel model = new IncapacidadesModel(); 
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
             Incapacidades entity = Manager().GetBusinessLogic<Incapacidades>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             IncapacidadesModel model = new IncapacidadesModel(); 
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
             Incapacidades entity = Manager().GetBusinessLogic<Incapacidades>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Incapacidades>().Remove(entity); 
        } 

        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetDiagnosticosId(DataSourceLoadOptions loadOptions, long hcId)
        {
            var diagnostivosId = Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().Tabla(true).Where(x => x.HistoriasClinicasId == hcId).Select(x => x.DiagnosticosId).ToList();
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Diagnosticos>().Tabla(true).Where(x=> diagnostivosId.Contains(x.Id)), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetProfesionalId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empleados>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetHIstoriasClinicasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetPacientesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Pacientes>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetIncapacidadesOrigenesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<IncapacidadesOrigenes>().Tabla(true), loadOptions);
        }
        #endregion

        [HttpGet]
        public IActionResult ImprimirIncapacidadesPorId(int Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { Id };
                var user = Manager().GetBusinessLogic<User>().FindById(x => x.UserName == User.Identity.Name, false);
                informacionReporte.ParametrosAdicionales.Add("P_UsuarioGenero", $"{user.UserName} | {user.Names} {user.LastNames}");

                IncapacidadesReporte report = new IncapacidadesReporte();
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
