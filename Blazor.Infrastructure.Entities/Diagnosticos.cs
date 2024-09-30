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
    /// Diagnosticos object for mapped table Diagnosticos.
    /// </summary>
    [Table("Diagnosticos")]
    public partial class Diagnosticos : BaseEntity
    {

       #region Columnas normales)

       [Column("Codigo")]
       [DDisplayName("Diagnosticos.Codigo")]
       [DRequired("Diagnosticos.Codigo")]
       [DStringLength("Diagnosticos.Codigo",45)]
       public virtual String Codigo { get; set; }

       [Column("Descripcion")]
       [DDisplayName("Diagnosticos.Descripcion")]
       [DRequired("Diagnosticos.Descripcion")]
       [DStringLength("Diagnosticos.Descripcion",255)]
       public virtual String Descripcion { get; set; }

       [Column("LimiteEdadInferior")]
       [DDisplayName("Diagnosticos.LimiteEdadInferior")]
       public virtual Int64? LimiteEdadInferior { get; set; }

       [Column("LimiteEdadSuperior")]
       [DDisplayName("Diagnosticos.LimiteEdadSuperior")]
       public virtual Int64? LimiteEdadSuperior { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("GenerosId")]
       [DDisplayName("Diagnosticos.GenerosId")]
       [DRequired("Diagnosticos.GenerosId")]
       [DRequiredFK("Diagnosticos.GenerosId")]
       public virtual Int64 GenerosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("GenerosId")]
       public virtual Generos Generos { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Diagnosticos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Diagnosticos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Diagnosticos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Admisiones, bool>> expression0 = entity => entity.DiagnosticosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<HistoriasClinicasDiagnosticos, bool>> expression1 = entity => entity.DiagnosticosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicasDiagnosticos"), typeof(HistoriasClinicasDiagnosticos)));

        Expression<Func<Incapacidades, bool>> expression2 = entity => entity.DiagnosticosId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Incapacidades"), typeof(Incapacidades)));

        Expression<Func<OrdenesMedicamentosDiagnosticos, bool>> expression3 = entity => entity.DiagnosticosId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesMedicamentosDiagnosticos"), typeof(OrdenesMedicamentosDiagnosticos)));

        Expression<Func<OrdenesServiciosDiagnosticos, bool>> expression4 = entity => entity.DiagnosticosId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesServiciosDiagnosticos"), typeof(OrdenesServiciosDiagnosticos)));

       return rules;
       }

       #endregion
    }
 }
