using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class TipoEntidadesModel
   {
      public TipoEntidades Entity { get; set; }

      public TipoEntidadesModel()
      {
         Entity = new TipoEntidades();
      }

   }

}
