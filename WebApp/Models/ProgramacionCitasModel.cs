using Blazor.Infrastructure.Entities;
using System;

namespace Blazor.WebApp.Models
{

    public partial class ProgramacionCitasModel
    {
        public ProgramacionCitas Entity { get; set; }
        public PacientesModel PacientesModel { get; set; }
        
        public ProgramacionCitasModel()
        {
            Entity = new ProgramacionCitas();
            PacientesModel = new PacientesModel();
        }

    }

}
