using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class GenerosModel
   {
      public Generos Entity { get; set; }

      public GenerosModel()
      {
         Entity = new Generos();
      }

   }

}
