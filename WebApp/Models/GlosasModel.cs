using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class GlosasModel
   {
      public Glosas Entity { get; set; }

      public GlosasModel()
      {
         Entity = new Glosas();
      }

   }

}
