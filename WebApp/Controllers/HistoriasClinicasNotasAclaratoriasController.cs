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
using DevExpress.Office.Drawing;
using Dominus.Backend.Application;
using WebApp.Reportes.HistoriaClinicasNotasAclaratorias;
using DevExpress.XtraReports.UI;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class HistoriasClinicasNotasAclaratoriasController : BaseAppController
    {

        //private const string Prefix = "HistoriasClinicasNotasAclaratorias"; 

        public HistoriasClinicasNotasAclaratoriasController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HistoriasClinicasNotasAclaratorias>().Tabla(true), loadOptions);
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

        private HistoriasClinicasNotasAclaratoriasModel NewModel() 
        { 
            HistoriasClinicasNotasAclaratoriasModel model = new HistoriasClinicasNotasAclaratoriasModel();
            var empleado = Manager().GetBusinessLogic<Empleados>().FindById(x => x.UserId.ToString() == User.FindFirst("UserId").Value, false);
            model.Entity.ProfesionalId = empleado.Id;
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private HistoriasClinicasNotasAclaratoriasModel EditModel(long Id) 
        { 
            HistoriasClinicasNotasAclaratoriasModel model = new HistoriasClinicasNotasAclaratoriasModel();
            model.Entity = Manager().GetBusinessLogic<HistoriasClinicasNotasAclaratorias>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(HistoriasClinicasNotasAclaratoriasModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private HistoriasClinicasNotasAclaratoriasModel EditModel(HistoriasClinicasNotasAclaratoriasModel model) 
        { 
            ViewBag.Accion = "Save"; 
            var OnState = model.Entity.IsNew;

            var hc = Manager().GetBusinessLogic<HistoriasClinicas>().FindById(x => x.Id == model.Entity.HistoriasClinicasId,false);
            if (hc.CreatedBy != User.Identity.Name)
            {
                ModelState.AddModelError("Entity.Id", $"Solo el empleado que creo la historia clinica {hc.Consecutivo} puede agregar notas aclaratorias.");
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
                        model.Entity = Manager().GetBusinessLogic<HistoriasClinicasNotasAclaratorias>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<HistoriasClinicasNotasAclaratorias>().Modify(model.Entity); 
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
        public IActionResult Delete(HistoriasClinicasNotasAclaratoriasModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private HistoriasClinicasNotasAclaratoriasModel DeleteModel(HistoriasClinicasNotasAclaratoriasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            HistoriasClinicasNotasAclaratoriasModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<HistoriasClinicasNotasAclaratorias>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<HistoriasClinicasNotasAclaratorias>().Remove(model.Entity); 
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

        private HistoriasClinicasNotasAclaratoriasModel NewModelDetail(long IdFather) 
        { 
            HistoriasClinicasNotasAclaratoriasModel model = new HistoriasClinicasNotasAclaratoriasModel(); 
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
        public IActionResult EditDetail(HistoriasClinicasNotasAclaratoriasModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(HistoriasClinicasNotasAclaratoriasModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private HistoriasClinicasNotasAclaratoriasModel DeleteModelDetail(HistoriasClinicasNotasAclaratoriasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            HistoriasClinicasNotasAclaratoriasModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<HistoriasClinicasNotasAclaratorias>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<HistoriasClinicasNotasAclaratorias>().Remove(model.Entity); 
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
             HistoriasClinicasNotasAclaratorias entity = new HistoriasClinicasNotasAclaratorias(); 
             JsonConvert.PopulateObject(values, entity); 
             HistoriasClinicasNotasAclaratoriasModel model = new HistoriasClinicasNotasAclaratoriasModel(); 
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
             HistoriasClinicasNotasAclaratorias entity = Manager().GetBusinessLogic<HistoriasClinicasNotasAclaratorias>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             HistoriasClinicasNotasAclaratoriasModel model = new HistoriasClinicasNotasAclaratoriasModel(); 
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
             HistoriasClinicasNotasAclaratorias entity = Manager().GetBusinessLogic<HistoriasClinicasNotasAclaratorias>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<HistoriasClinicasNotasAclaratorias>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetProfesionalId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empleados>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetHistoriasClinicasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(true), loadOptions);
        }
        #endregion

        [HttpGet]
        public IActionResult ImprimirHCNAPorId(int Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { Id };
                var user = Manager().GetBusinessLogic<User>().FindById(x => x.UserName == User.Identity.Name, false);
                informacionReporte.ParametrosAdicionales.Add("P_UsuarioGenero", $"{user.UserName} | {user.Names} {user.LastNames}");

                HistoriaClinicasNotasAclaratoriasReporte report = new HistoriaClinicasNotasAclaratoriasReporte();
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
