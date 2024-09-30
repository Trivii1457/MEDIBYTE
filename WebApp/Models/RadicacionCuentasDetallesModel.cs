using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class RadicacionCuentasDetallesModel
   {
      public RadicacionCuentasDetalles Entity { get; set; }

      public RadicacionCuentasDetallesModel()
      {
         Entity = new RadicacionCuentasDetalles();
      }

   }

}
