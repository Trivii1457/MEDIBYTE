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
    /// NotasDetalles object for mapped table NotasDetalles.
    /// </summary>
    [Table("NotasDetalles")]
    public partial class NotasDetalles : BaseEntity
    {

       #region Columnas normales)

       [Column("NroLinea")]
       [DDisplayName("NotasDetalles.NroLinea")]
       [DRequired("NotasDetalles.NroLinea")]
       public virtual Int32 NroLinea { get; set; }

       [Column("Cantidad")]
       [DDisplayName("NotasDetalles.Cantidad")]
       [DRequired("NotasDetalles.Cantidad")]
       public virtual Decimal Cantidad { get; set; }

       [Column("PorcDescuento")]
       [DDisplayName("NotasDetalles.PorcDescuento")]
       [DRequired("NotasDetalles.PorcDescuento")]
       public virtual Decimal PorcDescuento { get; set; }

       [Column("PorcImpuesto")]
       [DDisplayName("NotasDetalles.PorcImpuesto")]
       [DRequired("NotasDetalles.PorcImpuesto")]
       public virtual Decimal PorcImpuesto { get; set; }

       [Column("ValorServicio")]
       [DDisplayName("NotasDetalles.ValorServicio")]
       [DRequired("NotasDetalles.ValorServicio")]
       public virtual Decimal ValorServicio { get; set; }

       [Column("SubTotal")]
       [DDisplayName("NotasDetalles.SubTotal")]
       [DRequired("NotasDetalles.SubTotal")]
       public virtual Decimal SubTotal { get; set; }

       [Column("ValorTotal")]
       [DDisplayName("NotasDetalles.ValorTotal")]
       [DRequired("NotasDetalles.ValorTotal")]
       public virtual Decimal ValorTotal { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("NotasId")]
       [DDisplayName("NotasDetalles.NotasId")]
       [DRequired("NotasDetalles.NotasId")]
       [DRequiredFK("NotasDetalles.NotasId")]
       public virtual Int64 NotasId { get; set; }

       [Column("ServiciosId")]
       [DDisplayName("NotasDetalles.ServiciosId")]
       [DRequired("NotasDetalles.ServiciosId")]
       [DRequiredFK("NotasDetalles.ServiciosId")]
       public virtual Int64 ServiciosId { get; set; }

       [Column("PacientesId")]
       [DDisplayName("NotasDetalles.PacientesId")]
       public virtual Int64? PacientesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("NotasId")]
       public virtual Notas Notas { get; set; }

       [ForeignKey("PacientesId")]
       public virtual Pacientes Pacientes { get; set; }

       [ForeignKey("ServiciosId")]
       public virtual Servicios Servicios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<NotasDetalles, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<NotasDetalles, bool>> expression = null;

        expression = entity => entity.NotasId == this.NotasId && entity.ServiciosId == this.ServiciosId && entity.NroLinea == this.NroLinea;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "NotasDetalles.NotasId" , "NotasDetalles.ServiciosId" , "NotasDetalles.NroLinea" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<NotasDetalles, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.NotasId == this.NotasId && entity.ServiciosId == this.ServiciosId && entity.NroLinea == this.NroLinea)
                               && entity.NotasId == this.NotasId && entity.ServiciosId == this.ServiciosId && entity.NroLinea == this.NroLinea;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "NotasDetalles.NotasId" , "NotasDetalles.ServiciosId" , "NotasDetalles.NroLinea" )));

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
