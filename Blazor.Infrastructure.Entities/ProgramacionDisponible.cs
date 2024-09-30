using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Serialize.Linq.Extensions;
using Dominus.Backend.Data;
using Dominus.Backend.DataBase;

namespace Blazor.Infrastructure.Entities
{
    /// <summary>
    /// ProgramacionDisponible object for mapped table ProgramacionDisponible.
    /// </summary>
    [Table("ProgramacionDisponible")]
    public partial class ProgramacionDisponible : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("ProgramacionAgendaId")]
       [DDisplayName("ProgramacionDisponible.ProgramacionAgendaId")]
       [DRequired("ProgramacionDisponible.ProgramacionAgendaId")]
       [DRequiredFK("ProgramacionDisponible.ProgramacionAgendaId")]
       public virtual Int64 ProgramacionAgendaId { get; set; }

       [Column("ServiciosId")]
       [DDisplayName("ProgramacionDisponible.ServiciosId")]
       [DRequired("ProgramacionDisponible.ServiciosId")]
       [DRequiredFK("ProgramacionDisponible.ServiciosId")]
       public virtual Int64 ServiciosId { get; set; }

       [Column("ConsultoriosId")]
       [DDisplayName("ProgramacionDisponible.ConsultoriosId")]
       public virtual Int64? ConsultoriosId { get; set; }

       [Column("EmpleadosId")]
       [DDisplayName("ProgramacionDisponible.EmpleadosId")]
       public virtual Int64? EmpleadosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("ConsultoriosId")]
       public virtual Consultorios Consultorios { get; set; }

       [ForeignKey("EmpleadosId")]
       public virtual Empleados Empleados { get; set; }

       [ForeignKey("ProgramacionAgendaId")]
       public virtual ProgramacionAgenda ProgramacionAgenda { get; set; }

       [ForeignKey("ServiciosId")]
       public virtual Servicios Servicios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ProgramacionDisponible, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProgramacionDisponible, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProgramacionDisponible, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
       return rules;
       }

       #endregion
    }
 }
