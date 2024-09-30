using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class CiclosCajasModel
   {
      public CiclosCajas Entity { get; set; }

        public bool TieneCaja { get; set; } = false;
      public CiclosCajasModel()
      {
         Entity = new CiclosCajas();
      }

   }

}
