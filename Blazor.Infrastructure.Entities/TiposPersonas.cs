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
    /// TiposPersonas object for mapped table TiposPersonas.
    /// </summary>
    [Table("TiposPersonas")]
    public partial class TiposPersonas : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("TiposPersonas.Nombre")]
       [DRequired("TiposPersonas.Nombre")]
       [DStringLength("TiposPersonas.Nombre",45)]
       public virtual String Nombre { get; set; }

       [Column("Codigo")]
       [DDisplayName("TiposPersonas.Codigo")]
       [DRequired("TiposPersonas.Codigo")]
       [DStringLength("TiposPersonas.Codigo",10)]
       public virtual String Codigo { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<TiposPersonas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposPersonas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposPersonas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Empresas, bool>> expression0 = entity => entity.TiposPersonasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Empresas"), typeof(Empresas)));

        Expression<Func<Entidades, bool>> expression1 = entity => entity.TiposPersonasId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Entidades"), typeof(Entidades)));

       return rules;
       }

       #endregion
    }
 }
