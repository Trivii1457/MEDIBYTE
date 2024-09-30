using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class CategoriasIngresosDetallesModel
   {
      public CategoriasIngresosDetalles Entity { get; set; }

      public CategoriasIngresosDetallesModel()
      {
         Entity = new CategoriasIngresosDetalles();
      }

   }

}
