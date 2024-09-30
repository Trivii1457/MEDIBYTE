using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class CategoriasIngresosModel
   {
      public CategoriasIngresos Entity { get; set; }

      public CategoriasIngresosModel()
      {
         Entity = new CategoriasIngresos();
      }

   }

}
