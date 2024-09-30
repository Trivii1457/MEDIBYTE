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
    /// TiposSangre object for mapped table TiposSangre.
    /// </summary>
    [Table("TiposSangre")]
    public partial class TiposSangre : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("TiposSangre.Nombre")]
       [DRequired("TiposSangre.Nombre")]
       [DStringLength("TiposSangre.Nombre",2)]
       public virtual String Nombre { get; set; }

       [Column("RH")]
       [DDisplayName("TiposSangre.RH")]
       [DRequired("TiposSangre.RH")]
       [DStringLength("TiposSangre.RH",1)]
       public virtual String RH { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<TiposSangre, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposSangre, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposSangre, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Empleados, bool>> expression0 = entity => entity.TiposSangreId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Empleados"), typeof(Empleados)));

        Expression<Func<Pacientes, bool>> expression1 = entity => entity.TiposSangreId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Pacientes"), typeof(Pacientes)));

       return rules;
       }

       #endregion
    }
 }
