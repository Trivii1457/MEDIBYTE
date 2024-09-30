using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class LanguagesModel
   {
      public Languages Entity { get; set; }

      public LanguagesModel()
      {
         Entity = new Languages();
      }

   }

}
