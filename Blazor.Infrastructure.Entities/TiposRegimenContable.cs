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
    /// TiposRegimenContable object for mapped table TiposRegimenContable.
    /// </summary>
    [Table("TiposRegimenContable")]
    public partial class TiposRegimenContable : BaseEntity
    {

       #region Columnas normales)

       [Column("Codigo")]
       [DDisplayName("TiposRegimenContable.Codigo")]
       [DRequired("TiposRegimenContable.Codigo")]
       [DStringLength("TiposRegimenContable.Codigo",5)]
       public virtual String Codigo { get; set; }

       [Column("Descripcion")]
       [DDisplayName("TiposRegimenContable.Descripcion")]
       [DRequired("TiposRegimenContable.Descripcion")]
       [DStringLength("TiposRegimenContable.Descripcion",150)]
       public virtual String Descripcion { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<TiposRegimenContable, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposRegimenContable, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposRegimenContable, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Empresas, bool>> expression0 = entity => entity.TiposRegimenContableId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Empresas"), typeof(Empresas)));

       return rules;
       }

       #endregion
    }
 }
