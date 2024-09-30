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
    /// EsquemasImpuestos object for mapped table EsquemasImpuestos.
    /// </summary>
    [Table("EsquemasImpuestos")]
    public partial class EsquemasImpuestos : BaseEntity
    {

       #region Columnas normales)

       [Column("Codigo")]
       [DDisplayName("EsquemasImpuestos.Codigo")]
       [DRequired("EsquemasImpuestos.Codigo")]
       [DStringLength("EsquemasImpuestos.Codigo",10)]
       public virtual String Codigo { get; set; }

       [Column("Descripcion")]
       [DDisplayName("EsquemasImpuestos.Descripcion")]
       [DRequired("EsquemasImpuestos.Descripcion")]
       [DStringLength("EsquemasImpuestos.Descripcion",255)]
       public virtual String Descripcion { get; set; }

       [Column("Observacion")]
       [DDisplayName("EsquemasImpuestos.Observacion")]
       [DRequired("EsquemasImpuestos.Observacion")]
       [DStringLength("EsquemasImpuestos.Observacion",255)]
       public virtual String Observacion { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<EsquemasImpuestos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EsquemasImpuestos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EsquemasImpuestos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EmpresasEsquemasImpuestos, bool>> expression0 = entity => entity.EsquemasImpuestosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EmpresasEsquemasImpuestos"), typeof(EmpresasEsquemasImpuestos)));

        Expression<Func<EntidadesEsquemasImpuestos, bool>> expression1 = entity => entity.EsquemasImpuestosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EntidadesEsquemasImpuestos"), typeof(EntidadesEsquemasImpuestos)));

        Expression<Func<Servicios, bool>> expression2 = entity => entity.TiposImpuestosId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Servicios"), typeof(Servicios)));

       return rules;
       }

       #endregion
    }
 }
