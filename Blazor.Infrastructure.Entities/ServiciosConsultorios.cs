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
    /// ServiciosConsultorios object for mapped table ServiciosConsultorios.
    /// </summary>
    [Table("ServiciosConsultorios")]
    public partial class ServiciosConsultorios : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("ServiciosId")]
       [DDisplayName("ServiciosConsultorios.ServiciosId")]
       [DRequired("ServiciosConsultorios.ServiciosId")]
       [DRequiredFK("ServiciosConsultorios.ServiciosId")]
       public virtual Int64 ServiciosId { get; set; }

       [Column("ConsultoriosId")]
       [DDisplayName("ServiciosConsultorios.ConsultoriosId")]
       [DRequired("ServiciosConsultorios.ConsultoriosId")]
       [DRequiredFK("ServiciosConsultorios.ConsultoriosId")]
       public virtual Int64 ConsultoriosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("ConsultoriosId")]
       public virtual Consultorios Consultorios { get; set; }

       [ForeignKey("ServiciosId")]
       public virtual Servicios Servicios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ServiciosConsultorios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ServiciosConsultorios, bool>> expression = null;

        expression = entity => entity.ServiciosId == this.ServiciosId && entity.ConsultoriosId == this.ConsultoriosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ServiciosConsultorios.ServiciosId" , "ServiciosConsultorios.ConsultoriosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ServiciosConsultorios, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.ServiciosId == this.ServiciosId && entity.ConsultoriosId == this.ConsultoriosId)
                               && entity.ServiciosId == this.ServiciosId && entity.ConsultoriosId == this.ConsultoriosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ServiciosConsultorios.ServiciosId" , "ServiciosConsultorios.ConsultoriosId" )));

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
