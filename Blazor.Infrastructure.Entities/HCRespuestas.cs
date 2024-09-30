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
    /// HCRespuestas object for mapped table HCRespuestas.
    /// </summary>
    [Table("HCRespuestas")]
    public partial class HCRespuestas : BaseEntity
    {

       #region Columnas normales)

       [Column("TipoControlId")]
       [DDisplayName("HCRespuestas.TipoControlId")]
       [DRequired("HCRespuestas.TipoControlId")]
       public virtual Int16 TipoControlId { get; set; }

       [Column("Texto")]
       [DDisplayName("HCRespuestas.Texto")]
       [DRequired("HCRespuestas.Texto")]
       [DStringLength("HCRespuestas.Texto",255)]
       public virtual String Texto { get; set; }

       [Column("TextoFinal")]
       [DDisplayName("HCRespuestas.TextoFinal")]
       [DStringLength("HCRespuestas.TextoFinal",255)]
       public virtual String TextoFinal { get; set; }

       [Column("Orden")]
       [DDisplayName("HCRespuestas.Orden")]
       [DRequired("HCRespuestas.Orden")]
       public virtual Int16 Orden { get; set; }

       [Column("ValoresLista")]
       [DDisplayName("HCRespuestas.ValoresLista")]
       [DStringLength("HCRespuestas.ValoresLista",1024)]
       public virtual String ValoresLista { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("HCPreguntaId")]
       [DDisplayName("HCRespuestas.HCPreguntaId")]
       [DRequired("HCRespuestas.HCPreguntaId")]
       [DRequiredFK("HCRespuestas.HCPreguntaId")]
       public virtual Int64 HCPreguntaId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("HCPreguntaId")]
       public virtual HCPreguntas HCPregunta { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<HCRespuestas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HCRespuestas, bool>> expression = null;

        expression = entity => entity.HCPreguntaId == this.HCPreguntaId && entity.Texto == this.Texto;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "HCRespuestas.HCPreguntaId" , "HCRespuestas.Texto" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HCRespuestas, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.HCPreguntaId == this.HCPreguntaId && entity.Texto == this.Texto)
                               && entity.HCPreguntaId == this.HCPreguntaId && entity.Texto == this.Texto;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "HCRespuestas.HCPreguntaId" , "HCRespuestas.Texto" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HistoriasClinicasRespuestas, bool>> expression0 = entity => entity.HCRespuestasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicasRespuestas"), typeof(HistoriasClinicasRespuestas)));

       return rules;
       }

       #endregion
    }
 }
