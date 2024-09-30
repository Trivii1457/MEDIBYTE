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
    /// Festivos object for mapped table Festivos.
    /// </summary>
    [Table("Festivos")]
    public partial class Festivos : BaseEntity
    {

       #region Columnas normales)

       [Column("Dia", TypeName = "datetime")]
       [DDisplayName("Festivos.Dia")]
       [DRequired("Festivos.Dia")]
       public virtual DateTime Dia { get; set; }

       [Column("Descripcion")]
       [DDisplayName("Festivos.Descripcion")]
       [DStringLength("Festivos.Descripcion",250)]
       public virtual String Descripcion { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Festivos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Festivos, bool>> expression = null;

        expression = entity => entity.Dia == this.Dia;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Festivos.Dia" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Festivos, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Dia == this.Dia)
                               && entity.Dia == this.Dia;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Festivos.Dia" )));

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
