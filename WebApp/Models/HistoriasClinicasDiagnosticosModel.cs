using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class HistoriasClinicasDiagnosticosModel
   {
      public HistoriasClinicasDiagnosticos Entity { get; set; }

      public HistoriasClinicasDiagnosticosModel()
      {
         Entity = new HistoriasClinicasDiagnosticos();
      }

   }

}
