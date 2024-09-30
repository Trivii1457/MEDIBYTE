using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class CategoriasServiciosModel
   {
      public CategoriasServicios Entity { get; set; }

      public CategoriasServiciosModel()
      {
         Entity = new CategoriasServicios();
      }

   }

}
