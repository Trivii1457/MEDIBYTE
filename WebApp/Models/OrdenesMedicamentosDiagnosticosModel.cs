using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class OrdenesMedicamentosDiagnosticosModel
   {
      public OrdenesMedicamentosDiagnosticos Entity { get; set; }

      public OrdenesMedicamentosDiagnosticosModel()
      {
         Entity = new OrdenesMedicamentosDiagnosticos();
      }

   }

}
