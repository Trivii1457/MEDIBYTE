using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class EmpleadoProfesionesModel
   {
      public EmpleadoProfesiones Entity { get; set; }

      public EmpleadoProfesionesModel()
      {
         Entity = new EmpleadoProfesiones();
      }

   }

}
