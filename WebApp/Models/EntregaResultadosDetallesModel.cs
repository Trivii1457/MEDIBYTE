using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class EntregaResultadosDetallesModel
   {
      public EntregaResultadosDetalles Entity { get; set; }

      public EntregaResultadosDetallesModel()
      {
         Entity = new EntregaResultadosDetalles();
      }

   }

}
