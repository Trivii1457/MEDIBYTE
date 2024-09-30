using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ProgramacionDisponibleModel
   {
      public ProgramacionDisponible Entity { get; set; }

      public ProgramacionDisponibleModel()
      {
         Entity = new ProgramacionDisponible();
      }

   }

}
