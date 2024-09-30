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
    /// Parentescos object for mapped table Parentescos.
    /// </summary>
    [Table("Parentescos")]
    public partial class Parentescos : BaseEntity
    {

       #region Columnas normales)

       [Column("Descripcion")]
       [DDisplayName("Parentescos.Descripcion")]
       [DRequired("Parentescos.Descripcion")]
       [DStringLength("Parentescos.Descripcion",255)]
       public virtual String Descripcion { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Parentescos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Parentescos, bool>> expression = null;

        expression = entity => entity.Descripcion == this.Descripcion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Parentescos.Descripcion" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Parentescos, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Descripcion == this.Descripcion)
                               && entity.Descripcion == this.Descripcion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Parentescos.Descripcion" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EntregaResultados, bool>> expression0 = entity => entity.ParentescosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EntregaResultados"), typeof(EntregaResultados)));

       return rules;
       }

       #endregion
    }
 }
