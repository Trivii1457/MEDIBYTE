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
    /// BasesRetencion object for mapped table BasesRetencion.
    /// </summary>
    [Table("BasesRetencion")]
    public partial class BasesRetencion : BaseEntity
    {

       #region Columnas normales)

       [Column("Concepto")]
       [DDisplayName("BasesRetencion.Concepto")]
       [DRequired("BasesRetencion.Concepto")]
       [DStringLength("BasesRetencion.Concepto",255)]
       public virtual String Concepto { get; set; }

       [Column("BaseUVT")]
       [DDisplayName("BasesRetencion.BaseUVT")]
       [DRequired("BasesRetencion.BaseUVT")]
       public virtual Decimal BaseUVT { get; set; }

       [Column("BasePesos")]
       [DDisplayName("BasesRetencion.BasePesos")]
       [DRequired("BasesRetencion.BasePesos")]
       public virtual Decimal BasePesos { get; set; }

       [Column("Tarifa")]
       [DDisplayName("BasesRetencion.Tarifa")]
       [DRequired("BasesRetencion.Tarifa")]
       public virtual Decimal Tarifa { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<BasesRetencion, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<BasesRetencion, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<BasesRetencion, bool>> expression = null;

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
