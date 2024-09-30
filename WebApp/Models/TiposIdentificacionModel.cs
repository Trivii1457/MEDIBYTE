using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class TiposIdentificacionModel
   {
      public TiposIdentificacion Entity { get; set; }

      public TiposIdentificacionModel()
      {
         Entity = new TiposIdentificacion();
      }

   }

}
