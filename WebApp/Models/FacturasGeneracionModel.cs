using Blazor.Infrastructure.Entities;
using System.Collections.Generic;

namespace Blazor.WebApp.Models
{

    public partial class FacturasGeneracionModel
    {
        public FacturasGeneracion Entity { get; set; } = new FacturasGeneracion();

        public List<ServiciosFacturar> DetalleServiciosFacturar { get; set; } = new List<ServiciosFacturar>();

        public bool Process { get; set; } = true;

        public FacturasGeneracionModel()
        {
            Entity = new FacturasGeneracion();
        }

    }

}