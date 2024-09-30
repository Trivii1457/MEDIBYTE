using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class EntidadesEsquemasImpuestosModel
   {
      public EntidadesEsquemasImpuestos Entity { get; set; }

      public EntidadesEsquemasImpuestosModel()
      {
         Entity = new EntidadesEsquemasImpuestos();
      }

   }

}
