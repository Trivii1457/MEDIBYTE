using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class RecaudosModel
   {
      public Recaudos Entity { get; set; }

      public RecaudosModel()
      {
         Entity = new Recaudos();
      }

   }

}
