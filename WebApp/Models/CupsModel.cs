using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class CupsModel
   {
      public Cups Entity { get; set; }

      public CupsModel()
      {
         Entity = new Cups();
      }

   }

}
