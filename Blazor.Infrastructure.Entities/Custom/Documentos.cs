using Dominus.Backend.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class Documentos
    {
        [NotMapped]
        [DDisplayName("Documentos.DocumentoCompleto")]
        public string DocumentoCompleto
        {
            get
            {
                return Prefijo + " - " + Descripcion;
            }
            private set
            {
            }
        }

    }
}
