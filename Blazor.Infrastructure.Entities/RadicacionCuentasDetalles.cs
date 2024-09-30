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
    /// RadicacionCuentasDetalles object for mapped table RadicacionCuentasDetalles.
    /// </summary>
    [Table("RadicacionCuentasDetalles")]
    public partial class RadicacionCuentasDetalles : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("RadicacionCuentasId")]
       [DDisplayName("RadicacionCuentasDetalles.RadicacionCuentasId")]
       [DRequired("RadicacionCuentasDetalles.RadicacionCuentasId")]
       [DRequiredFK("RadicacionCuentasDetalles.RadicacionCuentasId")]
       public virtual Int64 RadicacionCuentasId { get; set; }

       [Column("FacurasId")]
       [DDisplayName("RadicacionCuentasDetalles.FacurasId")]
       [DRequired("RadicacionCuentasDetalles.FacurasId")]
       [DRequiredFK("RadicacionCuentasDetalles.FacurasId")]
       public virtual Int64 FacurasId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("FacurasId")]
       public virtual Facturas Facuras { get; set; }

       [ForeignKey("RadicacionCuentasId")]
       public virtual RadicacionCuentas RadicacionCuentas { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<RadicacionCuentasDetalles, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<RadicacionCuentasDetalles, bool>> expression = null;

        expression = entity => entity.RadicacionCuentasId == this.RadicacionCuentasId && entity.FacurasId == this.FacurasId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "RadicacionCuentasDetalles.RadicacionCuentasId" , "RadicacionCuentasDetalles.FacurasId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<RadicacionCuentasDetalles, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.RadicacionCuentasId == this.RadicacionCuentasId && entity.FacurasId == this.FacurasId)
                               && entity.RadicacionCuentasId == this.RadicacionCuentasId && entity.FacurasId == this.FacurasId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "RadicacionCuentasDetalles.RadicacionCuentasId" , "RadicacionCuentasDetalles.FacurasId" )));

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
