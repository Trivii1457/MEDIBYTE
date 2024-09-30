using DevExtreme.AspNet.Data;
using WidgetGallery;
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

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class RecaudosDetallesController : BaseAppController
    {

        //private const string Prefix = "RecaudosDetalles"; 

        public RecaudosDetallesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<RecaudosDetalles>().Tabla(true), loadOptions);
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

        private RecaudosDetallesModel NewModel() 
        { 
            RecaudosDetallesModel model = new RecaudosDetallesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private RecaudosDetallesModel EditModel(long Id) 
        { 
            RecaudosDetallesModel model = new RecaudosDetallesModel();
            model.Entity = Manager().GetBusinessLogic<RecaudosDetalles>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(RecaudosDetallesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private RecaudosDetallesModel EditModel(RecaudosDetallesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<RecaudosDetalles>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<RecaudosDetalles>().Modify(model.Entity); 
                    } 
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
        public IActionResult Delete(RecaudosDetallesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private RecaudosDetallesModel DeleteModel(RecaudosDetallesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            RecaudosDetallesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<RecaudosDetalles>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<RecaudosDetalles>().Remove(model.Entity); 
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

        private RecaudosDetallesModel NewModelDetail(long IdFather)
        {
            RecaudosDetallesModel model = new RecaudosDetallesModel();
            model.Entity.RecaudosId = IdFather;
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(RecaudosDetallesModel model)
        {
            return PartialView("EditDetail", EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(RecaudosDetallesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private RecaudosDetallesModel DeleteModelDetail(RecaudosDetallesModel model)
        {
            ViewBag.Accion = "Delete";
            RecaudosDetallesModel newModel = NewModelDetail(model.Entity.RecaudosId);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<RecaudosDetalles>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<RecaudosDetalles>().Remove(model.Entity);
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
            try
            {
                RecaudosDetalles entity = new RecaudosDetalles();
                JsonConvert.PopulateObject(values, entity);
                RecaudosDetallesModel model = new RecaudosDetallesModel();
                model.Entity = entity;
                model.Entity.IsNew = true;
                var saldo = Manager().GetBusinessLogic<Facturas>().FindById(x => x.Id == model.Entity.FacturasId, true);
                if (saldo.Saldo < (model.Entity.ValorAplicado + model.Entity.ValorRetencion + model.Entity.ValorReteIca))
                    throw new Exception("El valor aplicado mas las retenciones es mayor que el saldo actual de la factura, saldo factura " + saldo.Documentos.Prefijo + " " + saldo.NroConsecutivo.ToString() + " Saldo Actual: " + saldo.Saldo.ToString());
                this.EditModel(model);
                saldo.Saldo = saldo.Saldo - (model.Entity.ValorAplicado + model.Entity.ValorRetencion + model.Entity.ValorReteIca);
                saldo.Estadosid = 16;
                Manager().GetBusinessLogic<Facturas>().Modify(saldo);
                var recaudo = Manager().GetBusinessLogic<Recaudos>().FindById(x => x.Id == entity.RecaudosId, false);
                recaudo.ValorTotalRecibido = recaudo.ValorTotalRecibido + (entity.ValorAplicado);
                Manager().GetBusinessLogic<Recaudos>().Modify(recaudo);
                if (ModelState.IsValid)
                    return Ok(ModelState);
                else
                    return BadRequest(ModelState.GetFullErrorMessage());
            }
            catch (Exception e)
            {
                return BadRequest(e.GetFullErrorMessage());
            }
        }

        [HttpPost]
        public IActionResult ModifyInGrid(int key, string values)
        {
            try
            {
                RecaudosDetalles entity = Manager().GetBusinessLogic<RecaudosDetalles>().FindById(x => x.Id == key, false);
                JsonConvert.PopulateObject(values, entity);
                RecaudosDetallesModel model = new RecaudosDetallesModel();
                model.Entity = entity;
                model.Entity.IsNew = false;
                RecaudosDetalles oldData = Manager().GetBusinessLogic<RecaudosDetalles>().FindById(x => x.Id == model.Entity.Id, false);
                if ((oldData.ValorAplicado + oldData.ValorRetencion + oldData.ValorReteIca) != (model.Entity.ValorAplicado + model.Entity.ValorRetencion + model.Entity.ValorReteIca))
                {
                    var saldo = Manager().GetBusinessLogic<Facturas>().FindById(x => x.Id == model.Entity.FacturasId, true);
                    saldo.Saldo = saldo.Saldo + (oldData.ValorAplicado + oldData.ValorRetencion + oldData.ValorReteIca);
                    saldo.Saldo = saldo.Saldo - (model.Entity.ValorAplicado + model.Entity.ValorRetencion + model.Entity.ValorReteIca);
                    if (saldo.Saldo < 0)
                        throw new Exception("El valor aplicado mas la retencion es mayor que el saldo actual de la factura, saldo factura " + saldo.Documentos.Prefijo + " " + saldo.NroConsecutivo.ToString() + " Saldo Actual: " + saldo.Saldo.ToString());

                }
                this.EditModel(model);
                if ((oldData.ValorAplicado + oldData.ValorRetencion + oldData.ValorReteIca) != (model.Entity.ValorAplicado + model.Entity.ValorRetencion + model.Entity.ValorReteIca))
                {
                    var saldo = Manager().GetBusinessLogic<Facturas>().FindById(x => x.Id == model.Entity.FacturasId, false);
                    saldo.Saldo = saldo.Saldo + (oldData.ValorAplicado + oldData.ValorRetencion + oldData.ValorReteIca);
                    saldo.Saldo = saldo.Saldo - (model.Entity.ValorAplicado + model.Entity.ValorRetencion + model.Entity.ValorReteIca);
                    saldo.Estadosid = 16;
                    Manager().GetBusinessLogic<Facturas>().Modify(saldo);
                    var recaudo = Manager().GetBusinessLogic<Recaudos>().FindById(x => x.Id == entity.RecaudosId, false);
                    recaudo.ValorTotalRecibido = recaudo.ValorTotalRecibido - (oldData.ValorAplicado + oldData.ValorRetencion + oldData.ValorReteIca);
                    recaudo.ValorTotalRecibido = recaudo.ValorTotalRecibido - (model.Entity.ValorAplicado + model.Entity.ValorRetencion + model.Entity.ValorReteIca);
                    Manager().GetBusinessLogic<Recaudos>().Modify(recaudo);
                    //var recaudo = Manager().GetBusinessLogic<Recaudos>().FindById(x => x.Id == entity.RecaudosId, false);
                    //recaudo.ValorTotalRecibido = recaudo.ValorTotalRecibido + (entity.ValorAplicado + entity.ValorRetencion + entity.ValorReteIca);
                }
                if (ModelState.IsValid)
                    return Ok(ModelState);
                else
                    return BadRequest(ModelState.GetFullErrorMessage());
            }
            catch (Exception e)
            {
                return BadRequest(e.GetFullErrorMessage());
            }

            
        }

        [HttpPost]
        public void DeleteInGrid(int key)
        {
            RecaudosDetalles entity = Manager().GetBusinessLogic<RecaudosDetalles>().FindById(x => x.Id == key, false);
            var saldo = Manager().GetBusinessLogic<Facturas>().FindById(x => x.Id == entity.FacturasId, false);
            saldo.Saldo = saldo.Saldo + (entity.ValorAplicado + entity.ValorRetencion + entity.ValorReteIca);
            Manager().GetBusinessLogic<Facturas>().Modify(saldo);
            var recaudo = Manager().GetBusinessLogic<Recaudos>().FindById(x => x.Id == entity.RecaudosId, false);
            recaudo.ValorTotalRecibido = recaudo.ValorTotalRecibido - (entity.ValorAplicado + entity.ValorRetencion + entity.ValorReteIca);
            Manager().GetBusinessLogic<Recaudos>().Modify(recaudo);
            Manager().GetBusinessLogic<RecaudosDetalles>().Remove(entity);
        }


        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetFacturasId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Facturas>().Tabla(true).OrderByDescending(x => x.Saldo), loadOptions);
        } 

        [HttpPost]
        public LoadResult GetRecaudosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Recaudos>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
