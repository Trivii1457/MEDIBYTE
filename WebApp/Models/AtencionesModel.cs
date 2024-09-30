using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

    public partial class AtencionesModel
    {
        public Atenciones Entity { get; set; }
        public AtencionesModel()
        {
            Entity = new Atenciones();
        }

    }

}
