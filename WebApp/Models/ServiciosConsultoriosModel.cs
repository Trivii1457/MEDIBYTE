using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ServiciosConsultoriosModel
   {
      public ServiciosConsultorios Entity { get; set; }

      public ServiciosConsultoriosModel()
      {
         Entity = new ServiciosConsultorios();
      }

   }

}
