using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

    public partial class ProfileUserModel
    {
        public ProfileUser Entity { get; set; }

        public ProfileUserModel()
        {
            Entity = new ProfileUser();
        }

    }

}
