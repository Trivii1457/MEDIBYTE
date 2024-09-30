using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class OrdenesMedicamentosModel
   {
      public OrdenesMedicamentos Entity { get; set; }

      public OrdenesMedicamentosModel()
      {
         Entity = new OrdenesMedicamentos();
      }

   }

}
