using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class OrdenesServiciosDetallesModel
   {
      public OrdenesServiciosDetalles Entity { get; set; }

      public OrdenesServiciosDetallesModel()
      {
         Entity = new OrdenesServiciosDetalles();
      }

   }

}
