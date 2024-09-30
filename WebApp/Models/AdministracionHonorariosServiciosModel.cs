using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class AdministracionHonorariosServiciosModel
   {
      public AdministracionHonorariosServicios Entity { get; set; }

      public AdministracionHonorariosServiciosModel()
      {
         Entity = new AdministracionHonorariosServicios();
      }

   }

}
