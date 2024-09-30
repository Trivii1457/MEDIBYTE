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
    /// MenuAction object for mapped table MenuActions.
    /// </summary>
    [Table("MenuActions")]
    public partial class MenuAction : BaseEntity
    {

       #region Columnas normales)

       [Column("ActionName")]
       [DDisplayName("MenuActions.ActionName")]
       [DRequired("MenuActions.ActionName")]
       [DStringLength("MenuActions.ActionName",50)]
       public virtual String ActionName { get; set; }

       [Column("ResourceKey")]
       [DDisplayName("MenuActions.ResourceKey")]
       [DRequired("MenuActions.ResourceKey")]
       [DStringLength("MenuActions.ResourceKey",255)]
       public virtual String ResourceKey { get; set; }

       [Column("MenuId")]
       [DDisplayName("MenuActions.MenuId")]
       [DRequired("MenuActions.MenuId")]
       public virtual Int64 MenuId { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<MenuAction, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<MenuAction, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<MenuAction, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProfileNavigation, bool>> expression0 = entity => entity.MenuActionId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProfileNavigation"), typeof(ProfileNavigation)));

       return rules;
       }

       #endregion
    }
 }
