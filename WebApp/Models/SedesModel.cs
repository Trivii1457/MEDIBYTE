using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

    public partial class SedesModel
    {
        public Sedes Entity { get; set; }

        public long PaisId { get; set; }
        public long DepartamentoId { get; set; }


        public SedesModel()
        {
            Entity = new Sedes();
        }

    }

}
