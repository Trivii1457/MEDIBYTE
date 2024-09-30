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
using System.Collections.Generic;
using Dominus.Backend.Data;

namespace Blazor.WebApp.Controllers
{

    [Authorize] 
    public partial class EntidadesController
    {
        private EntidadesModel EditModel(long Id)
        {
            EntidadesModel model = new EntidadesModel();
            model.Entity = Manager().GetBusinessLogic<Entidades>().FindById(x => x.Id == Id, false);

            if (model.Entity.CiudadesId > 0)
            {
                model.CiudadesId = model.Entity.CiudadesId;
                Ciudades ciudades = Manager().GetBusinessLogic<Ciudades>().FindById(x => x.Id == model.Entity.CiudadesId, true);
                model.DepartamentosId = ciudades.Departamentos.Id;
                model.PaisesId = ciudades.Departamentos.PaisesId;
            }

            model.Entity.IsNew = false;
            return model;
        }

        public LoadResult GetTiposIdentificacionId(DataSourceLoadOptions loadOptions)
        {
            List<string> tipos = new List<string>() { "NI", "CC", "CE", "CD", "PA", "PE" };
            return DataSourceLoader.Load(Manager().GetBusinessLogic<TiposIdentificacion>().Tabla(true).Where(x => tipos.Contains(x.Codigo)), loadOptions);
        }

        public LoadResult GetResponsableIva(DataSourceLoadOptions loadOptions)
        {
            List<KeyValue> tiposPreguntas = new List<KeyValue>();
            tiposPreguntas.Add(new KeyValue { Id = "48", Key = 1, Value = "SI" });
            tiposPreguntas.Add(new KeyValue { Id = "49", Key = 2, Value = "NO" });
            //tiposPreguntas.Add(new KeyValue { Id = "3", Key = 3, Value = "Checkbox" });
            //tiposPreguntas.Add(new KeyValue { Id = "4", Key = 4, Value = "Radio Button" });
            //tiposPreguntas.Add(new KeyValue { Id = "5", Key = 4, Value = "Lista" });

            return DataSourceLoader.Load(tiposPreguntas, loadOptions);
        }

        [HttpPost]
        public LoadResult GetPaisesId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Paises>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetDepartamentosId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Departamentos>().Tabla(true), loadOptions);
        }

    }
}
