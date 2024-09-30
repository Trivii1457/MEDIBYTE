using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class OrdenesMedicamentosDetallesModel
   {
      public OrdenesMedicamentosDetalles Entity { get; set; }

      public OrdenesMedicamentosDetallesModel()
      {
         Entity = new OrdenesMedicamentosDetalles();
      }

   }

}
