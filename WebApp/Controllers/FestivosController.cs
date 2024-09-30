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
using WebApp;
using Dominus.Backend.Application;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class FestivosController : BaseAppController
    {

        //private const string Prefix = "Festivos"; 

        public FestivosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Festivos>().Tabla(true), loadOptions);
        }

        public IActionResult List()
        {
            FestivosModel model = new FestivosModel();
            return View("List", model);
        }

        public IActionResult ListPartial()
        {
            FestivosModel model = new FestivosModel();
            return PartialView("List",model);
        }

        [HttpGet]
        public IActionResult New()
        {
            return PartialView("Edit", NewModel());
        }

        private FestivosModel NewModel() 
        { 
            FestivosModel model = new FestivosModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private FestivosModel EditModel(long Id) 
        { 
            FestivosModel model = new FestivosModel();
            model.Entity = Manager().GetBusinessLogic<Festivos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(FestivosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private FestivosModel EditModel(FestivosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<Festivos>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<Festivos>().Modify(model.Entity); 
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
        public IActionResult Delete(FestivosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private FestivosModel DeleteModel(FestivosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            FestivosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Festivos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Festivos>().Remove(model.Entity); 
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

        private FestivosModel NewModelDetail(long IdFather) 
        { 
            FestivosModel model = new FestivosModel(); 
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
        public IActionResult EditDetail(FestivosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(FestivosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private FestivosModel DeleteModelDetail(FestivosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            FestivosModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Festivos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Festivos>().Remove(model.Entity); 
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
             Festivos entity = new Festivos(); 
             JsonConvert.PopulateObject(values, entity); 
             FestivosModel model = new FestivosModel(); 
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
             Festivos entity = Manager().GetBusinessLogic<Festivos>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             FestivosModel model = new FestivosModel(); 
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
             Festivos entity = Manager().GetBusinessLogic<Festivos>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Festivos>().Remove(entity); 
        } 

        */
        #endregion

        [HttpPost]
        public IActionResult AddInGrid(string values)
        {
            Festivos entity = new Festivos();
            JsonConvert.PopulateObject(values, entity);
            FestivosModel model = new FestivosModel();
            model.Entity = entity;
            model.Entity.IsNew = true;
            this.EditModel(model);
            if (ModelState.IsValid)
                return Ok(ModelState);
            else
                return BadRequest(ModelState);
        }

        [HttpPost]
        public void DeleteInGrid(int key)
        {
            Festivos entity = Manager().GetBusinessLogic<Festivos>().FindById(x => x.Id == key, false);
            Manager().GetBusinessLogic<Festivos>().Remove(entity);
        }

        public IActionResult GetFestivosPorAnio(int anio)
        {
            var festivos = DApp.Util.Festivos(anio);
            return new OkObjectResult(festivos);
        }

        public IActionResult SetFestivosPorAnio(int anio)
        {
            var festivos = DApp.Util.Festivos(anio);
            foreach (var festivo in festivos)
            {
                var dia = Manager().GetBusinessLogic<Festivos>().FindById(x => x.Dia == festivo.Dia, false);
                if (dia == null)
                {
                    Festivos diaNuevo = new Festivos();
                    diaNuevo.Dia = festivo.Dia;
                    diaNuevo.Descripcion = festivo.Descripcion;
                    diaNuevo.CreatedBy = User.Identity.Name;
                    diaNuevo.UpdatedBy = User.Identity.Name;
                    diaNuevo.CreationDate = DateTime.Now;
                    diaNuevo.LastUpdate = DateTime.Now;
                    Manager().GetBusinessLogic<Festivos>().Add(diaNuevo);
                }
            }
            return Ok();
        }
    }
}
