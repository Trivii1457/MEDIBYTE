using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class EventosDIANModel
   {
      public EventosDIAN Entity { get; set; }

      public EventosDIANModel()
      {
         Entity = new EventosDIAN();
      }

   }

}
