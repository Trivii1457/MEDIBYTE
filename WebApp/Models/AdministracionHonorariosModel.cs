using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class AdministracionHonorariosModel
   {
      public AdministracionHonorarios Entity { get; set; }

      public AdministracionHonorariosModel()
      {
         Entity = new AdministracionHonorarios();
      }

   }

}
