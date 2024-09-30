using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class EmpleadosEspecialidadesModel
   {
      public EmpleadosEspecialidades Entity { get; set; }

      public EmpleadosEspecialidadesModel()
      {
         Entity = new EmpleadosEspecialidades();
      }

   }

}
