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
    /// TiposDiagnosticos object for mapped table TiposDiagnosticos.
    /// </summary>
    [Table("TiposDiagnosticos")]
    public partial class TiposDiagnosticos : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("TiposDiagnosticos.Nombre")]
       [DRequired("TiposDiagnosticos.Nombre")]
       [DStringLength("TiposDiagnosticos.Nombre",255)]
       public virtual String Nombre { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<TiposDiagnosticos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposDiagnosticos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposDiagnosticos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HistoriasClinicasDiagnosticos, bool>> expression0 = entity => entity.TiposDiagnosticosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicasDiagnosticos"), typeof(HistoriasClinicasDiagnosticos)));

        Expression<Func<OrdenesMedicamentosDiagnosticos, bool>> expression1 = entity => entity.TiposDiagnosticosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesMedicamentosDiagnosticos"), typeof(OrdenesMedicamentosDiagnosticos)));

        Expression<Func<OrdenesServiciosDiagnosticos, bool>> expression2 = entity => entity.TiposDiagnosticosId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesServiciosDiagnosticos"), typeof(OrdenesServiciosDiagnosticos)));

       return rules;
       }

       #endregion
    }
 }
