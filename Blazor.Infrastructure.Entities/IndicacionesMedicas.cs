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
    /// IndicacionesMedicas object for mapped table IndicacionesMedicas.
    /// </summary>
    [Table("IndicacionesMedicas")]
    public partial class IndicacionesMedicas : BaseEntity
    {

       #region Columnas normales)

       [Column("Fecha", TypeName = "datetime")]
       [DDisplayName("IndicacionesMedicas.Fecha")]
       [DRequired("IndicacionesMedicas.Fecha")]
       public virtual DateTime Fecha { get; set; }

       [Column("NroOrden")]
       [DDisplayName("IndicacionesMedicas.NroOrden")]
       [DRequired("IndicacionesMedicas.NroOrden")]
       public virtual Int64 NroOrden { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("ProfesionalId")]
       [DDisplayName("IndicacionesMedicas.ProfesionalId")]
       [DRequired("IndicacionesMedicas.ProfesionalId")]
       [DRequiredFK("IndicacionesMedicas.ProfesionalId")]
       public virtual Int64 ProfesionalId { get; set; }

       [Column("HistoriasClinicasId")]
       [DDisplayName("IndicacionesMedicas.HistoriasClinicasId")]
       [DRequired("IndicacionesMedicas.HistoriasClinicasId")]
       [DRequiredFK("IndicacionesMedicas.HistoriasClinicasId")]
       public virtual Int64 HistoriasClinicasId { get; set; }

       [Column("PacientesId")]
       [DDisplayName("IndicacionesMedicas.PacientesId")]
       [DRequired("IndicacionesMedicas.PacientesId")]
       [DRequiredFK("IndicacionesMedicas.PacientesId")]
       public virtual Int64 PacientesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("ProfesionalId")]
       public virtual Empleados Profesional { get; set; }

       [ForeignKey("HistoriasClinicasId")]
       public virtual HistoriasClinicas HistoriasClinicas { get; set; }

       [ForeignKey("PacientesId")]
       public virtual Pacientes Pacientes { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<IndicacionesMedicas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<IndicacionesMedicas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<IndicacionesMedicas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<IndicacionesMedicasDetalles, bool>> expression0 = entity => entity.IndicacionesMedicasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","IndicacionesMedicasDetalles"), typeof(IndicacionesMedicasDetalles)));

       return rules;
       }

       #endregion
    }
 }
