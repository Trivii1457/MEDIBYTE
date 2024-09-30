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
    /// OrdenesMedicamentos object for mapped table OrdenesMedicamentos.
    /// </summary>
    [Table("OrdenesMedicamentos")]
    public partial class OrdenesMedicamentos : BaseEntity
    {

       #region Columnas normales)

       [Column("Fecha", TypeName = "datetime")]
       [DDisplayName("OrdenesMedicamentos.Fecha")]
       [DRequired("OrdenesMedicamentos.Fecha")]
       public virtual DateTime Fecha { get; set; }

       [Column("NroOrden")]
       [DDisplayName("OrdenesMedicamentos.NroOrden")]
       [DRequired("OrdenesMedicamentos.NroOrden")]
       public virtual Int64 NroOrden { get; set; }

       [Column("FechaVencimiento", TypeName = "datetime")]
       [DDisplayName("OrdenesMedicamentos.FechaVencimiento")]
       [DRequired("OrdenesMedicamentos.FechaVencimiento")]
       public virtual DateTime FechaVencimiento { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("ProfesionalId")]
       [DDisplayName("OrdenesMedicamentos.ProfesionalId")]
       [DRequired("OrdenesMedicamentos.ProfesionalId")]
       [DRequiredFK("OrdenesMedicamentos.ProfesionalId")]
       public virtual Int64 ProfesionalId { get; set; }

       [Column("HIstoriasClinicasId")]
       [DDisplayName("OrdenesMedicamentos.HIstoriasClinicasId")]
       [DRequired("OrdenesMedicamentos.HIstoriasClinicasId")]
       [DRequiredFK("OrdenesMedicamentos.HIstoriasClinicasId")]
       public virtual Int64 HIstoriasClinicasId { get; set; }

       [Column("PacientesId")]
       [DDisplayName("OrdenesMedicamentos.PacientesId")]
       [DRequired("OrdenesMedicamentos.PacientesId")]
       [DRequiredFK("OrdenesMedicamentos.PacientesId")]
       public virtual Int64 PacientesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("ProfesionalId")]
       public virtual Empleados Profesional { get; set; }

       [ForeignKey("HIstoriasClinicasId")]
       public virtual HistoriasClinicas HIstoriasClinicas { get; set; }

       [ForeignKey("PacientesId")]
       public virtual Pacientes Pacientes { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<OrdenesMedicamentos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesMedicamentos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesMedicamentos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesMedicamentosDetalles, bool>> expression0 = entity => entity.OrdenesMedicamentosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesMedicamentosDetalles"), typeof(OrdenesMedicamentosDetalles)));

        Expression<Func<OrdenesMedicamentosDiagnosticos, bool>> expression1 = entity => entity.OrdenesMedicamentosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesMedicamentosDiagnosticos"), typeof(OrdenesMedicamentosDiagnosticos)));

       return rules;
       }

       #endregion
    }
 }
