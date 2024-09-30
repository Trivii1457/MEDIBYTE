using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

    public partial class FacturasModel
    {
        public Facturas Entity { get; set; }

        public string UrlReport { get; set; }

        public FacturasModel()
        {
            Entity = new Facturas();
        }

    }

}
