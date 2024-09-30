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
    public partial class CiclosCajasController : BaseAppController
    {

        //private const string Prefix = "CiclosCajas"; 

        public CiclosCajasController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<CiclosCajas>().Tabla(true), loadOptions);
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

        private CiclosCajasModel NewModel()
        {
            CiclosCajasModel model = new CiclosCajasModel();
            model.Entity.IsNew = true;
            var ciclosAbriertos = Manager().GetBusinessLogic<CiclosCajas>().FindAll(x => x.OpenUsers.UserName == User.Identity.Name && x.CloseUsersId == null);
            if (ciclosAbriertos != null && ciclosAbriertos.Count > 0)
            {
                ModelState.AddModelError("Entity.Id", "El usuario " + User.Identity.Name + " ya tiene una caja abierta");
                model.TieneCaja = true;
            }


            model.Entity.FechaApertura = DateTime.Now;
            model.Entity.OpenUsers = Manager().GetBusinessLogic<User>().FindById(x => x.UserName == User.Identity.Name, false);
            if (model.Entity.OpenUsers != null)
                model.Entity.OpenUsersId = model.Entity.OpenUsers.Id;
            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private CiclosCajasModel EditModel(long Id) 
        { 
            CiclosCajasModel model = new CiclosCajasModel();
            model.Entity = Manager().GetBusinessLogic<CiclosCajas>().FindById(x => x.Id == Id, false);
            if (model.Entity.FechaCierre == null)
                model.Entity.FechaCierre = DateTime.Now;
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(CiclosCajasModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private CiclosCajasModel EditModel(CiclosCajasModel model) 
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
                        var cajasAbiertas = Manager().GetBusinessLogic<CiclosCajas>().FindAll(x => x.CloseUsersId == null, false).Select(x => x.CajasId).ToList();
                        if(cajasAbiertas.Contains(model.Entity.CajasId))
                        {
                            throw new Exception($"Esta caja se encuentra actualmete abierta.");
                        }
                        model.Entity = Manager().GetBusinessLogic<CiclosCajas>().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        if (model.Entity.CloseUsersId != null)
                        {
                            model.Entity.TotalRecaudos = Manager().GetBusinessLogic<Recaudos>().FindAll(x => x.CiclosCajasId == model.Entity.Id,true).Where(x=>x.MediosPago.Codigo == "10").Sum(x => x.ValorTotalRecibido);
                            var cuadre = model.Entity.ValorApertura + model.Entity.TotalRecaudos - model.Entity.ValorCierre;
                            if (cuadre < 0)
                            {
                                model.Entity.TotalSobrante = cuadre * -1;
                                model.Entity.TotalFaltante = 0;
                            }
                            else
                            {
                                model.Entity.TotalFaltante = cuadre;
                                model.Entity.TotalSobrante = 0;
                            }
                        }
                        model.Entity = Manager().GetBusinessLogic<CiclosCajas>().Modify(model.Entity);
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
        public IActionResult Delete(CiclosCajasModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private CiclosCajasModel DeleteModel(CiclosCajasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            CiclosCajasModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<CiclosCajas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<CiclosCajas>().Remove(model.Entity); 
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

        private CiclosCajasModel NewModelDetail(long IdFather) 
        { 
            CiclosCajasModel model = new CiclosCajasModel(); 
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
        public IActionResult EditDetail(CiclosCajasModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(CiclosCajasModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private CiclosCajasModel DeleteModelDetail(CiclosCajasModel model)
        { 
            ViewBag.Accion = "Delete"; 
            CiclosCajasModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<CiclosCajas>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<CiclosCajas>().Remove(model.Entity); 
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
             CiclosCajas entity = new CiclosCajas(); 
             JsonConvert.PopulateObject(values, entity); 
             CiclosCajasModel model = new CiclosCajasModel(); 
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
             CiclosCajas entity = Manager().GetBusinessLogic<CiclosCajas>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             CiclosCajasModel model = new CiclosCajasModel(); 
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
             CiclosCajas entity = Manager().GetBusinessLogic<CiclosCajas>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<CiclosCajas>().Remove(entity); 
        } 

        */
        #endregion

        #region Datasource Combobox Foraneos 


        [HttpPost]
        public LoadResult GetCajasId(DataSourceLoadOptions loadOptions)
        {
            var cajasAbiertas = Manager().GetBusinessLogic<CiclosCajas>().FindAll(x => x.CloseUsersId == null).Select(x => x.CajasId).ToList();

            return DataSourceLoader.Load(  Manager().GetBusinessLogic<Cajas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetCloseUsersId(DataSourceLoadOptions loadOptions, long? closeUserId)
        {
            var query = Manager().GetBusinessLogic<User>().Tabla(true);
            if (closeUserId == null)
            {
                query = query.Where(x => x.Id == this.ActualUsuarioId());
            }
            return DataSourceLoader.Load(query, loadOptions);
        } 
        [HttpPost]
        public LoadResult GetEstadosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true).Where(x=>x.Tipo== "CICLO DE CAJA"), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetOpenUsersId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<User>().Tabla(true), loadOptions);
        } 
       #endregion

    }
}
