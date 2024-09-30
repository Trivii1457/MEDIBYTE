using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class LiquidacionHonorariosDetalleModel
   {
      public LiquidacionHonorariosDetalle Entity { get; set; }

      public LiquidacionHonorariosDetalleModel()
      {
         Entity = new LiquidacionHonorariosDetalle();
      }

   }

}
