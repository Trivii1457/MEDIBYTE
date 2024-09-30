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
    /// HistoriasClinicasDiagnosticos object for mapped table HistoriasClinicasDiagnosticos.
    /// </summary>
    [Table("HistoriasClinicasDiagnosticos")]
    public partial class HistoriasClinicasDiagnosticos : BaseEntity
    {

       #region Columnas normales)

       [Column("Principal")]
       [DDisplayName("HistoriasClinicasDiagnosticos.Principal")]
       [DRequired("HistoriasClinicasDiagnosticos.Principal")]
       public virtual Boolean Principal { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("HistoriasClinicasId")]
       [DDisplayName("HistoriasClinicasDiagnosticos.HistoriasClinicasId")]
       [DRequired("HistoriasClinicasDiagnosticos.HistoriasClinicasId")]
       [DRequiredFK("HistoriasClinicasDiagnosticos.HistoriasClinicasId")]
       public virtual Int64 HistoriasClinicasId { get; set; }

       [Column("DiagnosticosId")]
       [DDisplayName("HistoriasClinicasDiagnosticos.DiagnosticosId")]
       [DRequired("HistoriasClinicasDiagnosticos.DiagnosticosId")]
       [DRequiredFK("HistoriasClinicasDiagnosticos.DiagnosticosId")]
       public virtual Int64 DiagnosticosId { get; set; }

       [Column("TiposDiagnosticosId")]
       [DDisplayName("HistoriasClinicasDiagnosticos.TiposDiagnosticosId")]
       [DRequired("HistoriasClinicasDiagnosticos.TiposDiagnosticosId")]
       [DRequiredFK("HistoriasClinicasDiagnosticos.TiposDiagnosticosId")]
       public virtual Int64 TiposDiagnosticosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("DiagnosticosId")]
       public virtual Diagnosticos Diagnosticos { get; set; }

       [ForeignKey("HistoriasClinicasId")]
       public virtual HistoriasClinicas HistoriasClinicas { get; set; }

       [ForeignKey("TiposDiagnosticosId")]
       public virtual TiposDiagnosticos TiposDiagnosticos { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<HistoriasClinicasDiagnosticos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HistoriasClinicasDiagnosticos, bool>> expression = null;

        expression = entity => entity.HistoriasClinicasId == this.HistoriasClinicasId && entity.DiagnosticosId == this.DiagnosticosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "HistoriasClinicasDiagnosticos.HistoriasClinicasId" , "HistoriasClinicasDiagnosticos.DiagnosticosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HistoriasClinicasDiagnosticos, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.HistoriasClinicasId == this.HistoriasClinicasId && entity.DiagnosticosId == this.DiagnosticosId)
                               && entity.HistoriasClinicasId == this.HistoriasClinicasId && entity.DiagnosticosId == this.DiagnosticosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "HistoriasClinicasDiagnosticos.HistoriasClinicasId" , "HistoriasClinicasDiagnosticos.DiagnosticosId" )));

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
