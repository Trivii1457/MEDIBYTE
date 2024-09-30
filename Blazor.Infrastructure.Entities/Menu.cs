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
    /// Menu object for mapped table Menus.
    /// </summary>
    [Table("Menus")]
    public partial class Menu : BaseEntity
    {

       #region Columnas normales)

       [Column("Name")]
       [DDisplayName("Menus.Name")]
       [DRequired("Menus.Name")]
       [DStringLength("Menus.Name",50)]
       public virtual String Name { get; set; }

       [Column("ResourceKey")]
       [DDisplayName("Menus.ResourceKey")]
       [DRequired("Menus.ResourceKey")]
       [DStringLength("Menus.ResourceKey",255)]
       public virtual String ResourceKey { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Menu, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Menu, bool>> expression = null;

        expression = entity => entity.Name == this.Name;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Menus.Name" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Menu, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Name == this.Name)
                               && entity.Name == this.Name;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Menus.Name" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProfileNavigation, bool>> expression0 = entity => entity.MenuId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProfileNavigation"), typeof(ProfileNavigation)));

       return rules;
       }

       #endregion
    }
 }
