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
    /// Languages object for mapped table Languages.
    /// </summary>
    [Table("Languages")]
    public partial class Languages : BaseEntity
    {

       #region Columnas normales)

       [Column("Code")]
       [DDisplayName("Languages.Code")]
       [DRequired("Languages.Code")]
       [DStringLength("Languages.Code",10)]
       public virtual String Code { get; set; }

       [Column("Culture")]
       [DDisplayName("Languages.Culture")]
       [DRequired("Languages.Culture")]
       [DStringLength("Languages.Culture",50)]
       public virtual String Culture { get; set; }

       [Column("Name")]
       [DDisplayName("Languages.Name")]
       [DRequired("Languages.Name")]
       [DStringLength("Languages.Name",50)]
       public virtual String Name { get; set; }

       [Column("Active")]
       [DDisplayName("Languages.Active")]
       [DRequired("Languages.Active")]
       public virtual Boolean Active { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Languages, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Languages, bool>> expression = null;

        expression = entity => entity.Code == this.Code;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Languages.Code" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Languages, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Code == this.Code)
                               && entity.Code == this.Code;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Languages.Code" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<LanguageResource, bool>> expression0 = entity => entity.LanguageId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","LanguageResources"), typeof(LanguageResource)));

       return rules;
       }

       #endregion
    }
 }
