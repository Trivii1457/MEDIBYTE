using Dominus.Backend.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class Servicios
    {

        [NotMapped]
        [DDisplayName("Servicios.DescripcionCompleta")]
        public string DescripcionCompleta
        {
            get
            {
                return Codigo + " | " + Nombre ;
            }
            private set
            {
            }
        }

    }
}
