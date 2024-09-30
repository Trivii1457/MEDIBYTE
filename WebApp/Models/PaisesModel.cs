using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class PaisesModel
   {
      public Paises Entity { get; set; }

      public PaisesModel()
      {
         Entity = new Paises();
      }

   }

}
