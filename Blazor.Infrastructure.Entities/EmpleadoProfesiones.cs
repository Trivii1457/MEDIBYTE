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
    /// EmpleadoProfesiones object for mapped table EmpleadoProfesiones.
    /// </summary>
    [Table("EmpleadoProfesiones")]
    public partial class EmpleadoProfesiones : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("EmpleadosId")]
       [DDisplayName("EmpleadoProfesiones.EmpleadosId")]
       [DRequired("EmpleadoProfesiones.EmpleadosId")]
       [DRequiredFK("EmpleadoProfesiones.EmpleadosId")]
       public virtual Int64 EmpleadosId { get; set; }

       [Column("ProfesionesId")]
       [DDisplayName("EmpleadoProfesiones.ProfesionesId")]
       [DRequired("EmpleadoProfesiones.ProfesionesId")]
       [DRequiredFK("EmpleadoProfesiones.ProfesionesId")]
       public virtual Int64 ProfesionesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EmpleadosId")]
       public virtual Empleados Empleados { get; set; }

       [ForeignKey("ProfesionesId")]
       public virtual Profesiones Profesiones { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<EmpleadoProfesiones, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EmpleadoProfesiones, bool>> expression = null;

        expression = entity => entity.EmpleadosId == this.EmpleadosId && entity.ProfesionesId == this.ProfesionesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "EmpleadoProfesiones.EmpleadosId" , "EmpleadoProfesiones.ProfesionesId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EmpleadoProfesiones, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.EmpleadosId == this.EmpleadosId && entity.ProfesionesId == this.ProfesionesId)
                               && entity.EmpleadosId == this.EmpleadosId && entity.ProfesionesId == this.ProfesionesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "EmpleadoProfesiones.EmpleadosId" , "EmpleadoProfesiones.ProfesionesId" )));

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
