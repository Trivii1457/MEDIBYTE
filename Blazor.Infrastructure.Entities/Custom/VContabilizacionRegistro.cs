using Dominus.Backend.Data;
using Dominus.Backend.DataBase;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    [Table("VContabilizacionRegistro")]
    public class VContabilizacionRegistro : BaseEntity
    {
        [Column("Documento")]
        public string Documento { get; set; }

        [Column("Registro")]
        public string Registro { get; set; }
    }
}

