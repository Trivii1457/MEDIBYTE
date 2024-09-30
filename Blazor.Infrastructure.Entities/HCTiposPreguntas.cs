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
    /// HCTiposPreguntas object for mapped table HCTiposPreguntas.
    /// </summary>
    [Table("HCTiposPreguntas")]
    public partial class HCTiposPreguntas : BaseEntity
    {

       #region Columnas normales)

       [Column("Orden")]
       [DDisplayName("HCTiposPreguntas.Orden")]
       [DRequired("HCTiposPreguntas.Orden")]
       public virtual Int16 Orden { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("HCTiposId")]
       [DDisplayName("HCTiposPreguntas.HCTiposId")]
       [DRequired("HCTiposPreguntas.HCTiposId")]
       [DRequiredFK("HCTiposPreguntas.HCTiposId")]
       public virtual Int64 HCTiposId { get; set; }

       [Column("HCPreguntasId")]
       [DDisplayName("HCTiposPreguntas.HCPreguntasId")]
       [DRequired("HCTiposPreguntas.HCPreguntasId")]
       [DRequiredFK("HCTiposPreguntas.HCPreguntasId")]
       public virtual Int64 HCPreguntasId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("HCPreguntasId")]
       public virtual HCPreguntas HCPreguntas { get; set; }

       [ForeignKey("HCTiposId")]
       public virtual HCTipos HCTipos { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<HCTiposPreguntas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HCTiposPreguntas, bool>> expression = null;

        expression = entity => entity.HCTiposId == this.HCTiposId && entity.HCPreguntasId == this.HCPreguntasId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "HCTiposPreguntas.HCTiposId" , "HCTiposPreguntas.HCPreguntasId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HCTiposPreguntas, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.HCTiposId == this.HCTiposId && entity.HCPreguntasId == this.HCPreguntasId)
                               && entity.HCTiposId == this.HCTiposId && entity.HCPreguntasId == this.HCPreguntasId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "HCTiposPreguntas.HCTiposId" , "HCTiposPreguntas.HCPreguntasId" )));

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
