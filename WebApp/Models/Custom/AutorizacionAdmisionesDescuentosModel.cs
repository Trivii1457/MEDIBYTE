using Blazor.Infrastructure.Entities;
using System.Collections.Generic;

namespace Blazor.WebApp.Models
{

    public partial class AutorizacionAdmisionesDescuentosModel
    {
        public List<Admisiones> ListAdmisiones { get; set; }
        public bool AutorizaDes { get; set; }
        public string Error { get; set; }
    }

}
