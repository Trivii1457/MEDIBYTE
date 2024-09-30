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
    /// InterfaceFacturasDetalles object for mapped table InterfaceFacturasDetalles.
    /// </summary>
    [Table("InterfaceFacturasDetalles")]
    public partial class InterfaceFacturasDetalles : BaseEntity
    {

       #region Columnas normales)

       [Column("NroLinea")]
       [DDisplayName("InterfaceFacturasDetalles.NroLinea")]
       [DRequired("InterfaceFacturasDetalles.NroLinea")]
       public virtual Int32 NroLinea { get; set; }

       [Column("CodigoServicio")]
       [DDisplayName("InterfaceFacturasDetalles.CodigoServicio")]
       [DRequired("InterfaceFacturasDetalles.CodigoServicio")]
       [DStringLength("InterfaceFacturasDetalles.CodigoServicio",50)]
       public virtual String CodigoServicio { get; set; }

       [Column("NombreServicio")]
       [DDisplayName("InterfaceFacturasDetalles.NombreServicio")]
       [DRequired("InterfaceFacturasDetalles.NombreServicio")]
       [DStringLength("InterfaceFacturasDetalles.NombreServicio",255)]
       public virtual String NombreServicio { get; set; }

       [Column("ValorUnitario")]
       [DDisplayName("InterfaceFacturasDetalles.ValorUnitario")]
       [DRequired("InterfaceFacturasDetalles.ValorUnitario")]
       public virtual Decimal ValorUnitario { get; set; }

       [Column("Cantidad")]
       [DDisplayName("InterfaceFacturasDetalles.Cantidad")]
       [DRequired("InterfaceFacturasDetalles.Cantidad")]
       public virtual Decimal Cantidad { get; set; }

       [Column("PorcentajeDescuento")]
       [DDisplayName("InterfaceFacturasDetalles.PorcentajeDescuento")]
       [DRequired("InterfaceFacturasDetalles.PorcentajeDescuento")]
       public virtual Decimal PorcentajeDescuento { get; set; }

       [Column("PorcentajeImpuesto")]
       [DDisplayName("InterfaceFacturasDetalles.PorcentajeImpuesto")]
       [DRequired("InterfaceFacturasDetalles.PorcentajeImpuesto")]
       public virtual Decimal PorcentajeImpuesto { get; set; }

       [Column("CodigoImpuesto")]
       [DDisplayName("InterfaceFacturasDetalles.CodigoImpuesto")]
       [DStringLength("InterfaceFacturasDetalles.CodigoImpuesto",2)]
       public virtual String CodigoImpuesto { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("InterfaceFacturasId")]
       [DDisplayName("InterfaceFacturasDetalles.InterfaceFacturasId")]
       [DRequired("InterfaceFacturasDetalles.InterfaceFacturasId")]
       [DRequiredFK("InterfaceFacturasDetalles.InterfaceFacturasId")]
       public virtual Int64 InterfaceFacturasId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("InterfaceFacturasId")]
       public virtual IntefaceFacturas InterfaceFacturas { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<InterfaceFacturasDetalles, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<InterfaceFacturasDetalles, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<InterfaceFacturasDetalles, bool>> expression = null;

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
