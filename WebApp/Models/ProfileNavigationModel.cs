using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ProfileNavigationModel
   {
      public ProfileNavigation Entity { get; set; }

      public ProfileNavigationModel()
      {
         Entity = new ProfileNavigation();
      }

   }

}
