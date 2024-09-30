using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class MedicamentosModel
   {
      public Medicamentos Entity { get; set; }

      public MedicamentosModel()
      {
         Entity = new Medicamentos();
      }

   }

}
