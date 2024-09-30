using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Serialize.Linq.Extensions;
using Dominus.Backend.Data;
using Dominus.Backend.DataBase;

namespace Blazor.Infrastructure.Entities
{
    public partial class EntregaResultados
    {
        [NotMapped]
        public List<long> ListAtencionesResultadoId { get; set; }

        [NotMapped]
        [DDisplayName("EntregaResultados.NombreCompleto")]
        public string NombreCompleto
        {
            get
            {
                return Nombres + " " + Apellidos;
            }
            private set
            {
            }
        }
    }
 }
