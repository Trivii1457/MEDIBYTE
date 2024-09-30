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
    /// LanguageResource object for mapped table LanguageResources.
    /// </summary>
    [Table("LanguageResources")]
    public partial class LanguageResource : BaseEntity
    {

       #region Columnas normales)

       [Column("ResourceKey")]
       [DDisplayName("LanguageResources.ResourceKey")]
       [DRequired("LanguageResources.ResourceKey")]
       [DStringLength("LanguageResources.ResourceKey",255)]
       public virtual String ResourceKey { get; set; }

       [Column("ResourceValue")]
       [DDisplayName("LanguageResources.ResourceValue")]
       [DRequired("LanguageResources.ResourceValue")]
       [DStringLength("LanguageResources.ResourceValue",1024)]
       public virtual String ResourceValue { get; set; }

       [Column("Active")]
       [DDisplayName("LanguageResources.Active")]
       [DRequired("LanguageResources.Active")]
       public virtual Boolean Active { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("LanguageId")]
       [DDisplayName("LanguageResources.LanguageId")]
       [DRequired("LanguageResources.LanguageId")]
       [DRequiredFK("LanguageResources.LanguageId")]
       public virtual Int64 LanguageId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("LanguageId")]
       public virtual Languages Language { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<LanguageResource, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<LanguageResource, bool>> expression = null;

        expression = entity => entity.LanguageId == this.LanguageId && entity.ResourceKey == this.ResourceKey;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "LanguageResources.LanguageId" , "LanguageResources.ResourceKey" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<LanguageResource, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.LanguageId == this.LanguageId && entity.ResourceKey == this.ResourceKey)
                               && entity.LanguageId == this.LanguageId && entity.ResourceKey == this.ResourceKey;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "LanguageResources.LanguageId" , "LanguageResources.ResourceKey" )));

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
