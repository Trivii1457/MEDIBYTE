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
    /// FormasPagos object for mapped table FormasPagos.
    /// </summary>
    [Table("FormasPagos")]
    public partial class FormasPagos : BaseEntity
    {

       #region Columnas normales)

       [Column("Codigo")]
       [DDisplayName("FormasPagos.Codigo")]
       [DRequired("FormasPagos.Codigo")]
       [DStringLength("FormasPagos.Codigo",5)]
       public virtual String Codigo { get; set; }

       [Column("Nombre")]
       [DDisplayName("FormasPagos.Nombre")]
       [DRequired("FormasPagos.Nombre")]
       [DStringLength("FormasPagos.Nombre",150)]
       public virtual String Nombre { get; set; }

       [Column("DiasVencimiento")]
       [DDisplayName("FormasPagos.DiasVencimiento")]
       [DRequired("FormasPagos.DiasVencimiento")]
       public virtual Int16 DiasVencimiento { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<FormasPagos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<FormasPagos, bool>> expression = null;

        expression = entity => entity.Codigo == this.Codigo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "FormasPagos.Codigo" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<FormasPagos, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Codigo == this.Codigo)
                               && entity.Codigo == this.Codigo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "FormasPagos.Codigo" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Admisiones, bool>> expression0 = entity => entity.FormasPagosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<Facturas, bool>> expression1 = entity => entity.FormasPagosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Facturas"), typeof(Facturas)));

        Expression<Func<Notas, bool>> expression2 = entity => entity.FormasPagosId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Notas"), typeof(Notas)));

       return rules;
       }

       #endregion
    }
 }
