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
    /// CausasExternas object for mapped table CausasExternas.
    /// </summary>
    [Table("CausasExternas")]
    public partial class CausasExternas : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("CausasExternas.Nombre")]
       [DRequired("CausasExternas.Nombre")]
       [DStringLength("CausasExternas.Nombre",255)]
       public virtual String Nombre { get; set; }

       [Column("CodigoRips")]
       [DDisplayName("CausasExternas.CodigoRips")]
       [DRequired("CausasExternas.CodigoRips")]
       [DStringLength("CausasExternas.CodigoRips",2)]
       public virtual String CodigoRips { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<CausasExternas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CausasExternas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CausasExternas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Atenciones, bool>> expression0 = entity => entity.CausasExternasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Atenciones"), typeof(Atenciones)));

       return rules;
       }

       #endregion
    }
 }
