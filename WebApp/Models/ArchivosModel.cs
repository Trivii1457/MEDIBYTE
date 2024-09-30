using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

    public partial class ArchivosModel
    {
        public Archivos Entity { get; set; }

        public ArchivosModel()
        {
            Entity = new Archivos();
        }

    }

}
