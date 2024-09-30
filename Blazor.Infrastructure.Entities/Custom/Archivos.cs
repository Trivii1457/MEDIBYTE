using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{
    public partial class Archivos 
    {
        [NotMapped]
        public string NombreCampoForanea { get; set; }
        [NotMapped]
        public string StringToBase64 { get; set; }
        [NotMapped]
        public bool EliminarArchivo { get; set; }
        [NotMapped]
        public bool EsArchivo { get; set; }

    }
 }
