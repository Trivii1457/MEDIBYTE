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

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class FilialesController : BaseAppController
    {

        //private const string Prefix = "Filiales"; 

        public FilialesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Filiales>().Tabla(true), loadOptions);
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

        private FilialesModel NewModel() 
        { 
            FilialesModel model = new FilialesModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private FilialesModel EditModel(long Id) 
        { 
            FilialesModel model = new FilialesModel();
            model.Entity = Manager().GetBusinessLogic<Filiales>().FindById(x => x.Id == Id, false);
            if (model.Entity.CiudadesId > 0)
            {
                Ciudades ciudad = Manager().GetBusinessLogic<Ciudades>().FindById(x => x.Id == model.Entity.CiudadesId, true);
                model.DepartamentoId = ciudad.DepartamentosId;
                model.PaisId = ciudad.Departamentos.PaisesId;
                model.Entity.IsNew = false;
            }
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(FilialesModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private FilialesModel EditModel(FilialesModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<Filiales>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<Filiales>().Modify(model.Entity); 
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
        public IActionResult Delete(FilialesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private FilialesModel DeleteModel(FilialesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            FilialesModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Filiales>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Filiales>().Remove(model.Entity); 
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

        private FilialesModel NewModelDetail(long IdFather) 
        { 
            FilialesModel model = new FilialesModel(); 
            model.Entity.EntidadesId = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(FilialesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(FilialesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private FilialesModel DeleteModelDetail(FilialesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            FilialesModel newModel = NewModelDetail(model.Entity.EntidadesId); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Filiales>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Filiales>().Remove(model.Entity); 
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

        //[HttpPost] 
        //public IActionResult AddInGrid(string values) 
        //{ 
        //     Filiales entity = new Filiales(); 
        //     JsonConvert.PopulateObject(values, entity); 
        //     FilialesModel model = new FilialesModel(); 
        //     model.Entity = entity; 
        //     model.Entity.IsNew = true; 
        //     this.EditModel(model); 
        //     if(ModelState.IsValid) 
        //         return Ok(ModelState); 
        //     else 
        //         return BadRequest(ModelState.GetFullErrorMessage()); 
        //} 

        //[HttpPost] 
        //public IActionResult ModifyInGrid(int key, string values) 
        //{ 
        //     Filiales entity = Manager().GetBusinessLogic<Filiales>().FindById(x => x.Id == key, false); 
        //     JsonConvert.PopulateObject(values, entity); 
        //     FilialesModel model = new FilialesModel(); 
        //     model.Entity = entity; 
        //     model.Entity.IsNew = false; 
        //     this.EditModel(model); 
        //     if(ModelState.IsValid) 
        //         return Ok(ModelState); 
        //     else 
        //         return BadRequest(ModelState.GetFullErrorMessage()); 
        //} 

        //[HttpPost]
        //public void DeleteInGrid(int key)
        //{ 
        //     Filiales entity = Manager().GetBusinessLogic<Filiales>().FindById(x => x.Id == key, false); 
        //     Manager().GetBusinessLogic<Filiales>().Remove(entity); 
        //} 


        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetCiudadesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Ciudades>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEntidadesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Entidades>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEstadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetDepartamentosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Departamentos>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetPaisesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Paises>().Tabla(true), loadOptions);
        }
        #endregion
    }
}
