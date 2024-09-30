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
using DevExpress.DataProcessing;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class AdmisionesController : BaseAppController
    {

        //private const string Prefix = "Admisiones"; 

        public AdmisionesController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            var result = Manager().GetBusinessLogic<Admisiones>().Tabla(true)
                .Include(x => x.ProgramacionCitas.Servicios)
                .Include(x => x.ProgramacionCitas.Consultorios)
                .Include(x => x.ProgramacionCitas.Empleados)
                .Where(x => x.EstadosId != 72);

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

        private AdmisionesModel NewModel()
        {
            AdmisionesModel model = new AdmisionesModel();
            model.Entity.IsNew = true;
            model.Entity.EmpresasId = this.ActualEmpresaId();
            model.Entity.EstadosId = 60;

            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }


        [HttpPost]
        public IActionResult Edit(AdmisionesModel model)
        {
            return PartialView("Edit", EditModel(model));
        }


        [HttpPost]
        public IActionResult Delete(AdmisionesModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private AdmisionesModel DeleteModel(AdmisionesModel model)
        {
            ViewBag.Accion = "Delete";
            AdmisionesModel newModel = NewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<Admisiones>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<Admisiones>().Remove(model.Entity);
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

        private AdmisionesModel NewModelDetail(long IdFather) 
        { 
            AdmisionesModel model = new AdmisionesModel(); 
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
        public IActionResult EditDetail(AdmisionesModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(AdmisionesModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private AdmisionesModel DeleteModelDetail(AdmisionesModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AdmisionesModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Admisiones>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Admisiones>().Remove(model.Entity); 
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
             Admisiones entity = new Admisiones(); 
             JsonConvert.PopulateObject(values, entity); 
             AdmisionesModel model = new AdmisionesModel(); 
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
             Admisiones entity = Manager().GetBusinessLogic<Admisiones>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             AdmisionesModel model = new AdmisionesModel(); 
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
             Admisiones entity = Manager().GetBusinessLogic<Admisiones>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<Admisiones>().Remove(entity); 
        } 

        */
        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetExoneracionArchivoId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Archivos>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetCausasExternasId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<CausasExternas>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetCitasId(DataSourceLoadOptions loadOptions)
        {
            var result = Manager().GetBusinessLogic<ProgramacionCitas>().Tabla(true).Where(x => x.EstadosId == 3).ToList();
            return DataSourceLoader.Load(result, loadOptions);
        }
        [HttpPost]
        public LoadResult GetConveniosId(DataSourceLoadOptions loadOptions, long EntidadesId, long ServiciosId)
        {
            return DataSourceLoader.Load(Manager().ProgramacionCitasBusinessLogic().GetConveniosByEntidad(EntidadesId, ServiciosId), loadOptions);
        }
        [HttpPost]
        public LoadResult GetDiagnosticosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Diagnosticos>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEmpresasId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empresas>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetValorPagoEstadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetDescuentoEstadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetValorDescuentoEstadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEstadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetFilialesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Filiales>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetPacientesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Pacientes>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetUserAproboId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<User>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetConsultoriosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Consultorios>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEmpleadosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Empleados>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEntidadesId(DataSourceLoadOptions loadOptions, long PacientesId)
        {
            return DataSourceLoader.Load(Manager().ProgramacionCitasBusinessLogic().GetEntidadesByPaciente(PacientesId), loadOptions);
        }
        [HttpPost]
        public LoadResult GetFormasPagosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<FormasPagos>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetMedioPagosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<MediosPago>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetDocumentosId(DataSourceLoadOptions loadOptions, long citaId)
        {
            if (citaId > 0)
            {
                var cita = Manager().GetBusinessLogic<ProgramacionCitas>().FindById(x => x.Id == citaId, false);
                return DataSourceLoader.Load(
                    Manager().GetBusinessLogic<SedesDocumentos>().Tabla(true)
                    .Where(x => x.Documentos.Transaccion == 0 && x.Documentos.Activo && x.SedesId == cita.SedesId)
                    .Select(x => x.Documentos)
                    , loadOptions);
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        public LoadResult GetTiposUsuariosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposUsuarios>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetCoberturaPlanBeneficiosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<CoberturaPlanBeneficios>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEstadosIdTipoDuracion(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true).Where(x => x.Tipo == "SERVICIOSDURACION"), loadOptions);
        }
        [HttpPost]
        public LoadResult GetServiciosId(DataSourceLoadOptions loadOptions)
        {
            List<string> codes = new List<string> { "COP", "CM", "CR", "PC" };
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Servicios>().Tabla(true).Where(x => x.EstadosId == 30 && !codes.Contains(x.Codigo)), loadOptions);
        }
        [HttpPost]
        public LoadResult GetSedesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Sedes>().Tabla(true), loadOptions);
        }
        #endregion

    }
}
