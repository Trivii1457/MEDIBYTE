using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ConveniosModel
   {
      public Convenios Entity { get; set; }

      public ConveniosModel()
      {
         Entity = new Convenios();
      }

   }

}
