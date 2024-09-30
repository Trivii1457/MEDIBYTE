using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ServiciosSedesModel
   {
      public ServiciosSedes Entity { get; set; }

      public ServiciosSedesModel()
      {
         Entity = new ServiciosSedes();
      }

   }

}
