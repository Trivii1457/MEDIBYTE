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
    /// Generos object for mapped table Generos.
    /// </summary>
    [Table("Generos")]
    public partial class Generos : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("Generos.Nombre")]
       [DRequired("Generos.Nombre")]
       [DStringLength("Generos.Nombre",45)]
       public virtual String Nombre { get; set; }

       [Column("Codigo")]
       [DDisplayName("Generos.Codigo")]
       [DRequired("Generos.Codigo")]
       [DStringLength("Generos.Codigo",5)]
       public virtual String Codigo { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Generos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Generos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Generos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Diagnosticos, bool>> expression0 = entity => entity.GenerosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Diagnosticos"), typeof(Diagnosticos)));

        Expression<Func<Empleados, bool>> expression1 = entity => entity.GenerosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Empleados"), typeof(Empleados)));

        Expression<Func<Pacientes, bool>> expression2 = entity => entity.GenerosId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Pacientes"), typeof(Pacientes)));

       return rules;
       }

       #endregion
    }
 }
