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
    /// Bancos object for mapped table Bancos.
    /// </summary>
    [Table("Bancos")]
    public partial class Bancos : BaseEntity
    {

       #region Columnas normales)

       [Column("Descripcion")]
       [DDisplayName("Bancos.Descripcion")]
       [DRequired("Bancos.Descripcion")]
       [DStringLength("Bancos.Descripcion",50)]
       public virtual String Descripcion { get; set; }

       [Column("Codigo")]
       [DDisplayName("Bancos.Codigo")]
       [DRequired("Bancos.Codigo")]
       [DStringLength("Bancos.Codigo",10)]
       public virtual String Codigo { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Bancos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Bancos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Bancos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Recaudos, bool>> expression0 = entity => entity.BancosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Recaudos"), typeof(Recaudos)));

       return rules;
       }

       #endregion
    }
 }
