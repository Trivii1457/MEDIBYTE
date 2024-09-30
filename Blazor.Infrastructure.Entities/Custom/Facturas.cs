using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{
    public partial class Facturas
    {

        [NotMapped]
        public List<AdmisionesServiciosPrestados> ServicioFacturados { get; set; } = new List<AdmisionesServiciosPrestados>();
       
        [NotMapped]
        public List<ServiciosFacturar> ServiciosAfacturar { get; set; } = new List<ServiciosFacturar>();

        [NotMapped]
        public long RadicacionFacturasId { get; set; }

        //public override string ToString()
        //{
        //    return Prefijo + " " + NroConsecutivo.ToString();
        //}

        [NotMapped]
        public List<TotalesPorTipoImpuesto> TotalesPorTipoImpuestos { get; set; }


        [NotMapped]
        public List<FacturasDetalles> FacturasDetalles { get; set; }

    }
}
