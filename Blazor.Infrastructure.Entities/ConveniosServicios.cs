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
    /// ConveniosServicios object for mapped table ConveniosServicios.
    /// </summary>
    [Table("ConveniosServicios")]
    public partial class ConveniosServicios : BaseEntity
    {

       #region Columnas normales)

       [Column("Cantidad")]
       [DDisplayName("ConveniosServicios.Cantidad")]
       public virtual Int16? Cantidad { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("ConveniosId")]
       [DDisplayName("ConveniosServicios.ConveniosId")]
       [DRequired("ConveniosServicios.ConveniosId")]
       [DRequiredFK("ConveniosServicios.ConveniosId")]
       public virtual Int64 ConveniosId { get; set; }

       [Column("ServiciosId")]
       [DDisplayName("ConveniosServicios.ServiciosId")]
       [DRequired("ConveniosServicios.ServiciosId")]
       [DRequiredFK("ConveniosServicios.ServiciosId")]
       public virtual Int64 ServiciosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("ConveniosId")]
       public virtual Convenios Convenios { get; set; }

       [ForeignKey("ServiciosId")]
       public virtual Servicios Servicios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ConveniosServicios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ConveniosServicios, bool>> expression = null;

        expression = entity => entity.ConveniosId == this.ConveniosId && entity.ServiciosId == this.ServiciosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ConveniosServicios.ConveniosId" , "ConveniosServicios.ServiciosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ConveniosServicios, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.ConveniosId == this.ConveniosId && entity.ServiciosId == this.ServiciosId)
                               && entity.ConveniosId == this.ConveniosId && entity.ServiciosId == this.ServiciosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ConveniosServicios.ConveniosId" , "ConveniosServicios.ServiciosId" )));

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
