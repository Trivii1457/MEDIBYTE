using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class HCTiposPreguntasModel
   {
      public HCTiposPreguntas Entity { get; set; }

      public HCTiposPreguntasModel()
      {
         Entity = new HCTiposPreguntas();
      }

   }

}
