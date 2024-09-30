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
using DevExpress.XtraReports.UI;
using WebApp.Reportes.OrdenesServicios;
using Dominus.Backend.Application;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class OrdenesServiciosController : BaseAppController
    {

        //private const string Prefix = "OrdenesServicios"; 

        public OrdenesServiciosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<OrdenesServicios>().Tabla(true), loadOptions);
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

        private OrdenesServiciosModel NewModel() 
        { 
            OrdenesServiciosModel model = new OrdenesServiciosModel();
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

        private OrdenesServiciosModel EditModel(long Id) 
        { 
            OrdenesServiciosModel model = new OrdenesServiciosModel();
            model.Entity = Manager().GetBusinessLogic<OrdenesServicios>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(OrdenesServiciosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private OrdenesServiciosModel EditModel(OrdenesServiciosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<OrdenesServicios>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<OrdenesServicios>().Modify(model.Entity); 
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
        public IActionResult Delete(OrdenesServiciosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private OrdenesServiciosModel DeleteModel(OrdenesServiciosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            OrdenesServiciosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<OrdenesServicios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<OrdenesServicios>().Remove(model.Entity); 
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

        private OrdenesServiciosModel NewModelDetail(long IdFather) 
        { 
            OrdenesServiciosModel model = new OrdenesServiciosModel(); 
            model.Entity.HIstoriasClinicasId = IdFather;
            var hc = Manager().GetBusinessLogic<HistoriasClinicas>().FindById(x => x.Id == IdFather, true);
            model.Entity.PacientesId = hc.PacientesId;
            model.Entity.ProfesionalId = hc.ProfesionalId;
            model.Entity.NroOrden = long.Parse(DateTime.Now.ToString("yyyyMMddHH24mmss"));
            model.Entity.IsNew = true;
            model.Entity.Fecha = DateTime.Now;
            model.Entity.FechaVencimiento = DateTime.Now.AddDays(90);
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(OrdenesServiciosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(OrdenesServiciosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private OrdenesServiciosModel DeleteModelDetail(OrdenesServiciosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            OrdenesServiciosModel newModel = NewModelDetail(model.Entity.HIstoriasClinicasId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<OrdenesServicios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<OrdenesServicios>().Remove(model.Entity); 
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
             OrdenesServicios entity = new OrdenesServicios(); 
             JsonConvert.PopulateObject(values, entity); 
             OrdenesServiciosModel model = new OrdenesServiciosModel(); 
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
             OrdenesServicios entity = Manager().GetBusinessLogic<OrdenesServicios>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             OrdenesServiciosModel model = new OrdenesServiciosModel(); 
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
             OrdenesServicios entity = Manager().GetBusinessLogic<OrdenesServicios>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<OrdenesServicios>().Remove(entity); 
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
        public IActionResult ImprimirOrdenServicioPorId(int Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { Id };
                var user = Manager().GetBusinessLogic<User>().FindById(x => x.UserName == User.Identity.Name, false);
                informacionReporte.ParametrosAdicionales.Add("P_UsuarioGenero", $"{user.UserName} | {user.Names} {user.LastNames}");

                OrdenesServiciosReporte report = new OrdenesServiciosReporte();
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
