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
    /// MediosPago object for mapped table MediosPago.
    /// </summary>
    [Table("MediosPago")]
    public partial class MediosPago : BaseEntity
    {

       #region Columnas normales)

       [Column("Descripcion")]
       [DDisplayName("MediosPago.Descripcion")]
       [DRequired("MediosPago.Descripcion")]
       [DStringLength("MediosPago.Descripcion",250)]
       public virtual String Descripcion { get; set; }

       [Column("Codigo")]
       [DDisplayName("MediosPago.Codigo")]
       [DRequired("MediosPago.Codigo")]
       [DStringLength("MediosPago.Codigo",50)]
       public virtual String Codigo { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<MediosPago, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<MediosPago, bool>> expression = null;

        expression = entity => entity.Codigo == this.Codigo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "MediosPago.Codigo" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<MediosPago, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Codigo == this.Codigo)
                               && entity.Codigo == this.Codigo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "MediosPago.Codigo" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Admisiones, bool>> expression0 = entity => entity.MedioPagosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<Notas, bool>> expression1 = entity => entity.MediosPagoId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Notas"), typeof(Notas)));

        Expression<Func<Recaudos, bool>> expression2 = entity => entity.MediosPagoId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Recaudos"), typeof(Recaudos)));

       return rules;
       }

       #endregion
    }
 }
