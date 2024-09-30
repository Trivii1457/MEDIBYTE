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
    /// HCPreguntas object for mapped table HCPreguntas.
    /// </summary>
    [Table("HCPreguntas")]
    public partial class HCPreguntas : BaseEntity
    {

       #region Columnas normales)

       [Column("Descripcion")]
       [DDisplayName("HCPreguntas.Descripcion")]
       [DRequired("HCPreguntas.Descripcion")]
       [DStringLength("HCPreguntas.Descripcion",255)]
       public virtual String Descripcion { get; set; }

       [Column("TipoPreguntaId")]
       [DDisplayName("HCPreguntas.TipoPreguntaId")]
       [DRequired("HCPreguntas.TipoPreguntaId")]
       public virtual Int16 TipoPreguntaId { get; set; }

       [Column("EsEdicionEnPopup")]
       [DDisplayName("HCPreguntas.EsEdicionEnPopup")]
       [DRequired("HCPreguntas.EsEdicionEnPopup")]
       public virtual Boolean EsEdicionEnPopup { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<HCPreguntas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HCPreguntas, bool>> expression = null;

        expression = entity => entity.Descripcion == this.Descripcion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "HCPreguntas.Descripcion" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HCPreguntas, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Descripcion == this.Descripcion)
                               && entity.Descripcion == this.Descripcion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "HCPreguntas.Descripcion" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HCRespuestas, bool>> expression0 = entity => entity.HCPreguntaId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HCRespuestas"), typeof(HCRespuestas)));

        Expression<Func<HCTiposPreguntas, bool>> expression1 = entity => entity.HCPreguntasId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HCTiposPreguntas"), typeof(HCTiposPreguntas)));

       return rules;
       }

       #endregion
    }
 }
