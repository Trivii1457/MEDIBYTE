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
using DevExpress.CodeParser.Diagnostics;
using System.Collections.Generic;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class RecaudosController : BaseAppController
    {

        //private const string Prefix = "Recaudos"; 

        public RecaudosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Recaudos>().Tabla(true), loadOptions);
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

        private RecaudosModel NewModel() 
        { 
            RecaudosModel model = new RecaudosModel();
            model.Entity.IsNew = true;
            model.Entity.FechaRecaudo = DateTime.Now;
            model.Entity.CiclosCajas = Manager().GetBusinessLogic<CiclosCajas>().FindById(x => x.OpenUsers.UserName == User.Identity.Name, true);
            if (model.Entity.CiclosCajas != null)
                model.Entity.CiclosCajasId = model.Entity.CiclosCajas.Id;
            model.Entity.Empresas = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == long.Parse(User.FindFirst("EmpresaId").Value), false);
            if (model.Entity.Empresas != null)
                model.Entity.EmpresasId = model.Entity.Empresas.Id;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private RecaudosModel EditModel(long Id) 
        { 
            RecaudosModel model = new RecaudosModel();
            model.Entity = Manager().GetBusinessLogic<Recaudos>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(RecaudosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private RecaudosModel EditModel(RecaudosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<Recaudos>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    {
                        model.Entity.ValorTotalRecibido = Manager().GetBusinessLogic<RecaudosDetalles>().FindAll(x => x.RecaudosId == model.Entity.Id).Select(y => y.ValorAplicado).Sum();
                        model.Entity = Manager().GetBusinessLogic<Recaudos>().Modify(model.Entity); 
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
        public IActionResult Delete(RecaudosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private RecaudosModel DeleteModel(RecaudosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            RecaudosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Recaudos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Recaudos>().Remove(model.Entity); 
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

        private RecaudosModel NewModelDetail(long IdFather) 
        { 
            RecaudosModel model = new RecaudosModel(); 
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
        public IActionResult EditDetail(RecaudosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(RecaudosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private RecaudosModel DeleteModelDetail(RecaudosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            RecaudosModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Recaudos>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Recaudos>().Remove(model.Entity); 
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
             Recaudos entity = new Recaudos(); 
             JsonConvert.PopulateObject(values, entity); 
             RecaudosModel model = new RecaudosModel(); 
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
             Recaudos entity = Manager().GetBusinessLogic<Recaudos>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             RecaudosModel model = new RecaudosModel(); 
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
             Recaudos entity = Manager().GetBusinessLogic<Recaudos>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Recaudos>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetBancosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Bancos>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetCiclosCajasIdAll(DataSourceLoadOptions loadOptions, bool isNuevo)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<CiclosCajas>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetCiclosCajasId(DataSourceLoadOptions loadOptions, bool isNuevo)
        {
            List<CiclosCajas> result = new List<CiclosCajas>();
            if (isNuevo)
            {
                result = Manager().GetBusinessLogic<CiclosCajas>().Tabla(true).Where(x => x.OpenUsersId == this.ActualUsuarioId() && x.CloseUsersId == null).ToList();
            }
            else
            {
                result = Manager().GetBusinessLogic<CiclosCajas>().Tabla(true).Where(x => x.OpenUsersId == this.ActualUsuarioId()).ToList();
            }
            return DataSourceLoader.Load(result, loadOptions);
        } 

        [HttpPost]
        public LoadResult GetMediosPagoId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<MediosPago>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetEmpresasId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empresas>().Tabla(true).Where(x => x.Id == long.Parse(User.FindFirst("EmpresaId").Value)), loadOptions);
        }

        [HttpPost]
        public LoadResult GetEntidadesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Entidades>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetPacientesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Pacientes>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetSedesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Sedes>().Tabla(true).Where(x=>x.EmpresasId== long.Parse(User.FindFirst("EmpresaId").Value)), loadOptions);
        }
        #endregion

    }
}
