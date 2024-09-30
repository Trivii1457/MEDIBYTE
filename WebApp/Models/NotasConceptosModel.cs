using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class NotasConceptosModel
   {
      public NotasConceptos Entity { get; set; }

      public NotasConceptosModel()
      {
         Entity = new NotasConceptos();
      }

   }

}
