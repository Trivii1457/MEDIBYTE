using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class EmpresasResponsabilidadesFiscalesModel
   {
      public EmpresasResponsabilidadesFiscales Entity { get; set; }

      public EmpresasResponsabilidadesFiscalesModel()
      {
         Entity = new EmpresasResponsabilidadesFiscales();
      }

   }

}
