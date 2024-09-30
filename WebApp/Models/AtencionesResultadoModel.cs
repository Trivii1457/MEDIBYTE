using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

    public partial class AtencionesResultadoModel
    {
        public AtencionesResultado Entity { get; set; }

        public string StringToBase64 { get; set; }
        public AtencionesResultadoModel()
        {
            Entity = new AtencionesResultado();
        }

    }

}
