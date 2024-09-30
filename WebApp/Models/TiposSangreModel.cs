using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class TiposSangreModel
   {
      public TiposSangre Entity { get; set; }

      public TiposSangreModel()
      {
         Entity = new TiposSangre();
      }

   }

}
