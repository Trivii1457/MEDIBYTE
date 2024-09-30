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
    /// ProfileNavigation object for mapped table ProfileNavigation.
    /// </summary>
    [Table("ProfileNavigation")]
    public partial class ProfileNavigation : BaseEntity
    {

       #region Columnas normales)

       [Column("Path")]
       [DDisplayName("ProfileNavigation.Path")]
       [DRequired("ProfileNavigation.Path")]
       [DStringLength("ProfileNavigation.Path",255)]
       public virtual String Path { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("ProfileId")]
       [DDisplayName("ProfileNavigation.ProfileId")]
       [DRequired("ProfileNavigation.ProfileId")]
       [DRequiredFK("ProfileNavigation.ProfileId")]
       public virtual Int64 ProfileId { get; set; }

       [Column("MenuId")]
       [DDisplayName("ProfileNavigation.MenuId")]
       [DRequired("ProfileNavigation.MenuId")]
       [DRequiredFK("ProfileNavigation.MenuId")]
       public virtual Int64 MenuId { get; set; }

       [Column("MenuActionId")]
       [DDisplayName("ProfileNavigation.MenuActionId")]
       [DRequired("ProfileNavigation.MenuActionId")]
       [DRequiredFK("ProfileNavigation.MenuActionId")]
       public virtual Int64 MenuActionId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("MenuId")]
       public virtual Menu Menu { get; set; }

       [ForeignKey("MenuActionId")]
       public virtual MenuAction MenuAction { get; set; }

       [ForeignKey("ProfileId")]
       public virtual Profile Profile { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ProfileNavigation, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProfileNavigation, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProfileNavigation, bool>> expression = null;

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
