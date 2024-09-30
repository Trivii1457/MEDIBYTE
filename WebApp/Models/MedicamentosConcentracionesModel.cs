using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class MedicamentosConcentracionesModel
   {
      public MedicamentosConcentraciones Entity { get; set; }

      public MedicamentosConcentracionesModel()
      {
         Entity = new MedicamentosConcentraciones();
      }

   }

}
