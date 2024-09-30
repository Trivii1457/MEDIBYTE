using Blazor.Infrastructure.Entities;
using System.Collections.Generic;
using System.IO;

namespace Blazor.WebApp.Models
{

    public partial class RadicacionCuentasModel
    {
        public RadicacionCuentas Entity { get; set; }
        public List<Facturas> DetalleFacturasPorRadicar { get; set; }
        public bool Process { get; set; }

        public RadicacionCuentasModel()
        {
            Entity = new RadicacionCuentas();
            Entity.RadicacionArchivos = new Archivos();
        }

    }

}