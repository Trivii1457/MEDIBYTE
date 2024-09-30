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
    /// Job object for mapped table Jobs.
    /// </summary>
    [Table("Jobs")]
    public partial class Job : BaseEntity
    {

       #region Columnas normales)

       [Column("Class")]
       [DDisplayName("Jobs.Class")]
       [DRequired("Jobs.Class")]
       [DStringLength("Jobs.Class",100)]
       public virtual String Class { get; set; }

       [Column("Description")]
       [DDisplayName("Jobs.Description")]
       [DRequired("Jobs.Description")]
       [DStringLength("Jobs.Description",255)]
       public virtual String Description { get; set; }

       [Column("Active")]
       [DDisplayName("Jobs.Active")]
       [DRequired("Jobs.Active")]
       public virtual Boolean Active { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Job, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Job, bool>> expression = null;

        expression = entity => entity.Class == this.Class;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Jobs.Class" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Job, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Class == this.Class)
                               && entity.Class == this.Class;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Jobs.Class" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<JobDetail, bool>> expression0 = entity => entity.JobId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","JobDetails"), typeof(JobDetail)));

       return rules;
       }

       #endregion
    }
 }
