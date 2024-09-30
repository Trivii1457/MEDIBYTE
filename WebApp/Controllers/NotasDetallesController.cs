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

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class NotasDetallesController : BaseAppController
    {

        //private const string Prefix = "NotasDetalles"; 

        public NotasDetallesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<NotasDetalles>().Tabla(true), loadOptions);
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

        private NotasDetallesModel NewModel()
        {
            NotasDetallesModel model = new NotasDetallesModel();
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private NotasDetallesModel EditModel(long Id)
        {
            NotasDetallesModel model = new NotasDetallesModel();
            model.Entity = Manager().GetBusinessLogic<NotasDetalles>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model;
        }

        [HttpPost]
        public IActionResult Edit(NotasDetallesModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        private NotasDetallesModel EditModel(NotasDetallesModel model)
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
                        model.Entity = Manager().GetBusinessLogic<NotasDetalles>().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        model.Entity = Manager().GetBusinessLogic<NotasDetalles>().Modify(model.Entity);
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
        public IActionResult Delete(NotasDetallesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private NotasDetallesModel DeleteModel(NotasDetallesModel model)
        {
            ViewBag.Accion = "Delete";
            NotasDetallesModel newModel = NewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<NotasDetalles>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<NotasDetalles>().Remove(model.Entity);
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

        private NotasDetallesModel NewModelDetail(long IdFather)
        {
            NotasDetallesModel model = new NotasDetallesModel();
            model.Entity.NotasId = IdFather;
            model.Entity.IsNew = true;
            model.Entity.NroLinea = Manager().GetBusinessLogic<NotasDetalles>().FindAll(x => x.NotasId == IdFather, false).Count() + 1;
            return model;
        }

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(NotasDetallesModel model)
        {
            return PartialView("EditDetail", EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(NotasDetallesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private NotasDetallesModel DeleteModelDetail(NotasDetallesModel model)
        {
            ViewBag.Accion = "Delete";
            NotasDetallesModel newModel = NewModelDetail(model.Entity.NotasId);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<NotasDetalles>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<NotasDetalles>().Remove(model.Entity);
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
            NotasDetalles entity = new NotasDetalles();
            JsonConvert.PopulateObject(values, entity);
            NotasDetallesModel model = new NotasDetallesModel();
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
            NotasDetalles entity = Manager().GetBusinessLogic<NotasDetalles>().FindById(x => x.Id == key, false);
            JsonConvert.PopulateObject(values, entity);
            NotasDetallesModel model = new NotasDetallesModel();
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
            NotasDetalles entity = Manager().GetBusinessLogic<NotasDetalles>().FindById(x => x.Id == key, false);
            Manager().GetBusinessLogic<NotasDetalles>().Remove(entity);
        }


        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetNotasId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Notas>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetServiciosId(DataSourceLoadOptions loadOptions, long notasId)
        {
            var nota = Manager().GetBusinessLogic<Notas>().Tabla(true).FirstOrDefault(x => x.Id == notasId);
            if (nota != null && nota.FacturasId != null)
            {
                var serviciosId = Manager().GetBusinessLogic<FacturasDetalles>().Tabla(true).Where(x => x.FacturasId == nota.FacturasId).Select(x => x.ServiciosId).ToList();
                return DataSourceLoader.Load(Manager().GetBusinessLogic<Servicios>().Tabla(true).Where(x => serviciosId.Contains(x.Id)), loadOptions);
            }

            return DataSourceLoader.Load(Manager().GetBusinessLogic<Servicios>().Tabla(true), loadOptions);
        }
        #endregion

        [HttpGet]
        public IActionResult TraerDatosFactura(long notaId, long servicioId)
        {
            FacturasDetalles facturaDetalle = null;
            var nota = Manager().GetBusinessLogic<Notas>().Tabla(true).FirstOrDefault(x => x.Id == notaId);
            if (nota != null && nota.FacturasId != null)
            {
                facturaDetalle = Manager().GetBusinessLogic<FacturasDetalles>().Tabla(true).FirstOrDefault(x => x.FacturasId == nota.FacturasId && x.ServiciosId == servicioId);
            }

            return new OkObjectResult(facturaDetalle);
        }

        [HttpGet]
        public IActionResult TraerValoresNota(long notaId)
        {
            var nota = Manager().GetBusinessLogic<Notas>().Tabla(true).FirstOrDefault(x => x.Id == notaId);
            return new OkObjectResult(nota);
        }

    }
}
