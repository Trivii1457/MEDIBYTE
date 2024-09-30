using Blazor.Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace Blazor.WebApp.Models
{

    public partial class EntregaResultadosModel
    {
        public EntregaResultados Entity { get; set; }
        public string SerializedResultados { get; set; }
        public DateTime Hora { get; set; }

        public EntregaResultadosModel()
        {
            Entity = new EntregaResultados();
            Entity.ContanciaArchivos = new Archivos();
        }

    }

}
