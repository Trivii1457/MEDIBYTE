using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ConsultoriosModel
   {
      public Consultorios Entity { get; set; }

      public ConsultoriosModel()
      {
         Entity = new Consultorios();
      }

   }

}
