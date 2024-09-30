using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class OrdenesServiciosDiagnosticosModel
   {
      public OrdenesServiciosDiagnosticos Entity { get; set; }

      public OrdenesServiciosDiagnosticosModel()
      {
         Entity = new OrdenesServiciosDiagnosticos();
      }

   }

}
