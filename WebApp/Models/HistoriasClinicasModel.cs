using Blazor.Infrastructure.Entities;
using System.Collections.Generic;

namespace Blazor.WebApp.Models
{

    public partial class HistoriasClinicasModel
    {
        public HistoriasClinicas Entity { get; set; }
        public List<HCPreguntas> Preguntas { get; set; } = new List<HCPreguntas>();
        public bool EsMismoUsuario { get; set; }
        public HistoriasClinicasModel()
        {
            Entity = new HistoriasClinicas();
        }

    }

}
