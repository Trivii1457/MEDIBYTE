using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class FormasPagosModel
   {
      public FormasPagos Entity { get; set; }

      public FormasPagosModel()
      {
         Entity = new FormasPagos();
      }

   }

}
