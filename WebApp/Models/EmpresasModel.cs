using Blazor.Infrastructure.Entities;
using System.IO;

namespace Blazor.WebApp.Models
{

    public partial class EmpresasModel
    {
        public Empresas Entity { get; set; }

        public long PaisesId { get; set; }
        public long DepartamentosId { get; set; }

        public EmpresasModel()
        {
            Entity = new Empresas();
            Entity.LogoArchivos = new Archivos();
        }

    }

}
