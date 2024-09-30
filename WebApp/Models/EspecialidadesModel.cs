using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class EspecialidadesModel
   {
      public Especialidades Entity { get; set; }

      public EspecialidadesModel()
      {
         Entity = new Especialidades();
      }

   }

}
