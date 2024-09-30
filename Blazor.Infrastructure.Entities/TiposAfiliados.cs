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
    /// TiposAfiliados object for mapped table TiposAfiliados.
    /// </summary>
    [Table("TiposAfiliados")]
    public partial class TiposAfiliados : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("TiposAfiliados.Nombre")]
       [DRequired("TiposAfiliados.Nombre")]
       [DStringLength("TiposAfiliados.Nombre",100)]
       public virtual String Nombre { get; set; }

       [Column("Codigo")]
       [DDisplayName("TiposAfiliados.Codigo")]
       [DRequired("TiposAfiliados.Codigo")]
       [DStringLength("TiposAfiliados.Codigo",5)]
       public virtual String Codigo { get; set; }

       [Column("Tipo")]
       [DDisplayName("TiposAfiliados.Tipo")]
       public virtual Int32? Tipo { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<TiposAfiliados, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposAfiliados, bool>> expression = null;

        expression = entity => entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "TiposAfiliados.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposAfiliados, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Nombre == this.Nombre)
                               && entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "TiposAfiliados.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Pacientes, bool>> expression0 = entity => entity.TiposAfiliadosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Pacientes"), typeof(Pacientes)));

       return rules;
       }

       #endregion
    }
 }
