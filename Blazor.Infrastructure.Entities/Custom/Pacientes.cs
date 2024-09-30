using Dominus.Backend.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class Pacientes
    {
        [NotMapped]
        [DDisplayName("Pacientes.PaisesId")]
        public long? PaisId { get; set; }
        [NotMapped]
        [DDisplayName("Pacientes.DepartamentosId")]
        public long? DepartamentoId { get; set; }

    }
}
