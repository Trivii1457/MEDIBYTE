using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class EstadosCivilesModel
   {
      public EstadosCiviles Entity { get; set; }

      public EstadosCivilesModel()
      {
         Entity = new EstadosCiviles();
      }

   }

}
