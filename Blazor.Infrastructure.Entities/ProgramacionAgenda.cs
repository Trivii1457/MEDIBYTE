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
    /// ProgramacionAgenda object for mapped table ProgramacionAgenda.
    /// </summary>
    [Table("ProgramacionAgenda")]
    public partial class ProgramacionAgenda : BaseEntity
    {

       #region Columnas normales)

       [Column("Consecutivo")]
       [DDisplayName("ProgramacionAgenda.Consecutivo")]
       [DRequired("ProgramacionAgenda.Consecutivo")]
       public virtual Int32 Consecutivo { get; set; }

       [Column("FechaInicio", TypeName = "datetime")]
       [DDisplayName("ProgramacionAgenda.FechaInicio")]
       [DRequired("ProgramacionAgenda.FechaInicio")]
       public virtual DateTime FechaInicio { get; set; }

       [Column("FechaFinal", TypeName = "datetime")]
       [DDisplayName("ProgramacionAgenda.FechaFinal")]
       [DRequired("ProgramacionAgenda.FechaFinal")]
       public virtual DateTime FechaFinal { get; set; }

       [Column("Observaciones")]
       [DDisplayName("ProgramacionAgenda.Observaciones")]
       [DStringLength("ProgramacionAgenda.Observaciones",1000)]
       public virtual String Observaciones { get; set; }

       [Column("Lunes")]
       [DDisplayName("ProgramacionAgenda.Lunes")]
       public virtual Boolean Lunes { get; set; }

       [Column("Martes")]
       [DDisplayName("ProgramacionAgenda.Martes")]
       public virtual Boolean Martes { get; set; }

       [Column("Miercoles")]
       [DDisplayName("ProgramacionAgenda.Miercoles")]
       public virtual Boolean Miercoles { get; set; }

       [Column("Jueves")]
       [DDisplayName("ProgramacionAgenda.Jueves")]
       public virtual Boolean Jueves { get; set; }

       [Column("Viernes")]
       [DDisplayName("ProgramacionAgenda.Viernes")]
       public virtual Boolean Viernes { get; set; }

       [Column("Sabado")]
       [DDisplayName("ProgramacionAgenda.Sabado")]
       public virtual Boolean Sabado { get; set; }

       [Column("Domingo")]
       [DDisplayName("ProgramacionAgenda.Domingo")]
       public virtual Boolean Domingo { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ProgramacionAgenda, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProgramacionAgenda, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProgramacionAgenda, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProgramacionDisponible, bool>> expression0 = entity => entity.ProgramacionAgendaId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionDisponible"), typeof(ProgramacionDisponible)));

       return rules;
       }

       #endregion
    }
 }
