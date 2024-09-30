using Blazor.Infrastructure.Entities;
using Dominus.Backend.Data;

namespace Blazor.WebApp.Models
{

    public partial class ListaPreciosModel
    {
        public ListaPrecios Entity { get; set; }
        [DDisplayName("ListaPrecios.NombreDuplicar")]
        public string NombreDuplicar { get; set; }
        [DDisplayName("ListaPrecios.TipoEstadoDuplicar")]
        public int TipoEstadoDuplicar { get; set; }
        [DDisplayName("ListaPrecios.ValorDuplicar")]
        public decimal ValorDuplicar { get; set; }
        [DDisplayName("ListaPrecios.PorcentajeDuplicar")]
        public decimal PorcentajeDuplicar { get; set; }
        [DDisplayName("ListaPrecios.PorcentajeIncrementoServicio")]
        public decimal PorcentajeIncrementoServicio { get; set; }
        public ListaPreciosModel()
      {
         Entity = new ListaPrecios();
      }

   }

}
