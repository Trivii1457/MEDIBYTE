using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

    public partial class ProfileModel
    {
        public Profile Entity { get; set; }

        public ProfileModel()
        {
            Entity = new Profile();
        }

    }

}
