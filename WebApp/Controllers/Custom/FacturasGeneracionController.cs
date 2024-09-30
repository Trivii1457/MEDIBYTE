using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Backend.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blazor.WebApp.Controllers
{

    public partial class FacturasGeneracionController
    {
        public Expression<Func<ServiciosFacturar, bool>> Where { get; set; }

        public List<ServiciosFacturar> GetServiciosAFacturar(FacturasGeneracionModel model)
        {
            Int64 fili = (Int64) ((model.Entity.FilialesId != null) ? model.Entity.FilialesId : 0);
            Int64 regi = (Int64)((model.Entity.TiposRegimenesId != null) ? model.Entity.TiposRegimenesId : 0);

            return Manager().GetBusinessLogic<ServiciosFacturar>().FindAll(x => x.Facturado == false && x.FacturasId == null &&
                 x.EmpresasId == model.Entity.EmpresasId &&
                 x.ConvenioId == model.Entity.ConveniosId &&
                 x.EntidadesId == model.Entity.EntidadesId &&
                 x.SedesId == model.Entity.SedesId &&
                 x.FilialesId == fili &&
                 x.TiposRegimenesId == regi &&
                 x.AtencionEstado == 10076 &&
                 x.FechaAtencion >= new System.DateTime(model.Entity.FechaInicio.Year, model.Entity.FechaInicio.Month, model.Entity.FechaInicio.Day) &&
                 x.FechaAtencion <= new System.DateTime(model.Entity.FechaFinal.Year, model.Entity.FechaFinal.Month, model.Entity.FechaFinal.Day));
        }

        [HttpPost]
        public IActionResult Facturar([FromBody]List<ServiciosFacturar> models)
        {
            FacturasGeneracionModel model = new FacturasGeneracionModel();

            try
            {
                model.Entity = Manager().FacturasGeneracionBusinessLogic().GenerarFactura(models, User.Identity.Name);
                model.Process = false;
                return PartialView("Edit", model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Entity.Id", e.Message);
                return PartialView("Edit", model);
            }

            
        }

        [HttpPost]
        public LoadResult GetServiciosFacturados(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ServiciosFacturar>().Tabla(true), loadOptions);
        }

    }
}