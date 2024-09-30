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
    /// EmpleadosEspecialidades object for mapped table EmpleadosEspecialidades.
    /// </summary>
    [Table("EmpleadosEspecialidades")]
    public partial class EmpleadosEspecialidades : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("EmpleadosId")]
       [DDisplayName("EmpleadosEspecialidades.EmpleadosId")]
       [DRequired("EmpleadosEspecialidades.EmpleadosId")]
       [DRequiredFK("EmpleadosEspecialidades.EmpleadosId")]
       public virtual Int64 EmpleadosId { get; set; }

       [Column("EspecialidadesId")]
       [DDisplayName("EmpleadosEspecialidades.EspecialidadesId")]
       [DRequired("EmpleadosEspecialidades.EspecialidadesId")]
       [DRequiredFK("EmpleadosEspecialidades.EspecialidadesId")]
       public virtual Int64 EspecialidadesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EmpleadosId")]
       public virtual Empleados Empleados { get; set; }

       [ForeignKey("EspecialidadesId")]
       public virtual Especialidades Especialidades { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<EmpleadosEspecialidades, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EmpleadosEspecialidades, bool>> expression = null;

        expression = entity => entity.EmpleadosId == this.EmpleadosId && entity.EspecialidadesId == this.EspecialidadesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "EmpleadosEspecialidades.EmpleadosId" , "EmpleadosEspecialidades.EspecialidadesId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EmpleadosEspecialidades, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.EmpleadosId == this.EmpleadosId && entity.EspecialidadesId == this.EspecialidadesId)
                               && entity.EmpleadosId == this.EmpleadosId && entity.EspecialidadesId == this.EspecialidadesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "EmpleadosEspecialidades.EmpleadosId" , "EmpleadosEspecialidades.EspecialidadesId" )));

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
