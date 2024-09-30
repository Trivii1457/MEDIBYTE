using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class AdministracionHonorariosLecturasModel
   {
      public AdministracionHonorariosLecturas Entity { get; set; }

      public AdministracionHonorariosLecturasModel()
      {
         Entity = new AdministracionHonorariosLecturas();
      }

   }

}
