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
using Dominus.Backend.Application;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using WebApp.Reportes.AtencionesResultado;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class EntregaResultadosController : BaseAppController
    {

        //private const string Prefix = "EntregaResultados"; 

        public EntregaResultadosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<EntregaResultados>().Tabla(true), loadOptions);
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

        private EntregaResultadosModel NewModel() 
        { 
            EntregaResultadosModel model = new EntregaResultadosModel();
            model.Entity.IsNew = true;
            model.Entity.Fecha = DateTime.Now;
            model.Hora = model.Entity.Fecha;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private EntregaResultadosModel EditModel(long Id) 
        { 
            EntregaResultadosModel model = new EntregaResultadosModel();
            model.Entity = Manager().GetBusinessLogic<EntregaResultados>().FindById(x => x.Id == Id, true);
            model.Entity.IsNew = false;
            model.Hora = model.Entity.Fecha;

            if (model.Entity.ContanciaArchivos == null)
                model.Entity.ContanciaArchivos = new Archivos();
            else
                model.Entity.ContanciaArchivos.StringToBase64 = DApp.Util.ArrayBytesToString(model.Entity.ContanciaArchivos.Archivo);

            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(EntregaResultadosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private EntregaResultadosModel EditModel(EntregaResultadosModel model) 
        { 
            ViewBag.Accion = "Save"; 

            if (model.Entity.IsNew)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(model.SerializedResultados))
                    {
                        model.Entity.ListAtencionesResultadoId = JsonConvert.DeserializeObject<List<long>>(model.SerializedResultados);
                    }

                    if ((model.Entity.ListAtencionesResultadoId == null || model.Entity.ListAtencionesResultadoId.Count <= 0))
                    {
                        ModelState.AddModelError("Entity.Id", DApp.DefaultLanguage.GetResource("EntregaResultados.SeleccionNula"));
                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Entity.Id", "Error des-serializando tablas de resultados. | " + e.Message);
                }
            }

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
                        model.Entity = Manager().GetBusinessLogic<EntregaResultados>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<EntregaResultados>().Modify(model.Entity); 
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
        public IActionResult Delete(EntregaResultadosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private EntregaResultadosModel DeleteModel(EntregaResultadosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EntregaResultadosModel newModel = NewModel(); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<EntregaResultados>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<EntregaResultados>().Remove(model.Entity); 
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

        private EntregaResultadosModel NewModelDetail(long IdFather) 
        { 
            EntregaResultadosModel model = new EntregaResultadosModel(); 
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
        public IActionResult EditDetail(EntregaResultadosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(EntregaResultadosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private EntregaResultadosModel DeleteModelDetail(EntregaResultadosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            EntregaResultadosModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<EntregaResultados>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<EntregaResultados>().Remove(model.Entity); 
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
             EntregaResultados entity = new EntregaResultados(); 
             JsonConvert.PopulateObject(values, entity); 
             EntregaResultadosModel model = new EntregaResultadosModel(); 
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
             EntregaResultados entity = Manager().GetBusinessLogic<EntregaResultados>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             EntregaResultadosModel model = new EntregaResultadosModel(); 
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
             EntregaResultados entity = Manager().GetBusinessLogic<EntregaResultados>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<EntregaResultados>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetContanciaArchivosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Archivos>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetMediosEntragasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<MediosEntregas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetPacientesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Pacientes>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetParentescosId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Parentescos>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetTiposIdentificacionid(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposIdentificacion>().Tabla(true), loadOptions);
        }
        #endregion


        [HttpPost]
        public LoadResult GetResultados(DataSourceLoadOptions loadOptions)
        {
            var result = Manager().GetBusinessLogic<AtencionesResultado>().Tabla(true).Where(x => x.EstadosId == 66 || x.EstadosId == 67)
                .Include(x => x.AdmisionesServiciosPrestados.Admisiones)
                .Include(x => x.AdmisionesServiciosPrestados.Atenciones)
                .Include(x => x.AdmisionesServiciosPrestados.Servicios)
                .Include(x => x.AdmisionesServiciosPrestados.Admisiones.Pacientes)
                .Where(x=>x.EstadosId == 67)
                .Select(x => new { x.Id, x.AdmisionesServiciosPrestados, x.Estados, x.Entregado });
            return DataSourceLoader.Load(result, loadOptions);
        }

        [HttpGet]
        public IActionResult ImprimirAtencionesResultadoPorId(long id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = Manager().GetBusinessLogic<EntregaResultadosDetalles>().Tabla(false).Where(x => x.EntregaResultadosId == id).Select(x => x.AtencionesResultadoId).ToArray();
                XtraReport report = new AtencionesResultadoReporte(informacionReporte);
                return PartialView("_ViewerReport", report);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

    }
}
