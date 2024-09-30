using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Serialize.Linq.Extensions;
using Dominus.Backend.Data;
using Dominus.Backend.DataBase;

namespace Blazor.Infrastructure.Entities
{

    public partial class ProgramacionCitas
    {
        [NotMapped]
        [DDisplayName("ProgramacionCitas.HoraInicio")]
        public DateTime HoraInicio { get; set; }
        [NotMapped]
        [DDisplayName("ProgramacionCitas.HoraFinal")]
        public DateTime HoraFinal { get; set; }

    }
 }
