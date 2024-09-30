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
    /// Profile object for mapped table Profiles.
    /// </summary>
    [Table("Profiles")]
    public partial class Profile : BaseEntity
    {

       #region Columnas normales)

       [Column("Description")]
       [DDisplayName("Profiles.Description")]
       [DRequired("Profiles.Description")]
       [DStringLength("Profiles.Description",255)]
       public virtual String Description { get; set; }

       [Column("SecurityPolicy")]
       [DDisplayName("Profiles.SecurityPolicy")]
       [DRequired("Profiles.SecurityPolicy")]
       public virtual Boolean SecurityPolicy { get; set; }

       [Column("IsActive")]
       [DDisplayName("Profiles.IsActive")]
       [DRequired("Profiles.IsActive")]
       public virtual Boolean IsActive { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("Profiles.EmpresasId")]
       [DRequired("Profiles.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Profile, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Profile, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Profile, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProfileNavigation, bool>> expression0 = entity => entity.ProfileId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProfileNavigation"), typeof(ProfileNavigation)));

        Expression<Func<ProfileUser, bool>> expression1 = entity => entity.ProfileId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProfileUsers"), typeof(ProfileUser)));

       return rules;
       }

       #endregion
    }
 }
