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
    /// TipoEntidades object for mapped table TipoEntidades.
    /// </summary>
    [Table("TipoEntidades")]
    public partial class TipoEntidades : BaseEntity
    {

       #region Columnas normales)

       [Column("Codigo")]
       [DDisplayName("TipoEntidades.Codigo")]
       [DRequired("TipoEntidades.Codigo")]
       [DStringLength("TipoEntidades.Codigo",5)]
       public virtual String Codigo { get; set; }

       [Column("Nombre")]
       [DDisplayName("TipoEntidades.Nombre")]
       [DRequired("TipoEntidades.Nombre")]
       [DStringLength("TipoEntidades.Nombre",100)]
       public virtual String Nombre { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<TipoEntidades, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TipoEntidades, bool>> expression = null;

        expression = entity => entity.Codigo == this.Codigo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "TipoEntidades.Codigo" )));

        expression = entity => entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "TipoEntidades.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TipoEntidades, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Codigo == this.Codigo)
                               && entity.Codigo == this.Codigo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "TipoEntidades.Codigo" )));

        expression = entity => !(entity.Id == this.Id && entity.Nombre == this.Nombre)
                               && entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "TipoEntidades.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Entidades, bool>> expression0 = entity => entity.TipoEntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Entidades"), typeof(Entidades)));

       return rules;
       }

       #endregion
    }
 }
