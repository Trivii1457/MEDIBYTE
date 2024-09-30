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
using System.Collections.Generic;
using Dominus.Backend.Application;
using DevExpress.XtraReports.UI;
using WebApp.Reportes.LiquidacionHonorarios;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class LiquidacionHonorariosController : BaseAppController
    {

        //private const string Prefix = "LiquidacionHonorarios"; 

        public LiquidacionHonorariosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<LiquidacionHonorarios>().Tabla(true), loadOptions);
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

        private LiquidacionHonorariosModel NewModel()
        {
            LiquidacionHonorariosModel model = new LiquidacionHonorariosModel();
            model.Entity.IsNew = true;
            model.Entity.EmpresasId = this.ActualEmpresaId();
            model.Entity.FechaInicio = DateTime.Now;
            model.Entity.FechaFinal = DateTime.Now;
            model.Entity.EstadosId = 10064;
            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private LiquidacionHonorariosModel EditModel(long Id)
        {
            LiquidacionHonorariosModel model = new LiquidacionHonorariosModel();
            model.Entity = Manager().GetBusinessLogic<LiquidacionHonorarios>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            model.LiquidacionHonorariosDetalle = Manager().LiquidacionHonorariosLogic().GetDetalleALiquidar(Id);
            return model;
        }

        [HttpPost]
        public IActionResult Edit(LiquidacionHonorariosModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        private LiquidacionHonorariosModel EditModel(LiquidacionHonorariosModel model)
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
                        model.Entity = Manager().GetBusinessLogic<LiquidacionHonorarios>().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        model.Entity = Manager().GetBusinessLogic<LiquidacionHonorarios>().Modify(model.Entity);
                    }
                    model.LiquidacionHonorariosDetalle = Manager().LiquidacionHonorariosLogic().GetDetalleALiquidar(model.Entity.Id);
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
        public IActionResult Delete(LiquidacionHonorariosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private LiquidacionHonorariosModel DeleteModel(LiquidacionHonorariosModel model)
        {
            ViewBag.Accion = "Delete";
            LiquidacionHonorariosModel newModel = NewModel();

            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<LiquidacionHonorarios>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<LiquidacionHonorarios>().Remove(model.Entity);
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

        private LiquidacionHonorariosModel NewModelDetail(long IdFather) 
        { 
            LiquidacionHonorariosModel model = new LiquidacionHonorariosModel(); 
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
        public IActionResult EditDetail(LiquidacionHonorariosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(LiquidacionHonorariosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private LiquidacionHonorariosModel DeleteModelDetail(LiquidacionHonorariosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            LiquidacionHonorariosModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<LiquidacionHonorarios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<LiquidacionHonorarios>().Remove(model.Entity); 
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
             LiquidacionHonorarios entity = new LiquidacionHonorarios(); 
             JsonConvert.PopulateObject(values, entity); 
             LiquidacionHonorariosModel model = new LiquidacionHonorariosModel(); 
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
             LiquidacionHonorarios entity = Manager().GetBusinessLogic<LiquidacionHonorarios>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             LiquidacionHonorariosModel model = new LiquidacionHonorariosModel(); 
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
             LiquidacionHonorarios entity = Manager().GetBusinessLogic<LiquidacionHonorarios>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<LiquidacionHonorarios>().Remove(entity); 
        } 

        */
        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetEmpleadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empleados>().Tabla(true).Where(x => x.TipoEmpleados == 2), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEmpresasId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empresas>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEstadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        }
        #endregion

        [HttpPost]
        public IActionResult Liquidar([FromBody] List<LiquidacionHonorariosDetalle> models)
        {
            try
            {
                if (models.Count <= 0)
                    throw new Exception("Debe escoger al menos una liquidación");

                long liquidacionId = Manager().LiquidacionHonorariosLogic().Liquidar(models, User.Identity.Name);
                return Edit(liquidacionId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AnularLiquidacion(long Id, string DetalleAnulacion)
        {
            try
            {
                var liquidacion = Manager().GetBusinessLogic<LiquidacionHonorarios>().FindById(x => x.Id == Id, true);
                liquidacion.EstadosId = 10065;
                liquidacion.DetalleAnulacion = DetalleAnulacion;
                liquidacion = Manager().GetBusinessLogic<LiquidacionHonorarios>().Modify(liquidacion);
                return Edit(Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet]
        public IActionResult ImprimirLiquidacionHonorariosId(long Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { Id };
                var user = Manager().GetBusinessLogic<User>().FindById(x => x.UserName == User.Identity.Name, false);
                informacionReporte.ParametrosAdicionales.Add("P_UsuarioGenero", $"{user.UserName} | {user.Names} {user.LastNames}");

                LiquidacionHonorariosReporte report = new LiquidacionHonorariosReporte();
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
