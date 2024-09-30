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
    /// OrdenesMedicamentosDiagnosticos object for mapped table OrdenesMedicamentosDiagnosticos.
    /// </summary>
    [Table("OrdenesMedicamentosDiagnosticos")]
    public partial class OrdenesMedicamentosDiagnosticos : BaseEntity
    {

       #region Columnas normales)

       [Column("Principal")]
       [DDisplayName("OrdenesMedicamentosDiagnosticos.Principal")]
       [DRequired("OrdenesMedicamentosDiagnosticos.Principal")]
       public virtual Boolean Principal { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("DiagnosticosId")]
       [DDisplayName("OrdenesMedicamentosDiagnosticos.DiagnosticosId")]
       [DRequired("OrdenesMedicamentosDiagnosticos.DiagnosticosId")]
       [DRequiredFK("OrdenesMedicamentosDiagnosticos.DiagnosticosId")]
       public virtual Int64 DiagnosticosId { get; set; }

       [Column("OrdenesMedicamentosId")]
       [DDisplayName("OrdenesMedicamentosDiagnosticos.OrdenesMedicamentosId")]
       public virtual Int64? OrdenesMedicamentosId { get; set; }

       [Column("TiposDiagnosticosId")]
       [DDisplayName("OrdenesMedicamentosDiagnosticos.TiposDiagnosticosId")]
       [DRequired("OrdenesMedicamentosDiagnosticos.TiposDiagnosticosId")]
       [DRequiredFK("OrdenesMedicamentosDiagnosticos.TiposDiagnosticosId")]
       public virtual Int64 TiposDiagnosticosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("DiagnosticosId")]
       public virtual Diagnosticos Diagnosticos { get; set; }

       [ForeignKey("OrdenesMedicamentosId")]
       public virtual OrdenesMedicamentos OrdenesMedicamentos { get; set; }

       [ForeignKey("TiposDiagnosticosId")]
       public virtual TiposDiagnosticos TiposDiagnosticos { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<OrdenesMedicamentosDiagnosticos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesMedicamentosDiagnosticos, bool>> expression = null;

        expression = entity => entity.DiagnosticosId == this.DiagnosticosId && entity.OrdenesMedicamentosId == this.OrdenesMedicamentosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "OrdenesMedicamentosDiagnosticos.DiagnosticosId" , "OrdenesMedicamentosDiagnosticos.OrdenesMedicamentosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesMedicamentosDiagnosticos, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.DiagnosticosId == this.DiagnosticosId && entity.OrdenesMedicamentosId == this.OrdenesMedicamentosId)
                               && entity.DiagnosticosId == this.DiagnosticosId && entity.OrdenesMedicamentosId == this.OrdenesMedicamentosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "OrdenesMedicamentosDiagnosticos.DiagnosticosId" , "OrdenesMedicamentosDiagnosticos.OrdenesMedicamentosId" )));

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
