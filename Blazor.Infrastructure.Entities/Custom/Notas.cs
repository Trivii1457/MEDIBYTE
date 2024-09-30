using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class Notas 
    {
        [NotMapped]
        public List<NotasDetalles> NotasDetalles { get; set; }
    }
 }
