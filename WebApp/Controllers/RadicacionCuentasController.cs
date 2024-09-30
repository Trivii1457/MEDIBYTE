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
using Dominus.Backend.Application;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class RadicacionCuentasController : BaseAppController
    {

        //private const string Prefix = "RadicacionCuentas"; 

        public RadicacionCuentasController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<RadicacionCuentas>().Tabla(true), loadOptions);
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

        private RadicacionCuentasModel NewModel() 
        { 
            RadicacionCuentasModel model = new RadicacionCuentasModel();
            model.Entity.Empresas = Manager().GetBusinessLogic<Empresas>().FindAll(null)[0];
            if (model.Entity.Empresas != null)
                model.Entity.EmpresasId = model.Entity.Empresas.Id;
            model.Entity.Entidades = Manager().GetBusinessLogic<Entidades>().FindAll(null)[0];
            if (model.Entity.Entidades != null)
                model.Entity.EntidadesId = model.Entity.Entidades.Id;
            model.Entity.IsNew = true;
            model.Entity.RadicacionArchivos.IsNew = true;
            model.Process = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private RadicacionCuentasModel EditModel(long Id) 
        { 
            RadicacionCuentasModel model = new RadicacionCuentasModel();
            model.Entity = Manager().GetBusinessLogic<RadicacionCuentas>().FindById(x => x.Id == Id, true);
            model.Entity.IsNew = false;
            model.Process = !(Manager().GetBusinessLogic<RadicacionCuentasDetalles>().FindAll(x => x.RadicacionCuentasId == Id).Count > 0);

            if (model.Entity.RadicacionArchivos == null)
                model.Entity.RadicacionArchivos = new Archivos();
            else
                model.Entity.RadicacionArchivos.StringToBase64 = DApp.Util.ArrayBytesToString(model.Entity.RadicacionArchivos.Archivo);

            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(RadicacionCuentasModel model)
        {
            model.DetalleFacturasPorRadicar = GetFacturasPorRadicar(model.Entity);
            model = EditModel(model);
            model.Process = true;
            foreach (var item in model.DetalleFacturasPorRadicar)
            {
                item.RadicacionFacturasId = model.Entity.Id;
            }
            return PartialView("Edit", model);

        }

        private RadicacionCuentasModel EditModel(RadicacionCuentasModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<RadicacionCuentas>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<RadicacionCuentas>().Modify(model.Entity); 
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
        public IActionResult Delete(RadicacionCuentasModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private RadicacionCuentasModel DeleteModel(RadicacionCuentasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            RadicacionCuentasModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<RadicacionCuentas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<RadicacionCuentas>().Remove(model.Entity); 
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

        private RadicacionCuentasModel NewModelDetail(long IdFather) 
        { 
            RadicacionCuentasModel model = new RadicacionCuentasModel(); 
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
        public IActionResult EditDetail(RadicacionCuentasModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(RadicacionCuentasModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private RadicacionCuentasModel DeleteModelDetail(RadicacionCuentasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            RadicacionCuentasModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<RadicacionCuentas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<RadicacionCuentas>().Remove(model.Entity); 
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
             RadicacionCuentas entity = new RadicacionCuentas(); 
             JsonConvert.PopulateObject(values, entity); 
             RadicacionCuentasModel model = new RadicacionCuentasModel(); 
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
             RadicacionCuentas entity = Manager().GetBusinessLogic<RadicacionCuentas>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             RadicacionCuentasModel model = new RadicacionCuentasModel(); 
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
             RadicacionCuentas entity = Manager().GetBusinessLogic<RadicacionCuentas>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<RadicacionCuentas>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetEmpresasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empresas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEntidadesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Entidades>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetSedesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Sedes>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
