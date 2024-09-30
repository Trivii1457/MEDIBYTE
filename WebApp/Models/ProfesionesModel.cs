using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ProfesionesModel
   {
      public Profesiones Entity { get; set; }

      public ProfesionesModel()
      {
         Entity = new Profesiones();
      }

   }

}
