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
using DevExpress.Web;
using Dominus.Backend.Application;
using DevExpress.XtraReports.UI;
using WebApp.Reportes.OrdenesMedicamentos;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class OrdenesMedicamentosController : BaseAppController
    {

        //private const string Prefix = "OrdenesMedicamentos"; 

        public OrdenesMedicamentosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<OrdenesMedicamentos>().Tabla(true), loadOptions);
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

        private OrdenesMedicamentosModel NewModel() 
        { 
            OrdenesMedicamentosModel model = new OrdenesMedicamentosModel();
            model.Entity.IsNew = true;
            model.Entity.Fecha = DateTime.Now;
            model.Entity.FechaVencimiento = DateTime.Now.AddDays(90);
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private OrdenesMedicamentosModel EditModel(long Id) 
        { 
            OrdenesMedicamentosModel model = new OrdenesMedicamentosModel();
            model.Entity = Manager().GetBusinessLogic<OrdenesMedicamentos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(OrdenesMedicamentosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private OrdenesMedicamentosModel EditModel(OrdenesMedicamentosModel model) 
        { 
            ViewBag.Accion = "Save"; 
            var OnState = model.Entity.IsNew;

            var totalDiagonisticos = Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().Tabla(false).Count(x => x.HistoriasClinicasId == model.Entity.HIstoriasClinicasId);
            if (totalDiagonisticos <= 0)
            {
                ModelState.AddModelError("Entity.Id", "Debe ingresar al menos un diagnostico primero.");
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
                        model.Entity = Manager().GetBusinessLogic<OrdenesMedicamentos>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<OrdenesMedicamentos>().Modify(model.Entity); 
                    } 
                } 
                catch (Exception e) 
                { 
                    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage()); 
                } 
            } 
            return model; 
        } 

        [HttpPost]
        public IActionResult Delete(OrdenesMedicamentosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private OrdenesMedicamentosModel DeleteModel(OrdenesMedicamentosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            OrdenesMedicamentosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<OrdenesMedicamentos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<OrdenesMedicamentos>().Remove(model.Entity); 
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

        private OrdenesMedicamentosModel NewModelDetail(long IdFather) 
        { 
            OrdenesMedicamentosModel model = new OrdenesMedicamentosModel(); 

            model.Entity.HIstoriasClinicasId = IdFather; 
            var hc = Manager().GetBusinessLogic<HistoriasClinicas>().FindById(x => x.Id == IdFather, true);
            model.Entity.PacientesId = hc.PacientesId;
            model.Entity.ProfesionalId = hc.ProfesionalId;
            model.Entity.NroOrden = long.Parse( DateTime.Now.ToString("yyyyMMddHH24mmss"));
            model.Entity.Fecha = DateTime.Now;
            model.Entity.FechaVencimiento = DateTime.Now.AddDays(90);
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(OrdenesMedicamentosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(OrdenesMedicamentosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private OrdenesMedicamentosModel DeleteModelDetail(OrdenesMedicamentosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            OrdenesMedicamentosModel newModel = NewModelDetail(model.Entity.HIstoriasClinicasId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<OrdenesMedicamentos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<OrdenesMedicamentos>().Remove(model.Entity); 
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
             OrdenesMedicamentos entity = new OrdenesMedicamentos(); 
             JsonConvert.PopulateObject(values, entity); 
             OrdenesMedicamentosModel model = new OrdenesMedicamentosModel(); 
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
             OrdenesMedicamentos entity = Manager().GetBusinessLogic<OrdenesMedicamentos>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             OrdenesMedicamentosModel model = new OrdenesMedicamentosModel(); 
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
             OrdenesMedicamentos entity = Manager().GetBusinessLogic<OrdenesMedicamentos>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<OrdenesMedicamentos>().Remove(entity); 
        } 

        #endregion 

        #region Datasource Combobox Foraneos 

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
        #endregion

        [HttpGet]
        public IActionResult ImprimirOrdenMedicamentoPorId(int Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { Id };
                var user = Manager().GetBusinessLogic<User>().FindById(x => x.UserName == User.Identity.Name, false);
                informacionReporte.ParametrosAdicionales.Add("P_UsuarioGenero", $"{user.UserName} | {user.Names} {user.LastNames}");

                OrdenesMedicamentosReporte report = new OrdenesMedicamentosReporte();
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
