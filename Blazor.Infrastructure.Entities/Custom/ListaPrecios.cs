using Dominus.Backend.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class ListaPrecios
    {
        [NotMapped]
        [DDisplayName("ListaPrecios.Total")]
        public decimal? Total { get; set; }
    }
}