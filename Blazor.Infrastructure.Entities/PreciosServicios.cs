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
    /// PreciosServicios object for mapped table PreciosServicios.
    /// </summary>
    [Table("PreciosServicios")]
    public partial class PreciosServicios : BaseEntity
    {

       #region Columnas normales)

       [Column("Precio")]
       [DDisplayName("PreciosServicios.Precio")]
       [DRequired("PreciosServicios.Precio")]
       public virtual Decimal Precio { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("ServiciosId")]
       [DDisplayName("PreciosServicios.ServiciosId")]
       [DRequired("PreciosServicios.ServiciosId")]
       [DRequiredFK("PreciosServicios.ServiciosId")]
       public virtual Int64 ServiciosId { get; set; }

       [Column("ListaPreciosId")]
       [DDisplayName("PreciosServicios.ListaPreciosId")]
       [DRequired("PreciosServicios.ListaPreciosId")]
       [DRequiredFK("PreciosServicios.ListaPreciosId")]
       public virtual Int64 ListaPreciosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("ListaPreciosId")]
       public virtual ListaPrecios ListaPrecios { get; set; }

       [ForeignKey("ServiciosId")]
       public virtual Servicios Servicios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<PreciosServicios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<PreciosServicios, bool>> expression = null;

        expression = entity => entity.ListaPreciosId == this.ListaPreciosId && entity.ServiciosId == this.ServiciosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "PreciosServicios.ListaPreciosId" , "PreciosServicios.ServiciosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<PreciosServicios, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.ListaPreciosId == this.ListaPreciosId && entity.ServiciosId == this.ServiciosId)
                               && entity.ListaPreciosId == this.ListaPreciosId && entity.ServiciosId == this.ServiciosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "PreciosServicios.ListaPreciosId" , "PreciosServicios.ServiciosId" )));

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
