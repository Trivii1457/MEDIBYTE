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
    /// Paises object for mapped table Paises.
    /// </summary>
    [Table("Paises")]
    public partial class Paises : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("Paises.Nombre")]
       [DRequired("Paises.Nombre")]
       [DStringLength("Paises.Nombre",150)]
       public virtual String Nombre { get; set; }

       [Column("Codigo")]
       [DDisplayName("Paises.Codigo")]
       [DRequired("Paises.Codigo")]
       [DStringLength("Paises.Codigo",5)]
       public virtual String Codigo { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Paises, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Paises, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Paises, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Departamentos, bool>> expression0 = entity => entity.PaisesId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Departamentos"), typeof(Departamentos)));

       return rules;
       }

       #endregion
    }
 }
