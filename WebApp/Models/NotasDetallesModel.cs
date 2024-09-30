using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class NotasDetallesModel
   {
      public NotasDetalles Entity { get; set; }

      public NotasDetallesModel()
      {
         Entity = new NotasDetalles();
      }

   }

}
