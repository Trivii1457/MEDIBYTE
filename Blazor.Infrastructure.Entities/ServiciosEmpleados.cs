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
    /// ServiciosEmpleados object for mapped table ServiciosEmpleados.
    /// </summary>
    [Table("ServiciosEmpleados")]
    public partial class ServiciosEmpleados : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("ServiciosId")]
       [DDisplayName("ServiciosEmpleados.ServiciosId")]
       [DRequired("ServiciosEmpleados.ServiciosId")]
       [DRequiredFK("ServiciosEmpleados.ServiciosId")]
       public virtual Int64 ServiciosId { get; set; }

       [Column("EmpleadosId")]
       [DDisplayName("ServiciosEmpleados.EmpleadosId")]
       [DRequired("ServiciosEmpleados.EmpleadosId")]
       [DRequiredFK("ServiciosEmpleados.EmpleadosId")]
       public virtual Int64 EmpleadosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EmpleadosId")]
       public virtual Empleados Empleados { get; set; }

       [ForeignKey("ServiciosId")]
       public virtual Servicios Servicios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ServiciosEmpleados, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ServiciosEmpleados, bool>> expression = null;

        expression = entity => entity.ServiciosId == this.ServiciosId && entity.EmpleadosId == this.EmpleadosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ServiciosEmpleados.ServiciosId" , "ServiciosEmpleados.EmpleadosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ServiciosEmpleados, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.ServiciosId == this.ServiciosId && entity.EmpleadosId == this.EmpleadosId)
                               && entity.ServiciosId == this.ServiciosId && entity.EmpleadosId == this.EmpleadosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ServiciosEmpleados.ServiciosId" , "ServiciosEmpleados.EmpleadosId" )));

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
