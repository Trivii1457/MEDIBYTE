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
    /// JobDetail object for mapped table JobDetails.
    /// </summary>
    [Table("JobDetails")]
    public partial class JobDetail : BaseEntity
    {

       #region Columnas normales)

       [Column("CronExpression")]
       [DDisplayName("JobDetails.CronExpression")]
       [DRequired("JobDetails.CronExpression")]
       [DStringLength("JobDetails.CronExpression",100)]
       public virtual String CronExpression { get; set; }

       [Column("Active")]
       [DDisplayName("JobDetails.Active")]
       [DRequired("JobDetails.Active")]
       public virtual Boolean Active { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("JobId")]
       [DDisplayName("JobDetails.JobId")]
       [DRequired("JobDetails.JobId")]
       [DRequiredFK("JobDetails.JobId")]
       public virtual Int64 JobId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("JobId")]
       public virtual Job Job { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<JobDetail, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<JobDetail, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<JobDetail, bool>> expression = null;

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
