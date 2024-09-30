using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Backend.Application;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class ProgramacionAgendaController : BaseAppController
    {

        //private const string Prefix = "ProgramacionAgenda"; 

        public ProgramacionAgendaController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ProgramacionAgenda>().Tabla(true), loadOptions);
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

        private ProgramacionAgendaModel NewModel() 
        { 
            ProgramacionAgendaModel model = new ProgramacionAgendaModel();
            model.Entity.IsNew = true;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private ProgramacionAgendaModel EditModel(long Id) 
        { 
            ProgramacionAgendaModel model = new ProgramacionAgendaModel();
            model.Entity = Manager().GetBusinessLogic<ProgramacionAgenda>().FindById(x => x.Id == Id, false);
            if (model.Entity.Lunes)
                model.DiasSemanaSelecionados.Add(1);
            if (model.Entity.Martes)
                model.DiasSemanaSelecionados.Add(2);
            if (model.Entity.Miercoles)
                model.DiasSemanaSelecionados.Add(3);
            if (model.Entity.Jueves)
                model.DiasSemanaSelecionados.Add(4);
            if (model.Entity.Viernes)
                model.DiasSemanaSelecionados.Add(5);
            if (model.Entity.Sabado)
                model.DiasSemanaSelecionados.Add(6);
            if (model.Entity.Domingo)
                model.DiasSemanaSelecionados.Add(7);
            model.Entity.IsNew = false;

            model.HoraInicio = model.Entity.FechaInicio;
            model.HoraFinal = model.Entity.FechaFinal;

            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(ProgramacionAgendaModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private ProgramacionAgendaModel EditModel(ProgramacionAgendaModel model) 
        { 
            ViewBag.Accion = "Save"; 
            var OnState = model.Entity.IsNew;

            try
            {
                if (!string.IsNullOrWhiteSpace(model.SerializedServiciosConsultorios))
                {
                    model.Entity.ServiciosConsultorios = JsonConvert.DeserializeObject<List<ServiciosConsultorios>>(model.SerializedServiciosConsultorios);
                }
                if (!string.IsNullOrWhiteSpace(model.SerializedServiciosEmpleados))
                {
                    model.Entity.ServiciosEmpleados = JsonConvert.DeserializeObject<List<ServiciosEmpleados>>(model.SerializedServiciosEmpleados);
                }

                if ((model.Entity.ServiciosConsultorios == null || model.Entity.ServiciosConsultorios.Count <= 0) && (model.Entity.ServiciosEmpleados == null || model.Entity.ServiciosEmpleados.Count <= 0)) 
                {
                    ModelState.AddModelError("Entity.Id", DApp.DefaultLanguage.GetResource("ProgramacionAgenda.SeleccionNula"));
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("Entity.Id", "Error des-serializando tablas de serivicios. | " + e.Message);
            }

            if (model.Entity.FechaFinal < model.Entity.FechaInicio)
            {
                ModelState.AddModelError("Entity.FechaFinal", DApp.GetResource("ProgramacionAgenda.ErrorFechaInicialMayorFechaFinal"));
            }

            //if (model.HoraFinal <= model.HoraInicio)
            //{
            //    ModelState.AddModelError("Entity.HoraFinal", DApp.GetResource("ProgramacionAgenda.ErrorHoraInicialMayorHoraFinal"));
            //}

            if (ModelState.IsValid) 
            {
                //Se construyer la fecha inicio y fecha final con la hora
                model.Entity.FechaInicio = new DateTime(model.Entity.FechaInicio.Year
                    , model.Entity.FechaInicio.Month
                    , model.Entity.FechaInicio.Day
                    , model.HoraInicio.Hour
                    , model.HoraInicio.Minute
                    , 0
                    );
                model.Entity.FechaFinal = new DateTime(model.Entity.FechaFinal.Year
                    , model.Entity.FechaFinal.Month
                    , model.Entity.FechaFinal.Day
                    , model.HoraFinal.Hour
                    , model.HoraFinal.Minute
                    , 0
                    );
                try 
                {
                    model.Entity.LastUpdate = DateTime.Now; 
                    model.Entity.UpdatedBy = User.Identity.Name; 
                    if (model.Entity.IsNew) 
                    { 
                        model.Entity.CreationDate = DateTime.Now; 
                        model.Entity.CreatedBy = User.Identity.Name; 
                        model.Entity = Manager().GetBusinessLogic<ProgramacionAgenda>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<ProgramacionAgenda>().Modify(model.Entity); 
                    } 
                } 
                catch (Exception e) 
                {
                    ViewBag.EsError = true;
                    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage()); 
                } 
            } 
            else 
            { 
                 ModelState.AddModelError("Entity.Id", $"Error: " + ModelState.GetFullErrorMessage()); 
            } 
            return model; 
        } 

        [HttpPost]
        public IActionResult Delete(ProgramacionAgendaModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private ProgramacionAgendaModel DeleteModel(ProgramacionAgendaModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ProgramacionAgendaModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ProgramacionAgenda>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ProgramacionAgenda>().Remove(model.Entity); 
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

        private ProgramacionAgendaModel NewModelDetail(long IdFather) 
        { 
            ProgramacionAgendaModel model = new ProgramacionAgendaModel(); 
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
        public IActionResult EditDetail(ProgramacionAgendaModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(ProgramacionAgendaModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private ProgramacionAgendaModel DeleteModelDetail(ProgramacionAgendaModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ProgramacionAgendaModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ProgramacionAgenda>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ProgramacionAgenda>().Remove(model.Entity); 
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
             ProgramacionAgenda entity = new ProgramacionAgenda(); 
             JsonConvert.PopulateObject(values, entity); 
             ProgramacionAgendaModel model = new ProgramacionAgendaModel(); 
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
             ProgramacionAgenda entity = Manager().GetBusinessLogic<ProgramacionAgenda>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             ProgramacionAgendaModel model = new ProgramacionAgendaModel(); 
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
             ProgramacionAgenda entity = Manager().GetBusinessLogic<ProgramacionAgenda>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<ProgramacionAgenda>().Remove(entity); 
        } 

        */
        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetConsultorios(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Consultorios>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEmpleados(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empleados>().Tabla(true).Where(x=>x.TipoEmpleados == 2), loadOptions);
        }
        [HttpPost]
        public Dictionary<string, Object> GetServiciosPorConsultorios(List<long> consultoriosId, long id)
        {
            return Manager().ProgramacionAgendaBusinessLogic().GetServiciosPorConsultorios(consultoriosId,id);
        }
        [HttpPost]
        public Dictionary<string, Object> GetServiciosPorEmpleados(List<long> empleadosId, long id)
        {
            return Manager().ProgramacionAgendaBusinessLogic().GetServiciosPorEmpleados(empleadosId,id);
        }
        [HttpPost]
        public List<long?> GetConsultoriosSeleccionados(long id)
        {
            return Manager().GetBusinessLogic<ProgramacionDisponible>().Tabla(false).Where(x => x.ProgramacionAgendaId == id && x.ConsultoriosId != null).Select(x => x.ConsultoriosId).ToList();
        }
        [HttpPost]
        public List<long?> GetEmpleadosSeleccionados(long id)
        {
            return Manager().GetBusinessLogic<ProgramacionDisponible>().Tabla(false).Where(x=>x.ProgramacionAgendaId == id && x.EmpleadosId != null).Select(x => x.EmpleadosId).ToList();
        }
        #endregion

    }
}
