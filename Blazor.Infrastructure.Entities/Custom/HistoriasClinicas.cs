using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class HistoriasClinicas
    {
        private string especialidad;

        [NotMapped]
        public string Especialidad
        {
            get
            {
                if (HCTipos != null && HCTipos.Especialidades != null)
                    especialidad = HCTipos.Especialidades.Nombre;
                return especialidad;
            }
            private set
            {

            }
        }
    }
 }
