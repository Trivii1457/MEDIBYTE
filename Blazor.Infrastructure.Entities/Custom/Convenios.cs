using Dominus.Backend.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class Convenios
    {
        [NotMapped]
        [DDisplayName("Convenios.Nombre")]
        public string CodigoNombre
        {
            get
            {
                return $"{Codigo} - {Nombre}";
            }
            private set
            {
            }
        }

    }
}
