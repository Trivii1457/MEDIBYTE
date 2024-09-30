using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class MedicamentosFormasFarmaceuticasModel
   {
      public MedicamentosFormasFarmaceuticas Entity { get; set; }

      public MedicamentosFormasFarmaceuticasModel()
      {
         Entity = new MedicamentosFormasFarmaceuticas();
      }

   }

}
