using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExtreme.AspNet.Data;
using WidgetGallery;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class GlosasController : BaseAppController
    {

        //private const string Prefix = "Glosas"; 

        public GlosasController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Glosas>().Tabla(true), loadOptions);
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

        private GlosasModel NewModel() 
        { 
            GlosasModel model = new GlosasModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private GlosasModel EditModel(long Id) 
        { 
            GlosasModel model = new GlosasModel();
            model.Entity = Manager().GetBusinessLogic<Glosas>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(GlosasModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private GlosasModel EditModel(GlosasModel model) 
        { 
            ViewBag.Accion = "Save"; 
            var OnState = model.Entity.IsNew; 
            if (ModelState.IsValid) 
            { 
                try 
                {
                    var factura = Manager().GetBusinessLogic<Facturas>().FindById(x => x.Id == model.Entity.FacturasId, true);
                    if (factura.Saldo <  model.Entity.ValorAceptado )
                        throw new Exception("El valor aceptado glosado es mayor que el saldo actual de la factura, saldo factura " + factura.Documentos.Prefijo + " " + factura.NroConsecutivo.ToString() + " Saldo Actual: " + factura.Saldo.ToString());
                    if (model.Entity.ValorGlosado<model.Entity.ValorAceptado)
                        throw new Exception("El valor aceptado es mayor que el valor glosado");
                    model.Entity.LastUpdate = DateTime.Now; 
                    model.Entity.UpdatedBy = User.Identity.Name; 
                    if (model.Entity.IsNew) 
                    { 
                        model.Entity.CreationDate = DateTime.Now; 
                        model.Entity.CreatedBy = User.Identity.Name;
                        model.Entity.ValorGlosadoFinal = model.Entity.ValorAceptado;
                        model.Entity = Manager().GetBusinessLogic<Glosas>().Add(model.Entity);
                        factura.Saldo = factura.Saldo - model.Entity.ValorGlosadoFinal;
                        factura.Estadosid = 17;
                        Manager().GetBusinessLogic<Facturas>().Modify(factura);
                        model.Entity.IsNew = false;
                    } 
                    else 
                    {
                        model.Entity.ValorGlosadoFinal = model.Entity.ValorAceptado;
                        Glosas oldData = Manager().GetBusinessLogic<Glosas>().FindById(x => x.Id == model.Entity.Id, false);
                        
                        model.Entity = Manager().GetBusinessLogic<Glosas>().Modify(model.Entity);
                        if (oldData.ValorAceptado != model.Entity.ValorAceptado)
                        {
                            factura.Saldo = factura.Saldo + oldData.ValorGlosadoFinal;
                            factura.Saldo = factura.Saldo - model.Entity.ValorGlosadoFinal;
                            factura.Estadosid = 10082;
                            Manager().GetBusinessLogic<Facturas>().Modify(factura);
                        }

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
        public IActionResult Delete(GlosasModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private GlosasModel DeleteModel(GlosasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            GlosasModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Glosas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Glosas>().Remove(model.Entity); 
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

        private GlosasModel NewModelDetail(long IdFather) 
        { 
            GlosasModel model = new GlosasModel(); 
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
        public IActionResult EditDetail(GlosasModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(GlosasModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private GlosasModel DeleteModelDetail(GlosasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            GlosasModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Glosas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Glosas>().Remove(model.Entity); 
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
             Glosas entity = new Glosas(); 
             JsonConvert.PopulateObject(values, entity); 
             GlosasModel model = new GlosasModel(); 
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
             Glosas entity = Manager().GetBusinessLogic<Glosas>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             GlosasModel model = new GlosasModel(); 
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
             Glosas entity = Manager().GetBusinessLogic<Glosas>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Glosas>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetCausalesGlosasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<CausalesGlosas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetFacturasId(DataSourceLoadOptions loadOptions)
        {
            var result = Manager().GetBusinessLogic<Facturas>().Tabla(true).ToList();

            return DataSourceLoader.Load(result, loadOptions);
        } 
       #endregion

    }
}
