using Blazor.Infrastructure.Entities;
using Dominus.Backend.Data;

namespace Blazor.WebApp.Models
{

    public partial class EntidadesModel
    {
        public Entidades Entity { get; set; }

        [DDisplayName("Entidades.DepartamentosId")]
        public long CiudadesId { get; set; }

        [DDisplayName("Entidades.DepartamentosId")]
        public long DepartamentosId { get; set; }

        [DDisplayName("Entidades.PaisesId")]
        public long PaisesId { get; set; }

        public EntidadesModel()
        {
            Entity = new Entidades();
        }

    }

}
