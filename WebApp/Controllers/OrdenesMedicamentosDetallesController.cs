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
    public partial class OrdenesMedicamentosDetallesController : BaseAppController
    {

        //private const string Prefix = "OrdenesMedicamentosDetalles"; 

        public OrdenesMedicamentosDetallesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<OrdenesMedicamentosDetalles>().Tabla(true), loadOptions);
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

        private OrdenesMedicamentosDetallesModel NewModel() 
        { 
            OrdenesMedicamentosDetallesModel model = new OrdenesMedicamentosDetallesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private OrdenesMedicamentosDetallesModel EditModel(long Id) 
        { 
            OrdenesMedicamentosDetallesModel model = new OrdenesMedicamentosDetallesModel();
            model.Entity = Manager().GetBusinessLogic<OrdenesMedicamentosDetalles>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(OrdenesMedicamentosDetallesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private OrdenesMedicamentosDetallesModel EditModel(OrdenesMedicamentosDetallesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<OrdenesMedicamentosDetalles>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<OrdenesMedicamentosDetalles>().Modify(model.Entity); 
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
        public IActionResult Delete(OrdenesMedicamentosDetallesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private OrdenesMedicamentosDetallesModel DeleteModel(OrdenesMedicamentosDetallesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            OrdenesMedicamentosDetallesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<OrdenesMedicamentosDetalles>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<OrdenesMedicamentosDetalles>().Remove(model.Entity); 
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

        private OrdenesMedicamentosDetallesModel NewModelDetail(long IdFather) 
        { 
            OrdenesMedicamentosDetallesModel model = new OrdenesMedicamentosDetallesModel(); 
            model.Entity.OrdenesMedicamentosId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(OrdenesMedicamentosDetallesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(OrdenesMedicamentosDetallesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private OrdenesMedicamentosDetallesModel DeleteModelDetail(OrdenesMedicamentosDetallesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            OrdenesMedicamentosDetallesModel newModel = NewModelDetail(model.Entity.OrdenesMedicamentosId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<OrdenesMedicamentosDetalles>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<OrdenesMedicamentosDetalles>().Remove(model.Entity); 
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
             OrdenesMedicamentosDetalles entity = new OrdenesMedicamentosDetalles(); 
             JsonConvert.PopulateObject(values, entity); 
             OrdenesMedicamentosDetallesModel model = new OrdenesMedicamentosDetallesModel(); 
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
             OrdenesMedicamentosDetalles entity = Manager().GetBusinessLogic<OrdenesMedicamentosDetalles>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             OrdenesMedicamentosDetallesModel model = new OrdenesMedicamentosDetallesModel(); 
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
             OrdenesMedicamentosDetalles entity = Manager().GetBusinessLogic<OrdenesMedicamentosDetalles>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<OrdenesMedicamentosDetalles>().Remove(entity); 
        } 

        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetMedicamentosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Medicamentos>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetOrdenesMedicamentosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<OrdenesMedicamentos>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
