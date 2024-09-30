using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class FacturasDetallesModel
   {
      public FacturasDetalles Entity { get; set; }

      public FacturasDetallesModel()
      {
         Entity = new FacturasDetalles();
      }

   }

}
