using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class NotasModel
   {
      public Notas Entity { get; set; }
      public string SerializedPacientes { get; set; }

      public NotasModel()
      {
         Entity = new Notas();
      }

   }

}
