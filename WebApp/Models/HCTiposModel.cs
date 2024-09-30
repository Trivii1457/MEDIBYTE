using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class HCTiposModel
   {
      public HCTipos Entity { get; set; }

      public HCTiposModel()
      {
         Entity = new HCTipos();
      }

   }

}
