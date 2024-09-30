using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class FestivosModel
   {
      public Festivos Entity { get; set; }

      public FestivosModel()
      {
         Entity = new Festivos();
      }

   }

}
