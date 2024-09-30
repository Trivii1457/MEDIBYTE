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
    /// RecaudosDetalles object for mapped table RecaudosDetalles.
    /// </summary>
    [Table("RecaudosDetalles")]
    public partial class RecaudosDetalles : BaseEntity
    {

       #region Columnas normales)

       [Column("ValorAplicado")]
       [DDisplayName("RecaudosDetalles.ValorAplicado")]
       [DRequired("RecaudosDetalles.ValorAplicado")]
       public virtual Decimal ValorAplicado { get; set; }

       [Column("ValorRetencion")]
       [DDisplayName("RecaudosDetalles.ValorRetencion")]
       [DRequired("RecaudosDetalles.ValorRetencion")]
       public virtual Decimal ValorRetencion { get; set; }

       [Column("ValorReteIca")]
       [DDisplayName("RecaudosDetalles.ValorReteIca")]
       [DRequired("RecaudosDetalles.ValorReteIca")]
       public virtual Decimal ValorReteIca { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("RecaudosId")]
       [DDisplayName("RecaudosDetalles.RecaudosId")]
       [DRequired("RecaudosDetalles.RecaudosId")]
       [DRequiredFK("RecaudosDetalles.RecaudosId")]
       public virtual Int64 RecaudosId { get; set; }

       [Column("FacturasId")]
       [DDisplayName("RecaudosDetalles.FacturasId")]
       [DRequired("RecaudosDetalles.FacturasId")]
       [DRequiredFK("RecaudosDetalles.FacturasId")]
       public virtual Int64 FacturasId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("FacturasId")]
       public virtual Facturas Facturas { get; set; }

       [ForeignKey("RecaudosId")]
       public virtual Recaudos Recaudos { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<RecaudosDetalles, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<RecaudosDetalles, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<RecaudosDetalles, bool>> expression = null;

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
