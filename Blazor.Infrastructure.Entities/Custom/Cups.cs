using Dominus.Backend.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class Cups
    {

        [NotMapped]
        [DDisplayName("Cups.DescripcionCompleta")]
        public string DescripcionCompleta
        {
            get
            {
                return Codigo + " | " + Descripcion ;
            }
            private set
            {
            }
        }

    }
}
