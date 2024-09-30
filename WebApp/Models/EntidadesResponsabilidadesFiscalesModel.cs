using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class EntidadesResponsabilidadesFiscalesModel
   {
      public EntidadesResponsabilidadesFiscales Entity { get; set; }

      public EntidadesResponsabilidadesFiscalesModel()
      {
         Entity = new EntidadesResponsabilidadesFiscales();
      }

   }

}
