using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class TiposAfiliadosModel
   {
      public TiposAfiliados Entity { get; set; }

      public TiposAfiliadosModel()
      {
         Entity = new TiposAfiliados();
      }

   }

}
