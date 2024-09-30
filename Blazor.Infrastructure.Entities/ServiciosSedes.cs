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
    /// ServiciosSedes object for mapped table ServiciosSedes.
    /// </summary>
    [Table("ServiciosSedes")]
    public partial class ServiciosSedes : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("ServiciosId")]
       [DDisplayName("ServiciosSedes.ServiciosId")]
       [DRequired("ServiciosSedes.ServiciosId")]
       [DRequiredFK("ServiciosSedes.ServiciosId")]
       public virtual Int64 ServiciosId { get; set; }

       [Column("SedesId")]
       [DDisplayName("ServiciosSedes.SedesId")]
       [DRequired("ServiciosSedes.SedesId")]
       [DRequiredFK("ServiciosSedes.SedesId")]
       public virtual Int64 SedesId { get; set; }

       [Column("EstadosId")]
       [DDisplayName("ServiciosSedes.EstadosId")]
       [DRequired("ServiciosSedes.EstadosId")]
       [DRequiredFK("ServiciosSedes.EstadosId")]
       public virtual Int64 EstadosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EstadosId")]
       public virtual Estados Estados { get; set; }

       [ForeignKey("SedesId")]
       public virtual Sedes Sedes { get; set; }

       [ForeignKey("ServiciosId")]
       public virtual Servicios Servicios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ServiciosSedes, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ServiciosSedes, bool>> expression = null;

        expression = entity => entity.ServiciosId == this.ServiciosId && entity.SedesId == this.SedesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ServiciosSedes.ServiciosId" , "ServiciosSedes.SedesId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ServiciosSedes, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.ServiciosId == this.ServiciosId && entity.SedesId == this.SedesId)
                               && entity.ServiciosId == this.ServiciosId && entity.SedesId == this.SedesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ServiciosSedes.ServiciosId" , "ServiciosSedes.SedesId" )));

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
