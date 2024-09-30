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
    /// IntefaceFacturas object for mapped table IntefaceFacturas.
    /// </summary>
    [Table("IntefaceFacturas")]
    public partial class IntefaceFacturas : BaseEntity
    {

       #region Columnas normales)

       [Column("CodigoSede")]
       [DDisplayName("IntefaceFacturas.CodigoSede")]
       [DRequired("IntefaceFacturas.CodigoSede")]
       [DStringLength("IntefaceFacturas.CodigoSede",10)]
       public virtual String CodigoSede { get; set; }

       [Column("FechaFactura", TypeName = "datetime")]
       [DDisplayName("IntefaceFacturas.FechaFactura")]
       [DRequired("IntefaceFacturas.FechaFactura")]
       public virtual DateTime FechaFactura { get; set; }

       [Column("PeriodoInicial", TypeName = "datetime")]
       [DDisplayName("IntefaceFacturas.PeriodoInicial")]
       public virtual DateTime? PeriodoInicial { get; set; }

       [Column("PeriodoFinal", TypeName = "datetime")]
       [DDisplayName("IntefaceFacturas.PeriodoFinal")]
       public virtual DateTime? PeriodoFinal { get; set; }

       [Column("OrdenCompra")]
       [DDisplayName("IntefaceFacturas.OrdenCompra")]
       [DStringLength("IntefaceFacturas.OrdenCompra",20)]
       public virtual String OrdenCompra { get; set; }

       [Column("Referencia")]
       [DDisplayName("IntefaceFacturas.Referencia")]
       [DStringLength("IntefaceFacturas.Referencia",20)]
       public virtual String Referencia { get; set; }

       [Column("Prefijo")]
       [DDisplayName("IntefaceFacturas.Prefijo")]
       [DRequired("IntefaceFacturas.Prefijo")]
       [DStringLength("IntefaceFacturas.Prefijo",10)]
       public virtual String Prefijo { get; set; }

       [Column("NroFactura")]
       [DDisplayName("IntefaceFacturas.NroFactura")]
       [DRequired("IntefaceFacturas.NroFactura")]
       public virtual Int64 NroFactura { get; set; }

       [Column("Cliente")]
       [DDisplayName("IntefaceFacturas.Cliente")]
       [DRequired("IntefaceFacturas.Cliente")]
       [DStringLength("IntefaceFacturas.Cliente",20)]
       public virtual String Cliente { get; set; }

       [Column("FormaPago")]
       [DDisplayName("IntefaceFacturas.FormaPago")]
       [DRequired("IntefaceFacturas.FormaPago")]
       public virtual Int16 FormaPago { get; set; }

       [Column("MedioPago")]
       [DDisplayName("IntefaceFacturas.MedioPago")]
       [DStringLength("IntefaceFacturas.MedioPago",3)]
       public virtual String MedioPago { get; set; }

       [Column("ValorSubtotal")]
       [DDisplayName("IntefaceFacturas.ValorSubtotal")]
       [DRequired("IntefaceFacturas.ValorSubtotal")]
       public virtual Decimal ValorSubtotal { get; set; }

       [Column("ValorDescuentos")]
       [DDisplayName("IntefaceFacturas.ValorDescuentos")]
       [DRequired("IntefaceFacturas.ValorDescuentos")]
       public virtual Decimal ValorDescuentos { get; set; }

       [Column("ValorImpuestos")]
       [DDisplayName("IntefaceFacturas.ValorImpuestos")]
       [DRequired("IntefaceFacturas.ValorImpuestos")]
       public virtual Decimal ValorImpuestos { get; set; }

       [Column("ValorTotal")]
       [DDisplayName("IntefaceFacturas.ValorTotal")]
       [DRequired("IntefaceFacturas.ValorTotal")]
       public virtual Decimal ValorTotal { get; set; }

       [Column("ValorCopagos")]
       [DDisplayName("IntefaceFacturas.ValorCopagos")]
       [DRequired("IntefaceFacturas.ValorCopagos")]
       public virtual Decimal ValorCopagos { get; set; }

       [Column("ValorCuotasModeradoras")]
       [DDisplayName("IntefaceFacturas.ValorCuotasModeradoras")]
       [DRequired("IntefaceFacturas.ValorCuotasModeradoras")]
       public virtual Decimal ValorCuotasModeradoras { get; set; }

       [Column("ValorSaldo")]
       [DDisplayName("IntefaceFacturas.ValorSaldo")]
       [DRequired("IntefaceFacturas.ValorSaldo")]
       public virtual Decimal ValorSaldo { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<IntefaceFacturas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<IntefaceFacturas, bool>> expression = null;

        expression = entity => entity.Prefijo == this.Prefijo && entity.NroFactura == this.NroFactura;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "IntefaceFacturas.Prefijo" , "IntefaceFacturas.NroFactura" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<IntefaceFacturas, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Prefijo == this.Prefijo && entity.NroFactura == this.NroFactura)
                               && entity.Prefijo == this.Prefijo && entity.NroFactura == this.NroFactura;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "IntefaceFacturas.Prefijo" , "IntefaceFacturas.NroFactura" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<InterfaceFacturasDetalles, bool>> expression0 = entity => entity.InterfaceFacturasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","InterfaceFacturasDetalles"), typeof(InterfaceFacturasDetalles)));

       return rules;
       }

       #endregion
    }
 }
