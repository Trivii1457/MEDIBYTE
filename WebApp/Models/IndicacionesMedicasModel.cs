using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class IndicacionesMedicasModel
   {
      public IndicacionesMedicas Entity { get; set; }

      public IndicacionesMedicasModel()
      {
         Entity = new IndicacionesMedicas();
      }

   }

}
