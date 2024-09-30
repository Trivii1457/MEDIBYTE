using Blazor.Infrastructure.Entities;
using Dominus.Backend.Data;

namespace Blazor.WebApp.Models
{

    public partial class PacientesModel
    {
        public Pacientes Entity { get; set; }
        public bool DesdeCitas { get; set; } = false;
        public bool CitaNueva { get; set; } = false;
        public PacientesModel()
        {
            Entity = new Pacientes();
        }

    }

}
