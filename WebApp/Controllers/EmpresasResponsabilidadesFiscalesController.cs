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
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class EmpresasResponsabilidadesFiscalesController : BaseAppController
    {

        //private const string Prefix = "EmpresasResponsabilidadesFiscales"; 

        public EmpresasResponsabilidadesFiscalesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<EmpresasResponsabilidadesFiscales>().Tabla(true), loadOptions);
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

        private EmpresasResponsabilidadesFiscalesModel NewModel() 
        { 
            EmpresasResponsabilidadesFiscalesModel model = new EmpresasResponsabilidadesFiscalesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private EmpresasResponsabilidadesFiscalesModel EditModel(long Id) 
        { 
            EmpresasResponsabilidadesFiscalesModel model = new EmpresasResponsabilidadesFiscalesModel();
            model.Entity = Manager().GetBusinessLogic<EmpresasResponsabilidadesFiscales>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(EmpresasResponsabilidadesFiscalesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private EmpresasResponsabilidadesFiscalesModel EditModel(EmpresasResponsabilidadesFiscalesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<EmpresasResponsabilidadesFiscales>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<EmpresasResponsabilidadesFiscales>().Modify(model.Entity); 
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
        public IActionResult Delete(EmpresasResponsabilidadesFiscalesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private EmpresasResponsabilidadesFiscalesModel DeleteModel(EmpresasResponsabilidadesFiscalesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EmpresasResponsabilidadesFiscalesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<EmpresasResponsabilidadesFiscales>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<EmpresasResponsabilidadesFiscales>().Remove(model.Entity); 
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

        private EmpresasResponsabilidadesFiscalesModel NewModelDetail(long IdFather) 
        { 
            EmpresasResponsabilidadesFiscalesModel model = new EmpresasResponsabilidadesFiscalesModel(); 
            model.Entity.EmpresasId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(EmpresasResponsabilidadesFiscalesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(EmpresasResponsabilidadesFiscalesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private EmpresasResponsabilidadesFiscalesModel DeleteModelDetail(EmpresasResponsabilidadesFiscalesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EmpresasResponsabilidadesFiscalesModel newModel = NewModelDetail(model.Entity.EmpresasId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<EmpresasResponsabilidadesFiscales>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<EmpresasResponsabilidadesFiscales>().Remove(model.Entity); 
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
             EmpresasResponsabilidadesFiscales entity = new EmpresasResponsabilidadesFiscales(); 
             JsonConvert.PopulateObject(values, entity); 
             EmpresasResponsabilidadesFiscalesModel model = new EmpresasResponsabilidadesFiscalesModel(); 
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
             EmpresasResponsabilidadesFiscales entity = Manager().GetBusinessLogic<EmpresasResponsabilidadesFiscales>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             EmpresasResponsabilidadesFiscalesModel model = new EmpresasResponsabilidadesFiscalesModel(); 
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
             EmpresasResponsabilidadesFiscales entity = Manager().GetBusinessLogic<EmpresasResponsabilidadesFiscales>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<EmpresasResponsabilidadesFiscales>().Remove(entity); 
        } 

        
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetEmpresasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empresas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetResponsabilidadesFiscalesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ResponsabilidadesFiscales>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
