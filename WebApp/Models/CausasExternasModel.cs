using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class CausasExternasModel
   {
      public CausasExternas Entity { get; set; }

      public CausasExternasModel()
      {
         Entity = new CausasExternas();
      }

   }

}
