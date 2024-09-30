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
    /// CategoriasIngresos object for mapped table CategoriasIngresos.
    /// </summary>
    [Table("CategoriasIngresos")]
    public partial class CategoriasIngresos : BaseEntity
    {

       #region Columnas normales)

       [Column("Codigo")]
       [DDisplayName("CategoriasIngresos.Codigo")]
       [DRequired("CategoriasIngresos.Codigo")]
       [DStringLength("CategoriasIngresos.Codigo",5)]
       public virtual String Codigo { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<CategoriasIngresos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CategoriasIngresos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CategoriasIngresos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CategoriasIngresosDetalles, bool>> expression0 = entity => entity.CategoriasIngresosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","CategoriasIngresosDetalles"), typeof(CategoriasIngresosDetalles)));

        Expression<Func<Pacientes, bool>> expression1 = entity => entity.CategoriasIngresosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Pacientes"), typeof(Pacientes)));

       return rules;
       }

       #endregion
    }
 }
