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
    /// TiposCitas object for mapped table TiposCitas.
    /// </summary>
    [Table("TiposCitas")]
    public partial class TiposCitas : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("TiposCitas.Nombre")]
       [DRequired("TiposCitas.Nombre")]
       [DStringLength("TiposCitas.Nombre",45)]
       public virtual String Nombre { get; set; }

       [Column("Codigo")]
       [DDisplayName("TiposCitas.Codigo")]
       [DRequired("TiposCitas.Codigo")]
       [DStringLength("TiposCitas.Codigo",5)]
       public virtual String Codigo { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<TiposCitas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposCitas, bool>> expression = null;

        expression = entity => entity.Codigo == this.Codigo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "TiposCitas.Codigo" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposCitas, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Codigo == this.Codigo)
                               && entity.Codigo == this.Codigo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "TiposCitas.Codigo" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProgramacionCitas, bool>> expression0 = entity => entity.TiposCitasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionCitas"), typeof(ProgramacionCitas)));

       return rules;
       }

       #endregion
    }
 }
