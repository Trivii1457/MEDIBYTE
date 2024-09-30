using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class TiposContratosModel
   {
      public TiposContratos Entity { get; set; }

      public TiposContratosModel()
      {
         Entity = new TiposContratos();
      }

   }

}
