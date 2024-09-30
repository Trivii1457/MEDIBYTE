using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Dominus.Backend.Data;
using Dominus.Backend.DataBase;

namespace Blazor.Infrastructure.Entities
{
    public partial class Atenciones
    {
    }
    public partial class Atenciones_Personas
    {
        public Atenciones Atenciones { get; set; }
    }
}
