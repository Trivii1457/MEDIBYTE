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
    /// UserOffice object for mapped table UserOffices.
    /// </summary>
    [Table("UserOffices")]
    public partial class UserOffice : BaseEntity
    {

       #region Columnas normales)

       [Column("OfficeId")]
       [DDisplayName("UserOffices.OfficeId")]
       [DRequired("UserOffices.OfficeId")]
       public virtual Int64 OfficeId { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("UserId")]
       [DDisplayName("UserOffices.UserId")]
       [DRequired("UserOffices.UserId")]
       [DRequiredFK("UserOffices.UserId")]
       public virtual Int64 UserId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("UserId")]
       public virtual User User { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<UserOffice, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<UserOffice, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<UserOffice, bool>> expression = null;

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
