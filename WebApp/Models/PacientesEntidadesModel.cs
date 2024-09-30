using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class PacientesEntidadesModel
   {
      public PacientesEntidades Entity { get; set; }

      public PacientesEntidadesModel()
      {
         Entity = new PacientesEntidades();
      }

   }

}
