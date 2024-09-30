using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class TiposDiagnosticosModel
   {
      public TiposDiagnosticos Entity { get; set; }

      public TiposDiagnosticosModel()
      {
         Entity = new TiposDiagnosticos();
      }

   }

}
