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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using WidgetGallery;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class AdmisionesCorrecionController : BaseAppController
    {
        public AdmisionesCorrecionController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            var result = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().Tabla(true)
                    .Include(x => x.Atenciones.Admisiones.Pacientes)
                    .Include(x => x.Atenciones.Admisiones.ProgramacionCitas.Entidades)
                    .Include(x => x.Atenciones.Admisiones.Convenios)
                    .Include(x => x.Atenciones.Admisiones.ProgramacionCitas.Servicios)
                    .Include(x => x.Atenciones.Admisiones.ProgramacionCitas.Consultorios)
                    .Include(x => x.Atenciones.Admisiones.ProgramacionCitas.Empleados)
                    .Where(x => x.Admisiones.EstadosId != 72 && !x.Facturado)
                    .Select(x => x.Atenciones).Distinct();

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
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private AdmisionesModel EditModel(long Id)
        {
            AdmisionesModel model = new AdmisionesModel();
            model.Entity = Manager().GetBusinessLogic<Admisiones>().FindById(x => x.Id == Id, true);
            model.Entity.IsNew = false;
            model.EsCorrecion = true;
            return model;
        }


        [HttpPost]
        public IActionResult Edit(AdmisionesModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        private AdmisionesModel EditModel(AdmisionesModel model)
        {
            ViewBag.Accion = "Save";
            try
            {
                var dataAdmision = Manager().GetBusinessLogic<Admisiones>().FindById(x => x.Id == model.Entity.Id, false);
                var dataCita = Manager().GetBusinessLogic<ProgramacionCitas>().FindById(x => x.Id == dataAdmision.ProgramacionCitasId, false);

                dataCita.ServiciosId = model.Entity.ProgramacionCitas.ServiciosId;
                dataCita.EntidadesId = model.Entity.ProgramacionCitas.EntidadesId;
                dataCita.ConveniosId = model.Entity.ConveniosId;
                dataCita.LastUpdate = DateTime.Now;
                dataCita.UpdatedBy = User.Identity.Name;

                dataAdmision.ConveniosId = model.Entity.ConveniosId;
                dataAdmision.FilialesId = model.Entity.FilialesId;
                dataAdmision.NroAutorizacion = model.Entity.NroAutorizacion;
                dataAdmision.FechaAutorizacion = model.Entity.FechaAutorizacion;
                dataAdmision.NumeroPoliza = model.Entity.NumeroPoliza;
                dataAdmision.EsControl = model.Entity.EsControl;
                dataAdmision.DiagnosticosId = model.Entity.DiagnosticosId;
                dataAdmision.TiposUsuariosId = model.Entity.TiposUsuariosId;
                dataAdmision.CoberturaPlanBeneficiosId = model.Entity.CoberturaPlanBeneficiosId;
                dataAdmision.LastUpdate = DateTime.Now;
                dataAdmision.UpdatedBy = User.Identity.Name;
                dataCita = Manager().GetBusinessLogic<ProgramacionCitas>().Modify(dataCita);
                dataAdmision = Manager().GetBusinessLogic<Admisiones>().Modify(dataAdmision);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage());
            }
            return model;
        }

        #endregion

    }
}
