using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class MediosPagoModel
   {
      public MediosPago Entity { get; set; }

      public MediosPagoModel()
      {
         Entity = new MediosPago();
      }

   }

}
