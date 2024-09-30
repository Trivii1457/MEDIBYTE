using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Serialize.Linq.Extensions;
using Dominus.Backend.Data;
using Dominus.Backend.DataBase;

namespace Blazor.Infrastructure.Entities
{

    public partial class ProgramacionAgenda : BaseEntity
    {
        [NotMapped]
        public List<ServiciosConsultorios> ServiciosConsultorios { get; set; }
        
        [NotMapped]
        public List<ServiciosEmpleados> ServiciosEmpleados { get; set; }
    }
 }
