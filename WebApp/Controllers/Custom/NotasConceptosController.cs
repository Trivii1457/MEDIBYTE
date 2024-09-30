using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Backend.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blazor.WebApp.Controllers
{


    public partial class NotasConceptosController 
    {

        [HttpPost]
        public LoadResult GetTiposNotas(DataSourceLoadOptions loadOptions)
        {
            List<KeyValue> tiposNotas = new List<KeyValue>();
            tiposNotas.Add(new KeyValue { Id = "3", Key = 3, Value = "Nota Credito" });
            tiposNotas.Add(new KeyValue { Id = "4", Key = 4, Value = "Nota Debito" });

            return DataSourceLoader.Load(tiposNotas, loadOptions);
        }

    }
}
