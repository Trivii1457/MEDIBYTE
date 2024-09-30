using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class HistoriasClinicasNotasAclaratoriasModel
   {
      public HistoriasClinicasNotasAclaratorias Entity { get; set; }

      public HistoriasClinicasNotasAclaratoriasModel()
      {
         Entity = new HistoriasClinicasNotasAclaratorias();
      }

   }

}
