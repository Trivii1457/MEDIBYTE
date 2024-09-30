using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

    public partial class UserModel
    {
        public User Entity { get; set; }
        public bool ModifyPassword { get; set; }

        public UserModel()
        {
            Entity = new User();
        }

    }

}
