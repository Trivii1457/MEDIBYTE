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
    /// Audit object for mapped table Audits.
    /// </summary>
    [Table("Audits")]
    public partial class Audit : BaseEntity
    {

       #region Columnas normales)

       [Column("TableName")]
       [DDisplayName("Audits.TableName")]
       [DRequired("Audits.TableName")]
       [DStringLength("Audits.TableName",255)]
       public virtual String TableName { get; set; }

       [Column("Action")]
       [DDisplayName("Audits.Action")]
       [DStringLength("Audits.Action",50)]
       public virtual String Action { get; set; }

       [Column("TransactionDate", TypeName = "datetime")]
       [DDisplayName("Audits.TransactionDate")]
       [DRequired("Audits.TransactionDate")]
       public virtual DateTime TransactionDate { get; set; }

       [Column("KeyValues")]
       [DDisplayName("Audits.KeyValues")]
       [DRequired("Audits.KeyValues")]
       [DStringLength("Audits.KeyValues",255)]
       public virtual String KeyValues { get; set; }

       [Column("OldValues")]
       [DDisplayName("Audits.OldValues")]
       public virtual String OldValues { get; set; }

       [Column("NewValues")]
       [DDisplayName("Audits.NewValues")]
       public virtual String NewValues { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Audit, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Audit, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Audit, bool>> expression = null;

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
