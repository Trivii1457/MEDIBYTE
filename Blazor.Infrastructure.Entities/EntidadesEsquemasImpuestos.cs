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
    /// EntidadesEsquemasImpuestos object for mapped table EntidadesEsquemasImpuestos.
    /// </summary>
    [Table("EntidadesEsquemasImpuestos")]
    public partial class EntidadesEsquemasImpuestos : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("EntidadesId")]
       [DDisplayName("EntidadesEsquemasImpuestos.EntidadesId")]
       [DRequired("EntidadesEsquemasImpuestos.EntidadesId")]
       [DRequiredFK("EntidadesEsquemasImpuestos.EntidadesId")]
       public virtual Int64 EntidadesId { get; set; }

       [Column("EsquemasImpuestosId")]
       [DDisplayName("EntidadesEsquemasImpuestos.EsquemasImpuestosId")]
       [DRequired("EntidadesEsquemasImpuestos.EsquemasImpuestosId")]
       [DRequiredFK("EntidadesEsquemasImpuestos.EsquemasImpuestosId")]
       public virtual Int64 EsquemasImpuestosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EntidadesId")]
       public virtual Entidades Entidades { get; set; }

       [ForeignKey("EsquemasImpuestosId")]
       public virtual EsquemasImpuestos EsquemasImpuestos { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<EntidadesEsquemasImpuestos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EntidadesEsquemasImpuestos, bool>> expression = null;

        expression = entity => entity.EntidadesId == this.EntidadesId && entity.EsquemasImpuestosId == this.EsquemasImpuestosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "EntidadesEsquemasImpuestos.EntidadesId" , "EntidadesEsquemasImpuestos.EsquemasImpuestosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EntidadesEsquemasImpuestos, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.EntidadesId == this.EntidadesId && entity.EsquemasImpuestosId == this.EsquemasImpuestosId)
                               && entity.EntidadesId == this.EntidadesId && entity.EsquemasImpuestosId == this.EsquemasImpuestosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "EntidadesEsquemasImpuestos.EntidadesId" , "EntidadesEsquemasImpuestos.EsquemasImpuestosId" )));

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
