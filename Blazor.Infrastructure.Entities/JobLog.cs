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
    /// JobLog object for mapped table JobLogs.
    /// </summary>
    [Table("JobLogs")]
    public partial class JobLog : BaseEntity
    {

       #region Columnas normales)

       [Column("Class")]
       [DDisplayName("JobLogs.Class")]
       [DRequired("JobLogs.Class")]
       [DStringLength("JobLogs.Class",100)]
       public virtual String Class { get; set; }

       [Column("DateExecution", TypeName = "datetime")]
       [DDisplayName("JobLogs.DateExecution")]
       [DRequired("JobLogs.DateExecution")]
       public virtual DateTime DateExecution { get; set; }

       [Column("IsSuccess")]
       [DDisplayName("JobLogs.IsSuccess")]
       [DRequired("JobLogs.IsSuccess")]
       public virtual Boolean IsSuccess { get; set; }

       [Column("Error")]
       [DDisplayName("JobLogs.Error")]
       public virtual String Error { get; set; }

       [Column("Result")]
       [DDisplayName("JobLogs.Result")]
       public virtual String Result { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<JobLog, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<JobLog, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<JobLog, bool>> expression = null;

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
