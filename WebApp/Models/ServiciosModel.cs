using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ServiciosModel
   {
      public Servicios Entity { get; set; }

      public ServiciosModel()
      {
         Entity = new Servicios();
      }

   }

}
