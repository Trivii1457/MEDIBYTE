using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExpress.XtraReports.UI;
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
using System.IO;
using System.Linq;
using WebApp.Reportes.AtencionesResultado;


//using NAudio.Lame;
//using NAudio.Wave;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class AtencionesResultadoController : BaseAppController
    {

        //private const string Prefix = "AtencionesResultado"; 

        public AtencionesResultadoController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            var result = Manager().GetBusinessLogic<AtencionesResultado>().Tabla(true).Where(x => x.EstadosId == 66 || x.EstadosId == 67)
                .Include(x => x.AdmisionesServiciosPrestados.Admisiones)
                .Include(x => x.AdmisionesServiciosPrestados.Atenciones)
                .Include(x => x.AdmisionesServiciosPrestados.Servicios)
                .Include(x => x.AdmisionesServiciosPrestados.Admisiones.Pacientes)
                .Select(x => new { x.Id, x.AdmisionesServiciosPrestados, x.Estados, x.Entregado });
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

        private AtencionesResultadoModel NewModel()
        {
            AtencionesResultadoModel model = new AtencionesResultadoModel();
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult NewAtencionLecturaAudio(long Id)
        {
            return PartialView("EditAudio", NewAtencionLecturaAudioModel(Id));
        }

        private AtencionesResultadoModel NewAtencionLecturaAudioModel(long Id)
        {
            AtencionesResultadoModel model = new AtencionesResultadoModel();
            model.Entity.AdmisionesServiciosPrestadosId = Id;
            model.Entity.AdmisionesServiciosPrestados = Manager().GetBusinessLogic<AdmisionesServiciosPrestados>().FindById(x => x.Id == Id, true);
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private AtencionesResultadoModel EditModel(long Id)
        {
            AtencionesResultadoModel model = new AtencionesResultadoModel();
            model.Entity = Manager().GetBusinessLogic<AtencionesResultado>().Tabla(true)
                .Include(x => x.AdmisionesServiciosPrestados.Atenciones).FirstOrDefault(x => x.Id == Id); //.FindById(x => x.Id == Id, true);
            model.StringToBase64 = DApp.Util.ArrayBytesToString(model.Entity.ResultadoAudio);
            model.Entity.IsNew = false;
            return model;
        }

        [HttpPost]
        public IActionResult Edit(AtencionesResultadoModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        private AtencionesResultadoModel EditModel(AtencionesResultadoModel model)
        {
            ViewBag.Accion = "Save";
            var OnState = model.Entity.IsNew;

            var llaves = ModelState.Where(x => x.Key.Contains("Entity.AdmisionesServiciosPrestados.")).Select(x => x.Key).ToList();
            foreach (var key in llaves)
            {
                ModelState.Remove(key);
            }

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(model.Entity.Resultado))
                {
                    model.Entity.EstadosId = 67;
                }

                try
                {
                    model.Entity.LastUpdate = DateTime.Now;
                    model.Entity.UpdatedBy = User.Identity.Name;
                    if (model.Entity.IsNew)
                    {
                        model.Entity.CreationDate = DateTime.Now;
                        model.Entity.CreatedBy = User.Identity.Name;
                        model.Entity = Manager().GetBusinessLogic<AtencionesResultado>().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        model.Entity.ResultadoAudio = Manager().GetBusinessLogic<AtencionesResultado>().FindById(x => x.Id == model.Entity.Id, false)?.ResultadoAudio;
                        model.Entity = Manager().GetBusinessLogic<AtencionesResultado>().Modify(model.Entity);
                    }
                    model.StringToBase64 = DApp.Util.ArrayBytesToString(model.Entity.ResultadoAudio);
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
        public IActionResult Delete(AtencionesResultadoModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private AtencionesResultadoModel DeleteModel(AtencionesResultadoModel model)
        {
            ViewBag.Accion = "Delete";
            AtencionesResultadoModel newModel = NewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<AtencionesResultado>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<AtencionesResultado>().Remove(model.Entity);
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

        private AtencionesResultadoModel NewModelDetail(long IdFather) 
        { 
            AtencionesResultadoModel model = new AtencionesResultadoModel(); 
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
        public IActionResult EditDetail(AtencionesResultadoModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(AtencionesResultadoModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private AtencionesResultadoModel DeleteModelDetail(AtencionesResultadoModel model)
        { 
            ViewBag.Accion = "Delete"; 
            AtencionesResultadoModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<AtencionesResultado>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<AtencionesResultado>().Remove(model.Entity); 
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
             AtencionesResultado entity = new AtencionesResultado(); 
             JsonConvert.PopulateObject(values, entity); 
             AtencionesResultadoModel model = new AtencionesResultadoModel(); 
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
             AtencionesResultado entity = Manager().GetBusinessLogic<AtencionesResultado>().FindById(x => x.Id == key, false); 
             JsonConvert.PopulateObject(values, entity); 
             AtencionesResultadoModel model = new AtencionesResultadoModel(); 
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
             AtencionesResultado entity = Manager().GetBusinessLogic<AtencionesResultado>().FindById(x => x.Id == key, false); 
             Manager().GetBusinessLogic<AtencionesResultado>().Remove(entity); 
        } 

        */
        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetAtencionesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Atenciones>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetEstaddosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Estados>().Tabla(true), loadOptions);
        }
        #endregion


        [HttpPost]
        public LoadResult GetArchivosResultado(DataSourceLoadOptions loadOptions, long admisionesServiciosPrestadosId)
        {
            var result = Manager().GetBusinessLogic<AdmisionesServiciosPrestadosArchivos>().Tabla(true)
                .Where(x => x.AdmisionesServiciosPrestados.Id == admisionesServiciosPrestadosId);
            return DataSourceLoader.Load(result, loadOptions);
        }

        [HttpPost]
        public LoadResult GetImagenesDiagnosticas(DataSourceLoadOptions loadOptions, long admisionesServiciosPrestadosId)
        {
            var result = Manager().GetBusinessLogic<ImagenesDiagnosticasDetalle>().Tabla(true)
                .Where(x => x.AdmisionesServiciosPrestados.Id == admisionesServiciosPrestadosId);
            return DataSourceLoader.Load(result, loadOptions);
        }

        [HttpPost]
        public IActionResult SubirAudioAResultado(IFormFile audioarchivoAudio, long admisionesServiciosPrestadosId, long id)
        {
            try
            {
                if (audioarchivoAudio == null)
                {
                    return BadRequest("No se grabo un audio en la forma.");
                }

                if (audioarchivoAudio.Length <= 0)
                {
                    return BadRequest("No se grabo un audio en la forma.");
                }

                if (admisionesServiciosPrestadosId == 0)
                {
                    return BadRequest("Error en la atencion del servicio prestado.");
                }

                byte[] bytesAudio = null;
                using (var ms = new MemoryStream())
                {
                    audioarchivoAudio.CopyTo(ms);
                    bytesAudio = ms.ToArray();
                }

                //using (var retMs = new MemoryStream())
                //using (var ms = new MemoryStream(bytesAudio))
                //using (var rdr = new WaveFileReader(ms))
                //using (var wtr = new LameMP3FileWriter(retMs, rdr.WaveFormat, LAMEPreset.MEDIUM))
                //{
                //    rdr.CopyTo(wtr);
                //    bytesAudio = retMs.ToArray();
                //}

                try
                {
                    AtencionesResultado atencionesResultado = Manager().AtencionesResultadoBusinessLogic().SubirAudioAResultado(bytesAudio, admisionesServiciosPrestadosId, id, User.Identity.Name, this.ActualUsuarioId());
                    if (atencionesResultado == null)
                    {
                        return new BadRequestObjectResult("Error grabando registro de audio.");
                    }
                }
                catch (Exception e)
                {
                    return new BadRequestObjectResult(e.GetFullErrorMessage());
                }
                
                return PartialView("../AtencionesLectura/List");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new BadRequestObjectResult("Error convirtiendo archivo a mp3. | " + e.Message);
            }
        }

        [HttpGet]
        public IActionResult ImprimirAtencionesResultadoPorId(int Id)
        {
            try
            {
                InformacionReporte informacionReporte = new InformacionReporte();
                informacionReporte.Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == this.ActualEmpresaId(), true);
                informacionReporte.BD = DApp.GetTenantConnection(Request.Host.Value);
                informacionReporte.Ids = new long[] { Id };
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
