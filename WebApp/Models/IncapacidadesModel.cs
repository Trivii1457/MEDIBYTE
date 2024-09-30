using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class IncapacidadesModel
   {
      public Incapacidades Entity { get; set; }

      public IncapacidadesModel()
      {
         Entity = new Incapacidades();
      }

   }

}
