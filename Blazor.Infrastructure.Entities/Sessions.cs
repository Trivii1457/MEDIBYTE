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
    /// Sessions object for mapped table Sessions.
    /// </summary>
    [Table("Sessions")]
    public partial class Sessions : BaseEntity
    {

       #region Columnas normales)

       [Column("Email")]
       [DDisplayName("Sessions.Email")]
       [DRequired("Sessions.Email")]
       [DStringLength("Sessions.Email",255)]
       public virtual String Email { get; set; }

       [Column("OfficeId")]
       [DDisplayName("Sessions.OfficeId")]
       [DRequired("Sessions.OfficeId")]
       public virtual Int64 OfficeId { get; set; }

       [Column("Token")]
       [DDisplayName("Sessions.Token")]
       [DRequired("Sessions.Token")]
       [DStringLength("Sessions.Token",512)]
       public virtual String Token { get; set; }

       [Column("ProfilesIds")]
       [DDisplayName("Sessions.ProfilesIds")]
       [DRequired("Sessions.ProfilesIds")]
       [DStringLength("Sessions.ProfilesIds",255)]
       public virtual String ProfilesIds { get; set; }

       [Column("ConnectionName")]
       [DDisplayName("Sessions.ConnectionName")]
       [DRequired("Sessions.ConnectionName")]
       [DStringLength("Sessions.ConnectionName",255)]
       public virtual String ConnectionName { get; set; }

       [Column("Host")]
       [DDisplayName("Sessions.Host")]
       [DStringLength("Sessions.Host",255)]
       public virtual String Host { get; set; }

       [Column("IsExpired")]
       [DDisplayName("Sessions.IsExpired")]
       [DRequired("Sessions.IsExpired")]
       public virtual Boolean IsExpired { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("UserId")]
       [DDisplayName("Sessions.UserId")]
       [DRequired("Sessions.UserId")]
       [DRequiredFK("Sessions.UserId")]
       public virtual Int64 UserId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("UserId")]
       public virtual User User { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Sessions, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Sessions, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Sessions, bool>> expression = null;

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
