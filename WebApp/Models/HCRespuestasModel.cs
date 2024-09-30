using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class HCRespuestasModel
   {
      public HCRespuestas Entity { get; set; }

      public HCRespuestasModel()
      {
         Entity = new HCRespuestas();
      }

   }

}
