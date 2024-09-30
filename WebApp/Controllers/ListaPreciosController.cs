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
    public partial class ListaPreciosController : BaseAppController
    {

        //private const string Prefix = "ListaPrecios"; 

        public ListaPreciosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            var result = Manager().GetBusinessLogic<ListaPrecios>().Tabla(true).ToList();
            result.ForEach(x=> {
                var precios = Manager().GetBusinessLogic<PreciosServicios>().Tabla(false).Where(j => j.ListaPreciosId == x.Id);
                if (precios != null)
                {
                    if (x.TipoEstadosId == 54)
                    {
                        x.Total = precios.Sum(x => x.Precio) * ((x.Porcentaje / 100) + 1);
                    }
                    else
                    {
                        x.Total = precios.Select(k => k.Precio + x.Valor).Sum();
                    }
                }
            });
            return DataSourceLoader.Load(result, loadOptions);
        }

        [HttpGet]
        public decimal CalcularValorFinalPor(long Id)
        {
            decimal ValorFinal = 0;
            var preciosServicios = Manager().GetBusinessLogic<PreciosServicios>().FindAll(x => x.ListaPreciosId == Id);

            foreach (var precioServicio in preciosServicios)
                ValorFinal = ValorFinal + precioServicio.Precio;

            return ValorFinal;
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

        private ListaPreciosModel NewModel() 
        { 
            ListaPreciosModel model = new ListaPreciosModel();
            model.Entity.IsNew = true;
            model.Entity.EmpresasId = this.ActualEmpresaId();
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private ListaPreciosModel EditModel(long Id) 
        { 
            ListaPreciosModel model = new ListaPreciosModel();
            model.Entity = Manager().GetBusinessLogic<ListaPrecios>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(ListaPreciosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private ListaPreciosModel EditModel(ListaPreciosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<ListaPrecios>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<ListaPrecios>().Modify(model.Entity); 
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
        public IActionResult Delete(ListaPreciosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private ListaPreciosModel DeleteModel(ListaPreciosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ListaPreciosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ListaPrecios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ListaPrecios>().Remove(model.Entity); 
                    return newModel;
                } 
                catch (Exception e) 
                { 
                    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage());
                } 
            } 
            return model; 
        }
        [HttpPost]
        public IActionResult Duplicate(ListaPreciosModel model)
        {

            return PartialView("Edit", DuplicateModel(model));
        }

        public ListaPreciosModel DuplicateModel(ListaPreciosModel model)
        {
            ViewBag.Accion = "Duplicate";

            model.Entity.Nombre = model.NombreDuplicar;
            model.Entity.Porcentaje = model.PorcentajeDuplicar;
            model.Entity.Valor = model.ValorDuplicar;
            model.Entity.RelacionaListaPreciosId = model.Entity.Id;
            model.Entity.Id = 0;
            model.Entity.IsNew = true;
            model.Entity.CreatedBy = User.Identity.Name;
            model.Entity.UpdatedBy = User.Identity.Name;
            model.Entity.CreationDate = DateTime.Now;
            model.Entity.LastUpdate = DateTime.Now;
            model.Entity.TipoEstadosId = model.TipoEstadoDuplicar;

            model.Entity = Manager().ListaPreciosLogic().Duplicate(model.Entity, model.PorcentajeIncrementoServicio);
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

        private ListaPreciosModel NewModelDetail(long IdFather) 
        { 
            ListaPreciosModel model = new ListaPreciosModel(); 
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
        public IActionResult EditDetail(ListaPreciosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(ListaPreciosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private ListaPreciosModel DeleteModelDetail(ListaPreciosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ListaPreciosModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ListaPrecios>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ListaPrecios>().Remove(model.Entity); 
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
             ListaPrecios entity = new ListaPrecios(); 
             JsonConvert.PopulateObject(values, entity); 
             ListaPreciosModel model = new ListaPreciosModel(); 
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
             ListaPrecios entity = Manager().GetBusinessLogic<ListaPrecios>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             ListaPreciosModel model = new ListaPreciosModel(); 
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
             ListaPrecios entity = Manager().GetBusinessLogic<ListaPrecios>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<ListaPrecios>().Remove(entity); 
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
        public LoadResult GetTipoEstadosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetRelacionaListaPreciosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ListaPrecios>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
