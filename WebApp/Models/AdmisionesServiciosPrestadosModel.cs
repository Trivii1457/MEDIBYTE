using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

    public partial class AdmisionesServiciosPrestadosModel
    {
        public AdmisionesServiciosPrestados Entity { get; set; }
        public AdmisionesServiciosPrestadosModel()
        {
            Entity = new AdmisionesServiciosPrestados();
        }

    }

}
