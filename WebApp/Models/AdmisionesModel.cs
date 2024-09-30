using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class AdmisionesModel
   {
      public Admisiones Entity { get; set; }

        public CategoriasIngresosDetalles CategoriasIngresosDetalles { get; set; } = new CategoriasIngresosDetalles();

        public decimal valorServicio { get; set; } = 0;

        public long? EntidadesConvenio { get; set; }
        public long ConsultoriosId { get; set; }
        public long? EmpleadosId { get; set; }

        public bool EsCorrecion { get; set; } = false;
        public bool TieneServiciosFacturadosAEntidad { get; set; } = false;

      public AdmisionesModel()
      {
         Entity = new Admisiones();
         Entity.ExoneracionArchivo = new Archivos();
      }

   }

}
