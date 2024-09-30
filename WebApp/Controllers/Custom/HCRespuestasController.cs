using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Backend.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blazor.WebApp.Controllers
{
    public partial class HCRespuestasController 
    {
        [HttpPost]
        public LoadResult GetTipoControles(DataSourceLoadOptions loadOptions)
        {
            List<KeyValue> tiposPreguntas = new List<KeyValue>();
            tiposPreguntas.Add(new KeyValue { Id = "1", Key = 1, Value = "Texto" });
            //tiposPreguntas.Add(new KeyValue { Id = "2", Key = 2, Value = "Area Texto" });
            //tiposPreguntas.Add(new KeyValue { Id = "3", Key = 3, Value = "Checkbox" });
            //tiposPreguntas.Add(new KeyValue { Id = "4", Key = 4, Value = "Radio Button" });
            //tiposPreguntas.Add(new KeyValue { Id = "5", Key = 4, Value = "Lista" });

            return DataSourceLoader.Load(tiposPreguntas, loadOptions);
        }
    }
}
