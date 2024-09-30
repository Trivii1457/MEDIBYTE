using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class HCPreguntasModel
   {
      public HCPreguntas Entity { get; set; }

      public HCPreguntasModel()
      {
         Entity = new HCPreguntas();
      }

   }

}
