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
    /// ProfileUser object for mapped table ProfileUsers.
    /// </summary>
    [Table("ProfileUsers")]
    public partial class ProfileUser : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("ProfileId")]
       [DDisplayName("ProfileUsers.ProfileId")]
       [DRequired("ProfileUsers.ProfileId")]
       [DRequiredFK("ProfileUsers.ProfileId")]
       public virtual Int64 ProfileId { get; set; }

       [Column("UserId")]
       [DDisplayName("ProfileUsers.UserId")]
       [DRequired("ProfileUsers.UserId")]
       [DRequiredFK("ProfileUsers.UserId")]
       public virtual Int64 UserId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("ProfileId")]
       public virtual Profile Profile { get; set; }

       [ForeignKey("UserId")]
       public virtual User User { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ProfileUser, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProfileUser, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProfileUser, bool>> expression = null;

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
