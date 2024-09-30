using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class TiposAtencionModel
   {
      public TiposAtencion Entity { get; set; }

      public TiposAtencionModel()
      {
         Entity = new TiposAtencion();
      }

   }

}
