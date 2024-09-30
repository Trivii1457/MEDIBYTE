using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class TiposServiciosModel
   {
      public TiposServicios Entity { get; set; }

      public TiposServiciosModel()
      {
         Entity = new TiposServicios();
      }

   }

}
