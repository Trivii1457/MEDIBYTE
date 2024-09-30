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
    /// FacturasDetalles object for mapped table FacturasDetalles.
    /// </summary>
    [Table("FacturasDetalles")]
    public partial class FacturasDetalles : BaseEntity
    {

       #region Columnas normales)

       [Column("NroLinea")]
       [DDisplayName("FacturasDetalles.NroLinea")]
       [DRequired("FacturasDetalles.NroLinea")]
       public virtual Int32 NroLinea { get; set; }

       [Column("Cantidad")]
       [DDisplayName("FacturasDetalles.Cantidad")]
       [DRequired("FacturasDetalles.Cantidad")]
       public virtual Decimal Cantidad { get; set; }

       [Column("PorcDescuento")]
       [DDisplayName("FacturasDetalles.PorcDescuento")]
       [DRequired("FacturasDetalles.PorcDescuento")]
       public virtual Decimal PorcDescuento { get; set; }

       [Column("PorcImpuesto")]
       [DDisplayName("FacturasDetalles.PorcImpuesto")]
       [DRequired("FacturasDetalles.PorcImpuesto")]
       public virtual Decimal PorcImpuesto { get; set; }

       [Column("ValorServicio")]
       [DDisplayName("FacturasDetalles.ValorServicio")]
       [DRequired("FacturasDetalles.ValorServicio")]
       public virtual Decimal ValorServicio { get; set; }

       [Column("SubTotal")]
       [DDisplayName("FacturasDetalles.SubTotal")]
       [DRequired("FacturasDetalles.SubTotal")]
       public virtual Decimal SubTotal { get; set; }

       [Column("ValorTotal")]
       [DDisplayName("FacturasDetalles.ValorTotal")]
       [DRequired("FacturasDetalles.ValorTotal")]
       public virtual Decimal ValorTotal { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("FacturasId")]
       [DDisplayName("FacturasDetalles.FacturasId")]
       [DRequired("FacturasDetalles.FacturasId")]
       [DRequiredFK("FacturasDetalles.FacturasId")]
       public virtual Int64 FacturasId { get; set; }

       [Column("ServiciosId")]
       [DDisplayName("FacturasDetalles.ServiciosId")]
       [DRequired("FacturasDetalles.ServiciosId")]
       [DRequiredFK("FacturasDetalles.ServiciosId")]
       public virtual Int64 ServiciosId { get; set; }

       [Column("AdmisionesServiciosPrestadosId")]
       [DDisplayName("FacturasDetalles.AdmisionesServiciosPrestadosId")]
       public virtual Int64? AdmisionesServiciosPrestadosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("AdmisionesServiciosPrestadosId")]
       public virtual AdmisionesServiciosPrestados AdmisionesServiciosPrestados { get; set; }

       [ForeignKey("FacturasId")]
       public virtual Facturas Facturas { get; set; }

       [ForeignKey("ServiciosId")]
       public virtual Servicios Servicios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<FacturasDetalles, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<FacturasDetalles, bool>> expression = null;

        expression = entity => entity.FacturasId == this.FacturasId && entity.ServiciosId == this.ServiciosId && entity.NroLinea == this.NroLinea;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "FacturasDetalles.FacturasId" , "FacturasDetalles.ServiciosId" , "FacturasDetalles.NroLinea" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<FacturasDetalles, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.FacturasId == this.FacturasId && entity.ServiciosId == this.ServiciosId && entity.NroLinea == this.NroLinea)
                               && entity.FacturasId == this.FacturasId && entity.ServiciosId == this.ServiciosId && entity.NroLinea == this.NroLinea;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "FacturasDetalles.FacturasId" , "FacturasDetalles.ServiciosId" , "FacturasDetalles.NroLinea" )));

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
