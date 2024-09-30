using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ServiciosEmpleadosModel
   {
      public ServiciosEmpleados Entity { get; set; }

      public ServiciosEmpleadosModel()
      {
         Entity = new ServiciosEmpleados();
      }

   }

}
