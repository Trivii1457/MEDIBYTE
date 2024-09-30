using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class HistoriasClinicasController
    {
        private HistoriasClinicas GetEntityData(long Id)
        {
            return Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(true)
                .Include(x => x.HCTipos.Especialidades)

                .FirstOrDefault(x => x.Id == Id);
        }

        protected Estados GetEstado()
        {
            return Manager().GetBusinessLogic<Estados>().FindById(x => x.Tipo == "HISTORIA CLINICA" && x.Nombre == "ABIERTA", false);
        }

        [HttpGet]
        public IActionResult OpenHC(int atentionId, int hcTipoId, long causaExternaId, long finalidadconsultaId)
        {
            HistoriasClinicasModel model = new HistoriasClinicasModel();
            model.Entity = Manager().GetBusinessLogic<HistoriasClinicas>().FindById(x => x.AtencionesId == atentionId, false);
            Atenciones aten = Manager().GetBusinessLogic<Atenciones>().Tabla(true).Include(x => x.Admisiones.Pacientes).FirstOrDefault(x=>x.Id == atentionId);
            if (model.Entity == null)
            {
                Estados est = GetEstado();
                model.Entity = new HistoriasClinicas();
                model.Entity.AtencionesId = atentionId;
                model.Entity.Atenciones = aten;
                model.Entity.EstadosId = est.Id;
                model.Entity.Consecutivo = $"{aten.Admisiones.Pacientes.NumeroIdentificacion}_{DateTime.Now.ToString("yyyyMMddHH24mmss")}";
                model.Entity.FechaApertura = DateTime.Now;
                model.Entity.PacientesId = aten.Admisiones.PacientesId;
                model.Entity.ProfesionalId = aten.EmpleadosId;
                model.Entity.HCTiposId = hcTipoId;
                model.Entity.LastUpdate = DateTime.Now;
                model.Entity.UpdatedBy = User.Identity.Name;
                model.Entity.CreationDate = DateTime.Now;
                model.Entity.CreatedBy = User.Identity.Name;
                model.Entity.EsControl = aten.Admisiones.EsControl;
                model.Entity.IsNew = true;
                model.Entity.Id = 0;
                model.Entity = Manager().GetBusinessLogic<HistoriasClinicas>().Add(model.Entity);
                var tiposPreguntas = Manager().GetBusinessLogic<HCTiposPreguntas>().FindAll(x=>x.HCTiposId== model.Entity.HCTiposId, false).Select(x=>x.HCPreguntasId).ToList();

                var respuestas = Manager().GetBusinessLogic<HCRespuestas>().FindAll(x => tiposPreguntas.Contains( x.HCPreguntaId) , true).ToList();
                if (respuestas != null && respuestas.Count > 0)
                    foreach (var item in respuestas)
                    {
                        HistoriasClinicasRespuestas reps = new HistoriasClinicasRespuestas();
                        reps.HCRespuestasId = item.Id;
                        reps.HIstoriasClinicasId = model.Entity.Id;
                        reps.LastUpdate = DateTime.Now;
                        reps.UpdatedBy = User.Identity.Name;
                        reps.CreationDate = DateTime.Now;
                        reps.CreatedBy = User.Identity.Name;
                        Manager().GetBusinessLogic<HistoriasClinicasRespuestas>().Add(reps);
                    }
 
            }

            aten.CausasExternasId = causaExternaId;
            aten.FinalidadConsultaId = finalidadconsultaId;
            Manager().GetBusinessLogic<Atenciones>().Modify(aten);

            model.Entity = Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(true).Include(x => x.HCTipos.Especialidades).FirstOrDefault(x => x.Id == model.Entity.Id);
            model.Preguntas = Manager().GetBusinessLogic<HCTiposPreguntas>().FindAll(x => x.HCTiposId == model.Entity.HCTiposId, true).Select(x => x.HCPreguntas).ToList();
            model.Entity.IsNew = false;
            model.EsMismoUsuario = true;
            return PartialView("Edit", model);
        }

        [HttpPost]
        public IActionResult CerrarHC(HistoriasClinicasModel model)
        {

            var totalDiagnosticosHC = Manager().GetBusinessLogic<HistoriasClinicasDiagnosticos>().Tabla(true).Count(x => x.HistoriasClinicasId == model.Entity.Id);
            if (totalDiagnosticosHC <= 0)
            {
                ModelState.AddModelError("Entity.Id", "Para cerrar la historia clinica debe tener al menos un diagnostico registrado.");
            }
            if (model.Entity.Peso <= 0)
            {
                ModelState.AddModelError("Entity.Peso", "Para cerrar la historia clinica debe registrar un peso mayor a cero (0).");
            }
            if (model.Entity.Altura <= 0)
            {
                ModelState.AddModelError("Entity.Altura", "Para cerrar la historia clinica debe registrar una altura mayor a cero (0).");
            }
            if (string.IsNullOrWhiteSpace(model.Entity.AntecedentesFamiliar))
            {
                ModelState.AddModelError("Entity.AntecedentesFamiliar", "Para cerrar la historia clinica debe registrar el antecedente familiar.");
            }
            if (string.IsNullOrWhiteSpace(model.Entity.AntecedentesPersonal))
            {
                ModelState.AddModelError("Entity.AntecedentesPersonal", "Para cerrar la historia clinica debe registrar el antecedente personal.");
            }
            if (string.IsNullOrWhiteSpace(model.Entity.MotivoConsulta))
            {
                ModelState.AddModelError("Entity.MotivoConsulta", "Para cerrar la historia clinica debe registrar el motivo de la consulta.");
            }
            if (string.IsNullOrWhiteSpace(model.Entity.EnfermedadActual))
            {
                ModelState.AddModelError("Entity.EnfermedadActual", "Para cerrar la historia clinica debe registrar la enfermedad actual.");
            }
            if (string.IsNullOrWhiteSpace(model.Entity.PlanManejo))
            {
                ModelState.AddModelError("Entity.PlanManejo", "Para cerrar la historia clinica debe registrar el plan de manejo.");
            }

            model.Entity = Manager().GetBusinessLogic<HistoriasClinicas>().Tabla(true)
                .Include(x => x.HCTipos.Especialidades)
                .Include(x => x.Atenciones.Admisiones)
                .FirstOrDefault(x => x.Id == model.Entity.Id);
            model.Preguntas = Manager().GetBusinessLogic<HCTiposPreguntas>().FindAll(x => x.HCTiposId == model.Entity.HCTiposId, true).Select(x => x.HCPreguntas).ToList();
            model.Entity.IsNew = false;
            model.EsMismoUsuario = true;

            var keyPacientes = ModelState.Where(x => x.Key.StartsWith("Entity.Atenciones")).Select(x => x.Key).ToList();
            foreach (var key in keyPacientes)
            {
                ModelState.Remove(key);
            }

            if (ModelState.IsValid)
            {
                var estadoIncial = model.Entity.EstadosId;
                try
                {
                    model.Entity.LastUpdate = DateTime.Now;
                    model.Entity.UpdatedBy = User.Identity.Name;
                    model.Entity.EstadosId = 19;
                    model.Entity = Manager().GetBusinessLogic<HistoriasClinicas>().Modify(model.Entity);
                }
                catch (Exception e)
                {
                    model.Entity.EstadosId = estadoIncial;
                    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage());
                    return PartialView("Edit", model);
                }

                Manager().AtencionesBusinessLogic().AddAtencion(model.Entity.Atenciones);
                ViewBag.Accion = "CerrarHC";
            }

            return PartialView("Edit", model);

            //var estadoIncial = model.Entity.EstadosId;
            //try
            //{
            //    model.Entity.LastUpdate = DateTime.Now;
            //    model.Entity.UpdatedBy = User.Identity.Name;
            //    model.Entity.EstadosId = 19;
            //    model.Entity = Manager().GetBusinessLogic<HistoriasClinicas>().Modify(model.Entity);
            //}
            //catch (Exception e)
            //{
            //    model.Entity.EstadosId = estadoIncial;
            //    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage());
            //    return PartialView("Edit", model);
            //}

            //Manager().AtencionesBusinessLogic().AddAtencion(model.Entity.Atenciones);
            //ViewBag.Accion = "CerrarHC";
            //return PartialView("Edit", model);
        }
    }
}
 