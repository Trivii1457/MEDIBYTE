using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class IndicacionesMedicasDetallesModel
   {
      public IndicacionesMedicasDetalles Entity { get; set; }

      public IndicacionesMedicasDetallesModel()
      {
         Entity = new IndicacionesMedicasDetalles();
      }

   }

}
