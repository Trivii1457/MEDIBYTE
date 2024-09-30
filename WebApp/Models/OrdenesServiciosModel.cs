using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class OrdenesServiciosModel
   {
      public OrdenesServicios Entity { get; set; }

      public OrdenesServiciosModel()
      {
         Entity = new OrdenesServicios();
      }

   }

}
