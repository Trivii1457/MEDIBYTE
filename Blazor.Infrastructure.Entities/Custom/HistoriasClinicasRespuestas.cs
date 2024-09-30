using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{
    public partial class HistoriasClinicasRespuestas
    {

        [NotMapped]
        public string Grupo
        {
            get
            {
                string dat = "";
                if (this.HCRespuestas != null && this.HCRespuestas.HCPregunta != null)
                    dat = this.HCRespuestas.HCPregunta.Descripcion;
                return dat;
            }

            set { }
        }
    }
}