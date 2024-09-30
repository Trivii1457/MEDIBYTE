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
    /// PacientesEntidades object for mapped table PacientesEntidades.
    /// </summary>
    [Table("PacientesEntidades")]
    public partial class PacientesEntidades : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("PacientesId")]
       [DDisplayName("PacientesEntidades.PacientesId")]
       [DRequired("PacientesEntidades.PacientesId")]
       [DRequiredFK("PacientesEntidades.PacientesId")]
       public virtual Int64 PacientesId { get; set; }

       [Column("EntidadesId")]
       [DDisplayName("PacientesEntidades.EntidadesId")]
       [DRequired("PacientesEntidades.EntidadesId")]
       [DRequiredFK("PacientesEntidades.EntidadesId")]
       public virtual Int64 EntidadesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EntidadesId")]
       public virtual Entidades Entidades { get; set; }

       [ForeignKey("PacientesId")]
       public virtual Pacientes Pacientes { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<PacientesEntidades, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<PacientesEntidades, bool>> expression = null;

        expression = entity => entity.EntidadesId == this.EntidadesId && entity.PacientesId == this.PacientesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "PacientesEntidades.EntidadesId" , "PacientesEntidades.PacientesId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<PacientesEntidades, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.EntidadesId == this.EntidadesId && entity.PacientesId == this.PacientesId)
                               && entity.EntidadesId == this.EntidadesId && entity.PacientesId == this.PacientesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "PacientesEntidades.EntidadesId" , "PacientesEntidades.PacientesId" )));

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
