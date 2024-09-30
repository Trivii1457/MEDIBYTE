using Dominus.Backend.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class User
    {
        [NotMapped]
        public string NombreCompleto
        {
            get
            {
                return UserName + " | " + Names + " " + LastNames;
            }
            private set
            {
            }
        }

        [NotMapped]
        public string NombresYApellidos
        {
            get
            {
                return Names + " " + LastNames;
            }
            private set
            {
            }
        }
    }
}
