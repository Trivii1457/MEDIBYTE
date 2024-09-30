using Dominus.Backend.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class AdmisionesServiciosPrestados 
    {
        [NotMapped]
        [DDisplayName("AdmisionesServiciosPrestados.Total")]
        public decimal ValorTotal
        {
            get
            {
                return Cantidad * ValorServicio;
            }
            private set
            {
            }
        }

        [NotMapped]
        [DDisplayName("AdmisionesServiciosPrestados.EsCorrecion")]
        public bool EsCorrecion { get; set; }
    }
}