using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class LanguageResourceModel
   {
      public LanguageResource Entity { get; set; }

      public LanguageResourceModel()
      {
         Entity = new LanguageResource();
      }

   }

}
