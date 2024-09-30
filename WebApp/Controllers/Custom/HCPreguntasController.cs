using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Backend.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blazor.WebApp.Controllers
{

    public partial class HCPreguntasController 
    {
        

        [HttpPost]
        public LoadResult GetTiposPreguntas(DataSourceLoadOptions loadOptions)
        {
            List<KeyValue> tiposPreguntas = new List<KeyValue>();
            tiposPreguntas.Add(new KeyValue { Id = "1", Key = 1, Value = "Abierta Simple" });
            tiposPreguntas.Add(new KeyValue { Id = "2", Key = 2, Value = "Abierta Multiple" });
            tiposPreguntas.Add(new KeyValue { Id = "3", Key = 3, Value = "Seleccion Simple" });
            tiposPreguntas.Add(new KeyValue { Id = "4", Key = 4, Value = "Seleccion Multiple" });

            return DataSourceLoader.Load(tiposPreguntas, loadOptions);
        }
    }
}
