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
    /// CoberturaPlanBeneficios object for mapped table CoberturaPlanBeneficios.
    /// </summary>
    [Table("CoberturaPlanBeneficios")]
    public partial class CoberturaPlanBeneficios : BaseEntity
    {

       #region Columnas normales)

       [Column("Descripcion")]
       [DDisplayName("CoberturaPlanBeneficios.Descripcion")]
       [DRequired("CoberturaPlanBeneficios.Descripcion")]
       [DStringLength("CoberturaPlanBeneficios.Descripcion",1024)]
       public virtual String Descripcion { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<CoberturaPlanBeneficios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CoberturaPlanBeneficios, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CoberturaPlanBeneficios, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Admisiones, bool>> expression0 = entity => entity.CoberturaPlanBeneficiosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

       return rules;
       }

       #endregion
    }
 }
