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
    /// EmpresasEsquemasImpuestos object for mapped table EmpresasEsquemasImpuestos.
    /// </summary>
    [Table("EmpresasEsquemasImpuestos")]
    public partial class EmpresasEsquemasImpuestos : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("EmpresasId")]
       [DDisplayName("EmpresasEsquemasImpuestos.EmpresasId")]
       [DRequired("EmpresasEsquemasImpuestos.EmpresasId")]
       [DRequiredFK("EmpresasEsquemasImpuestos.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("EsquemasImpuestosId")]
       [DDisplayName("EmpresasEsquemasImpuestos.EsquemasImpuestosId")]
       [DRequired("EmpresasEsquemasImpuestos.EsquemasImpuestosId")]
       [DRequiredFK("EmpresasEsquemasImpuestos.EsquemasImpuestosId")]
       public virtual Int64 EsquemasImpuestosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("EsquemasImpuestosId")]
       public virtual EsquemasImpuestos EsquemasImpuestos { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<EmpresasEsquemasImpuestos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EmpresasEsquemasImpuestos, bool>> expression = null;

        expression = entity => entity.EmpresasId == this.EmpresasId && entity.EsquemasImpuestosId == this.EsquemasImpuestosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "EmpresasEsquemasImpuestos.EmpresasId" , "EmpresasEsquemasImpuestos.EsquemasImpuestosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EmpresasEsquemasImpuestos, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.EmpresasId == this.EmpresasId && entity.EsquemasImpuestosId == this.EsquemasImpuestosId)
                               && entity.EmpresasId == this.EmpresasId && entity.EsquemasImpuestosId == this.EsquemasImpuestosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "EmpresasEsquemasImpuestos.EmpresasId" , "EmpresasEsquemasImpuestos.EsquemasImpuestosId" )));

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
