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
using Microsoft.EntityFrameworkCore;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class EntregaResultadosDetallesController : BaseAppController
    {

        //private const string Prefix = "EntregaResultadosDetalles"; 

        public EntregaResultadosDetallesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            var result = Manager().GetBusinessLogic<EntregaResultadosDetalles>().Tabla(true)
                .Include(x => x.AtencionesResultado.AdmisionesServiciosPrestados)
                .Include(x => x.AtencionesResultado.AdmisionesServiciosPrestados.Servicios)
                .Include(x => x.AtencionesResultado.AdmisionesServiciosPrestados.Atenciones);
            return DataSourceLoader.Load(result, loadOptions);
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

        private EntregaResultadosDetallesModel NewModel() 
        { 
            EntregaResultadosDetallesModel model = new EntregaResultadosDetallesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private EntregaResultadosDetallesModel EditModel(long Id) 
        { 
            EntregaResultadosDetallesModel model = new EntregaResultadosDetallesModel();
            model.Entity = Manager().GetBusinessLogic<EntregaResultadosDetalles>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(EntregaResultadosDetallesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private EntregaResultadosDetallesModel EditModel(EntregaResultadosDetallesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<EntregaResultadosDetalles>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<EntregaResultadosDetalles>().Modify(model.Entity); 
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
        public IActionResult Delete(EntregaResultadosDetallesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private EntregaResultadosDetallesModel DeleteModel(EntregaResultadosDetallesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EntregaResultadosDetallesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<EntregaResultadosDetalles>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<EntregaResultadosDetalles>().Remove(model.Entity); 
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

        private EntregaResultadosDetallesModel NewModelDetail(long IdFather) 
        { 
            EntregaResultadosDetallesModel model = new EntregaResultadosDetallesModel(); 
            model.Entity.EntregaResultadosId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(EntregaResultadosDetallesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(EntregaResultadosDetallesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private EntregaResultadosDetallesModel DeleteModelDetail(EntregaResultadosDetallesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EntregaResultadosDetallesModel newModel = NewModelDetail(model.Entity.EntregaResultadosId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<EntregaResultadosDetalles>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<EntregaResultadosDetalles>().Remove(model.Entity); 
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
             EntregaResultadosDetalles entity = new EntregaResultadosDetalles(); 
             JsonConvert.PopulateObject(values, entity); 
             EntregaResultadosDetallesModel model = new EntregaResultadosDetallesModel(); 
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
             EntregaResultadosDetalles entity = Manager().GetBusinessLogic<EntregaResultadosDetalles>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             EntregaResultadosDetallesModel model = new EntregaResultadosDetallesModel(); 
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
             EntregaResultadosDetalles entity = Manager().GetBusinessLogic<EntregaResultadosDetalles>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<EntregaResultadosDetalles>().Remove(entity); 
        } 

        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetAtencionesResultadoId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<AtencionesResultado>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEntregaResultadosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<EntregaResultados>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
