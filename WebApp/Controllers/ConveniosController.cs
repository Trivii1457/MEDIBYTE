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
    public partial class ConveniosController : BaseAppController
    {

        //private const string Prefix = "Convenios"; 

        public ConveniosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Convenios>().Tabla(true), loadOptions);
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

        private ConveniosModel NewModel() 
        { 
            ConveniosModel model = new ConveniosModel();
            model.Entity.IsNew = true;
            model.Entity.EmpresasId = this.ActualEmpresaId();
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private ConveniosModel EditModel(long Id) 
        { 
            ConveniosModel model = new ConveniosModel();
            model.Entity = Manager().GetBusinessLogic<Convenios>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(ConveniosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private ConveniosModel EditModel(ConveniosModel model) 
        { 
            ViewBag.Accion = "Save"; 
            var OnState = model.Entity.IsNew;

            var fechaHoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0,0,0);

            if (model.Entity.FechaCaducidad < model.Entity.FechaInicio)
                ModelState.AddModelError("Entity.Id", $"La Fecha de Caducidad no puede ser menor a la Fecha de inicio");
            if (model.Entity.FechaCaducidad < fechaHoy)
                ModelState.AddModelError("Entity.FechaCaducidad", $"La Fecha de Caducidad no puede ser menor a la fecha actual.");

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
                        model.Entity = Manager().GetBusinessLogic<Convenios>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<Convenios>().Modify(model.Entity); 
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
        public IActionResult Delete(ConveniosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private ConveniosModel DeleteModel(ConveniosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ConveniosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Convenios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Convenios>().Remove(model.Entity); 
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

        private ConveniosModel NewModelDetail(long IdFather) 
        { 
            ConveniosModel model = new ConveniosModel(); 
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
        public IActionResult EditDetail(ConveniosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(ConveniosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private ConveniosModel DeleteModelDetail(ConveniosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ConveniosModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Convenios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Convenios>().Remove(model.Entity); 
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
             Convenios entity = new Convenios(); 
             JsonConvert.PopulateObject(values, entity); 
             ConveniosModel model = new ConveniosModel(); 
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
             Convenios entity = Manager().GetBusinessLogic<Convenios>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             ConveniosModel model = new ConveniosModel(); 
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
             Convenios entity = Manager().GetBusinessLogic<Convenios>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Convenios>().Remove(entity); 
        } 

        */
        #endregion

        #region Datasource Combobox Foraneos 


        [HttpPost]
        public LoadResult GetFormasPagosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<FormasPagos>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetContraparteEntidadesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Entidades>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEntidadesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Entidades>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEstadosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true).Where(x=>x.Tipo== "CONVENIO"), loadOptions);
        }
        [HttpPost]
        public LoadResult GetTipoFacturaEstadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true).Where(x => x.Tipo == "CONVENIOTIPOFACTURA"), loadOptions);
        }
        [HttpPost]
        public LoadResult GetListaPreciosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ListaPrecios>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEmpresasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empresas>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetModalidadesContratacionId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ModalidadesContratacion>().Tabla(true), loadOptions);
        }
        #endregion

    }
}
