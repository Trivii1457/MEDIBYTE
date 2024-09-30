using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class EstadosModel
   {
      public Estados Entity { get; set; }

      public EstadosModel()
      {
         Entity = new Estados();
      }

   }

}
