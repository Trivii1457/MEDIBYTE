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
    /// CausalesGlosas object for mapped table CausalesGlosas.
    /// </summary>
    [Table("CausalesGlosas")]
    public partial class CausalesGlosas : BaseEntity
    {

       #region Columnas normales)

       [Column("Descripcion")]
       [DDisplayName("CausalesGlosas.Descripcion")]
       [DRequired("CausalesGlosas.Descripcion")]
       [DStringLength("CausalesGlosas.Descripcion",50)]
       public virtual String Descripcion { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<CausalesGlosas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CausalesGlosas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CausalesGlosas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Glosas, bool>> expression0 = entity => entity.CausalesGlosasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Glosas"), typeof(Glosas)));

       return rules;
       }

       #endregion
    }
 }
