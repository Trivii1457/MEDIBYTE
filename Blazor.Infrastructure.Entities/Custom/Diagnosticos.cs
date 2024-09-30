using Dominus.Backend.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class Diagnosticos
    {
        [NotMapped]
        [DDisplayName("Diagnosticos.CodigoDescripcion")]
        public string DescripcionCompleta
        {
            get
            {
                return Codigo + " - " + Descripcion;
            }
            private set
            {
            }
        }

    }
}
