using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ConveniosServiciosModel
   {
      public ConveniosServicios Entity { get; set; }

      public ConveniosServiciosModel()
      {
         Entity = new ConveniosServicios();
      }

   }

}
