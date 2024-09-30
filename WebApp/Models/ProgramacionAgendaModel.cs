using Blazor.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.WebApp.Models
{

    public partial class ProgramacionAgendaModel
    {
        public ProgramacionAgenda Entity { get; set; }
        public virtual DateTime HoraInicio { get; set; }
        public virtual DateTime HoraFinal { get; set; }
        public string SerializedServiciosConsultorios { get; set; }
        public string SerializedServiciosEmpleados { get; set; }
        public List<DiasSemana> DiasSemana { get; set; } = new List<DiasSemana>();
        public List<int> DiasSemanaSelecionados { get; set; } = new List<int>();
        public ProgramacionAgendaModel()
        {
            DiasSemana.Add(new DiasSemana { Id = 1, Nombre = "Lunes" });
            DiasSemana.Add(new DiasSemana { Id = 2, Nombre = "Martes" });
            DiasSemana.Add(new DiasSemana { Id = 3, Nombre = "Miercoles" });
            DiasSemana.Add(new DiasSemana { Id = 4, Nombre = "Jueves" });
            DiasSemana.Add(new DiasSemana { Id = 5, Nombre = "Viernes" });
            DiasSemana.Add(new DiasSemana { Id = 6, Nombre = "Sabado" });
            DiasSemana.Add(new DiasSemana { Id = 7, Nombre = "Domingo" });
          
            Entity = new ProgramacionAgenda();
            Entity.FechaInicio = DateTime.Now;
            Entity.FechaFinal = DateTime.Now;
        }

    }

    public class DiasSemana
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

}
