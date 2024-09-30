using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class MediosEntregasModel
   {
      public MediosEntregas Entity { get; set; }

      public MediosEntregasModel()
      {
         Entity = new MediosEntregas();
      }

   }

}
