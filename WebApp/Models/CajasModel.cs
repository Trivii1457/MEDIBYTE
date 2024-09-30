using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class CajasModel
   {
      public Cajas Entity { get; set; }

      public CajasModel()
      {
         Entity = new Cajas();
      }

   }

}
