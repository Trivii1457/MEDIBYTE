using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class MedicamentosViaAdministracionModel
   {
      public MedicamentosViaAdministracion Entity { get; set; }

      public MedicamentosViaAdministracionModel()
      {
         Entity = new MedicamentosViaAdministracion();
      }

   }

}
