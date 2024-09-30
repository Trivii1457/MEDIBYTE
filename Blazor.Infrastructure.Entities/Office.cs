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
    /// Office object for mapped table Offices.
    /// </summary>
    [Table("Offices")]
    public partial class Office : BaseEntity
    {

       #region Columnas normales)

       [Column("Code")]
       [DDisplayName("Offices.Code")]
       [DRequired("Offices.Code")]
       [DStringLength("Offices.Code",10)]
       public virtual String Code { get; set; }

       [Column("Description")]
       [DDisplayName("Offices.Description")]
       [DRequired("Offices.Description")]
       [DStringLength("Offices.Description",50)]
       public virtual String Description { get; set; }

       [Column("ShortDescription")]
       [DDisplayName("Offices.ShortDescription")]
       [DRequired("Offices.ShortDescription")]
       [DStringLength("Offices.ShortDescription",20)]
       public virtual String ShortDescription { get; set; }

       [Column("IsActive")]
       [DDisplayName("Offices.IsActive")]
       [DRequired("Offices.IsActive")]
       public virtual Boolean IsActive { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Office, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Office, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Office, bool>> expression = null;

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
