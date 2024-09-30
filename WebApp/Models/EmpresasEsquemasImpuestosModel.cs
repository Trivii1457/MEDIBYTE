using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class EmpresasEsquemasImpuestosModel
   {
      public EmpresasEsquemasImpuestos Entity { get; set; }

      public EmpresasEsquemasImpuestosModel()
      {
         Entity = new EmpresasEsquemasImpuestos();
      }

   }

}
