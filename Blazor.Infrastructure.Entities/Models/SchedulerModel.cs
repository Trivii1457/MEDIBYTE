using System;
using System.Collections;
using System.Collections.Generic;

namespace Blazor.Infrastructure.Models
{
    public class SchedulerModel
    {
        public DateTime FechaMinima { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        public DateTime FechaMaxima { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0);
        public DateTime FechaInicial { get; set; } = DateTime.Now;
        public int IntervaloCelda { get; set; } = 10;
        public double HoraMinima { get; set; } = 1;
        public double HoraMaxima { get; set; } = 23;

        public IEnumerable Data { get; set; } // La data que recive para pintar el scheduler
        public bool Habilitado { get; set; } = true;

        public readonly IEnumerable<RecursoCitasEstados> RecursosCitaEstados = new[] {
            new RecursoCitasEstados {
                Id = 3,
                Text = "PROGRAMADA",
                Color = "#244cd1"
            },
            new RecursoCitasEstados {
                Id = 4,
                Text = "ADMITIDA",
                Color = "#917106"
            },
            new RecursoCitasEstados {
                Id = 5,
                Text = "ADMITIDA EN ATENCIÓN",
                Color = "#e8d41c"
            },
            new RecursoCitasEstados {
                Id = 6,
                Text = "ATENDIDA",
                Color = "#2e7505"
            },
            new RecursoCitasEstados {
                Id = 8,
                Text = "CANCELADA",
                Color = "#8f2609"
            }
        };

    }

    public class RecursoCitasEstados
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }

    } 
}
