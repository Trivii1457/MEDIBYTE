using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class RipsModel
   {
      public Rips Entity { get; set; }

      public RipsModel()
      {
         Entity = new Rips();
      }

   }

}
