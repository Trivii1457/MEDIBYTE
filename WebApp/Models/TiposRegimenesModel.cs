using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class TiposRegimenesModel
   {
      public TiposRegimenes Entity { get; set; }

      public TiposRegimenesModel()
      {
         Entity = new TiposRegimenes();
      }

   }

}
