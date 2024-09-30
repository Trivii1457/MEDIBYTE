using Dominus.Backend.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class Empleados
    {
        [NotMapped]
        [DDisplayName("Empleados.PaisesId")]
        public long? PaisId { get; set; }
        [NotMapped]
        [DDisplayName("Empleados.DepartamentosId")]
        public long? DepartamentoId { get; set; }

    }
}
