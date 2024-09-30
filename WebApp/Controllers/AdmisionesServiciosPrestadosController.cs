using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExtreme.AspNet.Data;
using WidgetGallery;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using DevExpress.Data.ODataLinq.Helpers;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class AdmisionesServiciosPrestadosController : BaseAppController
    {

        //private const string Prefix = "AdmisionesServiciosPrestados"; 

        public AdmisionesServiciosPrestadosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetAdmisionesServiciosPrestadosParaArchivos(DataSourceLoadOptions loadOptions)
        {
            var result = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().Tabla(true)
                .Include(x => x.Admisiones.Pacientes)
                .Where(x=>!x.LecturaRealizada && x.Servicios.RequiereLecturaResultados);

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
        public IActionResult New(bool EsCorrecion)
        {
            return PartialView("Edit", NewModel(EsCorrecion));
        }

        private AdmisionesServiciosPrestadosModel NewModel(bool EsCorrecion) 
        { 
            AdmisionesServiciosPrestadosModel model = new AdmisionesServiciosPrestadosModel();
            model.Entity.IsNew = true;
            model.Entity.Facturado = false;
            model.Entity.PorcImpuesto = 0;
            model.Entity.FacturasId = null;
            model.Entity.FacturasGeneracionId = null;
            model.Entity.EsCorrecion = EsCorrecion;
            return model; 
        } 

        [HttpGet]
        public IActionResult Edit(long Id, bool EsCorrecion)
        {
            return PartialView("Edit", EditModel(Id, EsCorrecion));
        }

        private AdmisionesServiciosPrestadosModel EditModel(long Id, bool EsCorrecion) 
        { 
            AdmisionesServiciosPrestadosModel model = new AdmisionesServiciosPrestadosModel();
            model.Entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            model.Entity.EsCorrecion = EsCorrecion;
            return model; 
        } 

        [HttpPost]
        public IActionResult Edit(AdmisionesServiciosPrestadosModel model)
        {
            return PartialView("Edit",EditModel(model));
        }

        private AdmisionesServiciosPrestadosModel EditModel(AdmisionesServiciosPrestadosModel model) 
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
                        model.Entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().Add(model.Entity); 
                        model.Entity.IsNew = false;
                    } 
                    else 
                    { 
                        model.Entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().Modify(model.Entity); 
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
        public IActionResult Delete(AdmisionesServiciosPrestadosModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private AdmisionesServiciosPrestadosModel DeleteModel(AdmisionesServiciosPrestadosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AdmisionesServiciosPrestadosModel newModel = NewModel(model.Entity.EsCorrecion); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().Remove(model.Entity); 
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
        public IActionResult NewDetail(long IdFather, bool EsCorrecion)
        {
            return PartialView("EditDetail", NewModelDetail(IdFather, EsCorrecion));
        }

        private AdmisionesServiciosPrestadosModel NewModelDetail(long IdFather, bool EsCorrecion) 
        { 
            AdmisionesServiciosPrestadosModel model = new AdmisionesServiciosPrestadosModel();
            model.Entity.AdmisionesId = IdFather;
            var atencion = Manager().GetBusinessLogic<Atenciones>().FindById(x => x.AdmisionesId == model.Entity.AdmisionesId, false);
            if (atencion != null)
                model.Entity.AtencionesId = atencion.Id;
            model.Entity.Cantidad = 1;
            model.Entity.IsNew = true; 
            model.Entity.EsCorrecion = EsCorrecion;
            if (EsCorrecion)
            {
                if (atencion == null)
                    throw new Exception("La admision no tiene una atencion relacionada.");
            }
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id, bool EsCorrecion)
        {
            return PartialView("EditDetail", EditModel(Id, EsCorrecion));
        }

        [HttpPost]
        public IActionResult EditDetail(AdmisionesServiciosPrestadosModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(AdmisionesServiciosPrestadosModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private AdmisionesServiciosPrestadosModel DeleteModelDetail(AdmisionesServiciosPrestadosModel model)
        { 
            ViewBag.Accion = "Delete"; 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().Remove(model.Entity); 
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
        /*
        [HttpPost] 
        public IActionResult AddInGrid(string values) 
        { 
             AdmisionesServiciosPrestados entity = new AdmisionesServiciosPrestados(); 
             JsonConvert.PopulateObject(values, entity); 
             AdmisionesServiciosPrestadosModel model = new AdmisionesServiciosPrestadosModel(); 
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
             AdmisionesServiciosPrestados entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             AdmisionesServiciosPrestadosModel model = new AdmisionesServiciosPrestadosModel(); 
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
             AdmisionesServiciosPrestados entity = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().Remove(entity); 
        } 

        */
        #endregion 

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetAdmisionesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Admisiones>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetAtencionesId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Atenciones>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetFacturasId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Facturas>().Tabla(true), loadOptions);
        } 
        [HttpPost]
        public LoadResult GetFacturasGeneracionId(DataSourceLoadOptions loadOptions)
        { 
            return DataSourceLoader.Load(Manager().GetBusinessLogic<FacturasGeneracion>().Tabla(true), loadOptions);
        }

        [HttpPost]
        public LoadResult GetServiciosId(DataSourceLoadOptions loadOptions)
        {
            List<string> codes = new List<string> { "COP", "CM", "CR", "PC" };
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Servicios>().Tabla(true).Where(x => x.EstadosId == 30 && !codes.Contains(x.Codigo)), loadOptions);
        }
        [HttpPost]
        public LoadResult GetServiciosFacturados(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ServiciosFacturar>().Tabla(true), loadOptions);
        }
        #endregion

        [HttpGet]
        public IActionResult RefrescarValorPagarParticular(long admisionId)
        {
            try
            {
                if (admisionId > 0)
                {
                    var admision = Manager().GetBusinessLogic<Admisiones>().FindById(x => x.Id == admisionId,false);
                    return new OkObjectResult(admision);
                }

                return new BadRequestObjectResult(0);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }

        [HttpGet]
        public IActionResult TraerValorServicio(long idServicio, long convenioId)
        {
            try
            {
                if (idServicio > 0 && convenioId > 0)
                {
                    decimal valorServicio = 0;

                    var convenio = Manager().GetBusinessLogic<Convenios>().FindById(x => x.Id == convenioId, false);
                    var precioServicio = Manager().GetBusinessLogic<PreciosServicios>().FindById(x => x.ListaPreciosId == convenio.ListaPreciosId && x.ServiciosId == idServicio,true);
                    if (precioServicio != null)
                    {
                        if (precioServicio.ListaPrecios.TipoEstadosId != 54)
                        {
                            valorServicio = precioServicio.Precio + precioServicio.ListaPrecios.Valor;
                        }
                        else
                        {
                            valorServicio = precioServicio.Precio + (precioServicio.Precio * (precioServicio.ListaPrecios.Porcentaje / 100));
                        }
                    }

                    return new OkObjectResult(valorServicio);
                }

                return new BadRequestObjectResult(0);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.GetFullErrorMessage());
            }
        }
    }
}
