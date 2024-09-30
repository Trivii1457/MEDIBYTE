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
using Microsoft.EntityFrameworkCore;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class LiquidacionHonorariosDetalleController : BaseAppController
    {

        //private const string Prefix = "LiquidacionHonorariosDetalle"; 

        public LiquidacionHonorariosDetalleController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            var result = Manager().GetBusinessLogic<LiquidacionHonorariosDetalle>().Tabla(true)
                .Include(x => x.AdmisionesServiciosPrestados.Servicios)
                .Include(x => x.AdmisionesServiciosPrestados.Atenciones)
                .Include(x => x.AdmisionesServiciosPrestados.Admisiones)
                .Include(x => x.AdmisionesServiciosPrestados.Admisiones.Pacientes)
                .Include(x => x.AdmisionesServiciosPrestados.Admisiones.ProgramacionCitas)
                .Include(x => x.AtencionesResultado);
            return DataSourceLoader.Load(result, loadOptions);
        }

        [HttpPost]
        public IActionResult GetDetalleALiquidar(DataSourceLoadOptions loadOptions, long liquidacionId)
        {
            try
            {
                List<LiquidacionHonorariosDetalle> result = Manager().LiquidacionHonorariosLogic().GetDetalleALiquidar(liquidacionId);
                return Ok(DataSourceLoader.Load(result, loadOptions));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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

        private LiquidacionHonorariosDetalleModel NewModel()
        {
            LiquidacionHonorariosDetalleModel model = new LiquidacionHonorariosDetalleModel();
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private LiquidacionHonorariosDetalleModel EditModel(long Id)
        {
            LiquidacionHonorariosDetalleModel model = new LiquidacionHonorariosDetalleModel();
            model.Entity = Manager().GetBusinessLogic<LiquidacionHonorariosDetalle>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model;
        }

        [HttpPost]
        public IActionResult Edit(LiquidacionHonorariosDetalleModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        private LiquidacionHonorariosDetalleModel EditModel(LiquidacionHonorariosDetalleModel model)
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
                        model.Entity = Manager().GetBusinessLogic<LiquidacionHonorariosDetalle>().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        model.Entity = Manager().GetBusinessLogic<LiquidacionHonorariosDetalle>().Modify(model.Entity);
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
        public IActionResult Delete(LiquidacionHonorariosDetalleModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private LiquidacionHonorariosDetalleModel DeleteModel(LiquidacionHonorariosDetalleModel model)
        {
            ViewBag.Accion = "Delete";
            LiquidacionHonorariosDetalleModel newModel = NewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<LiquidacionHonorariosDetalle>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<LiquidacionHonorariosDetalle>().Remove(model.Entity);
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

        private LiquidacionHonorariosDetalleModel NewModelDetail(long IdFather)
        {
            LiquidacionHonorariosDetalleModel model = new LiquidacionHonorariosDetalleModel();
            model.Entity.LiquidacionHonorariosId = IdFather;
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(LiquidacionHonorariosDetalleModel model)
        {
            return PartialView("EditDetail", EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(LiquidacionHonorariosDetalleModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private LiquidacionHonorariosDetalleModel DeleteModelDetail(LiquidacionHonorariosDetalleModel model)
        {
            ViewBag.Accion = "Delete";
            LiquidacionHonorariosDetalleModel newModel = NewModelDetail(model.Entity.LiquidacionHonorariosId);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<LiquidacionHonorariosDetalle>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<LiquidacionHonorariosDetalle>().Remove(model.Entity);
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
            LiquidacionHonorariosDetalle entity = new LiquidacionHonorariosDetalle();
            JsonConvert.PopulateObject(values, entity);
            LiquidacionHonorariosDetalleModel model = new LiquidacionHonorariosDetalleModel();
            model.Entity = entity;
            model.Entity.IsNew = true;
            this.EditModel(model);
            if (ModelState.IsValid)
                return Ok(ModelState);
            else
                return BadRequest(ModelState);
        }

        [HttpPost]
        public IActionResult ModifyInGrid(int key, string values)
        {
            LiquidacionHonorariosDetalle entity = Manager().GetBusinessLogic<LiquidacionHonorariosDetalle>().FindById(x => x.Id == key, false);
            JsonConvert.PopulateObject(values, entity);
            LiquidacionHonorariosDetalleModel model = new LiquidacionHonorariosDetalleModel();
            model.Entity = entity;
            model.Entity.IsNew = false;
            this.EditModel(model);
            if (ModelState.IsValid)
                return Ok(ModelState);
            else
                return BadRequest(ModelState);
        }

        [HttpPost]
        public void DeleteInGrid(int key)
        {
            LiquidacionHonorariosDetalle entity = Manager().GetBusinessLogic<LiquidacionHonorariosDetalle>().FindById(x => x.Id == key, false);
            Manager().GetBusinessLogic<LiquidacionHonorariosDetalle>().Remove(entity);
        }

        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetAdmisionesServiciosPrestadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEmpleadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empleados>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetLiquidacionHonorariosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<LiquidacionHonorarios>().Tabla(true), loadOptions);
        }
        #endregion

    }
}
