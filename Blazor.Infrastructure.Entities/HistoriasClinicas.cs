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
    /// HistoriasClinicas object for mapped table HistoriasClinicas.
    /// </summary>
    [Table("HistoriasClinicas")]
    public partial class HistoriasClinicas : BaseEntity
    {

       #region Columnas normales)

       [Column("Consecutivo")]
       [DDisplayName("HistoriasClinicas.Consecutivo")]
       [DRequired("HistoriasClinicas.Consecutivo")]
       [DStringLength("HistoriasClinicas.Consecutivo",45)]
       public virtual String Consecutivo { get; set; }

       [Column("FechaApertura", TypeName = "datetime")]
       [DDisplayName("HistoriasClinicas.FechaApertura")]
       public virtual DateTime? FechaApertura { get; set; }

       [Column("MotivoConsulta")]
       [DDisplayName("HistoriasClinicas.MotivoConsulta")]
       public virtual String MotivoConsulta { get; set; }

       [Column("AntecedentesFamiliar")]
       [DDisplayName("HistoriasClinicas.AntecedentesFamiliar")]
       public virtual String AntecedentesFamiliar { get; set; }

       [Column("AntecedentesPersonal")]
       [DDisplayName("HistoriasClinicas.AntecedentesPersonal")]
       public virtual String AntecedentesPersonal { get; set; }

       [Column("EnfermedadActual")]
       [DDisplayName("HistoriasClinicas.EnfermedadActual")]
       public virtual String EnfermedadActual { get; set; }

       [Column("PlanManejo")]
       [DDisplayName("HistoriasClinicas.PlanManejo")]
       public virtual String PlanManejo { get; set; }

       [Column("EsControl")]
       [DDisplayName("HistoriasClinicas.EsControl")]
       [DRequired("HistoriasClinicas.EsControl")]
       public virtual Boolean EsControl { get; set; }

       [Column("Peso")]
       [DDisplayName("HistoriasClinicas.Peso")]
       [DRequired("HistoriasClinicas.Peso")]
       public virtual Decimal Peso { get; set; }

       [Column("Altura")]
       [DDisplayName("HistoriasClinicas.Altura")]
       [DRequired("HistoriasClinicas.Altura")]
       public virtual Decimal Altura { get; set; }

       [Column("TensionArterial")]
       [DDisplayName("HistoriasClinicas.TensionArterial")]
       [DStringLength("HistoriasClinicas.TensionArterial",100)]
       public virtual String TensionArterial { get; set; }

       [Column("FrecuenciaCardiaca")]
       [DDisplayName("HistoriasClinicas.FrecuenciaCardiaca")]
       [DStringLength("HistoriasClinicas.FrecuenciaCardiaca",100)]
       public virtual String FrecuenciaCardiaca { get; set; }

       [Column("FrecuenciaRespiratoria")]
       [DDisplayName("HistoriasClinicas.FrecuenciaRespiratoria")]
       [DStringLength("HistoriasClinicas.FrecuenciaRespiratoria",100)]
       public virtual String FrecuenciaRespiratoria { get; set; }

       [Column("Temperatura")]
       [DDisplayName("HistoriasClinicas.Temperatura")]
       [DStringLength("HistoriasClinicas.Temperatura",100)]
       public virtual String Temperatura { get; set; }

       [Column("PerimetroCefalico")]
       [DDisplayName("HistoriasClinicas.PerimetroCefalico")]
       [DStringLength("HistoriasClinicas.PerimetroCefalico",100)]
       public virtual String PerimetroCefalico { get; set; }

       [Column("Hallazgos")]
       [DDisplayName("HistoriasClinicas.Hallazgos")]
       public virtual String Hallazgos { get; set; }

       [Column("Analisis")]
       [DDisplayName("HistoriasClinicas.Analisis")]
       public virtual String Analisis { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("AtencionesId")]
       [DDisplayName("HistoriasClinicas.AtencionesId")]
       [DRequired("HistoriasClinicas.AtencionesId")]
       [DRequiredFK("HistoriasClinicas.AtencionesId")]
       public virtual Int64 AtencionesId { get; set; }

       [Column("ProfesionalId")]
       [DDisplayName("HistoriasClinicas.ProfesionalId")]
       [DRequired("HistoriasClinicas.ProfesionalId")]
       [DRequiredFK("HistoriasClinicas.ProfesionalId")]
       public virtual Int64 ProfesionalId { get; set; }

       [Column("EstadosId")]
       [DDisplayName("HistoriasClinicas.EstadosId")]
       [DRequired("HistoriasClinicas.EstadosId")]
       [DRequiredFK("HistoriasClinicas.EstadosId")]
       public virtual Int64 EstadosId { get; set; }

       [Column("HCTiposId")]
       [DDisplayName("HistoriasClinicas.HCTiposId")]
       [DRequired("HistoriasClinicas.HCTiposId")]
       [DRequiredFK("HistoriasClinicas.HCTiposId")]
       public virtual Int64 HCTiposId { get; set; }

       [Column("PacientesId")]
       [DDisplayName("HistoriasClinicas.PacientesId")]
       [DRequired("HistoriasClinicas.PacientesId")]
       [DRequiredFK("HistoriasClinicas.PacientesId")]
       public virtual Int64 PacientesId { get; set; }

       [Column("HistoriaClinicaPrincipal")]
       [DDisplayName("HistoriasClinicas.HistoriaClinicaPrincipal")]
       public virtual Int64? HistoriaClinicaPrincipal { get; set; }

       [Column("DominanciaEstadosId")]
       [DDisplayName("HistoriasClinicas.DominanciaEstadosId")]
       public virtual Int64? DominanciaEstadosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("AtencionesId")]
       public virtual Atenciones Atenciones { get; set; }

       [ForeignKey("DominanciaEstadosId")]
       public virtual Estados DominanciaEstados { get; set; }

       [ForeignKey("ProfesionalId")]
       public virtual Empleados Profesional { get; set; }

       [ForeignKey("EstadosId")]
       public virtual Estados Estados { get; set; }

       [ForeignKey("HCTiposId")]
       public virtual HCTipos HCTipos { get; set; }

       [ForeignKey("HistoriaClinicaPrincipal")]
       public virtual HistoriasClinicas HistoriaClinicaPrincip { get; set; }

       [ForeignKey("PacientesId")]
       public virtual Pacientes Pacientes { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<HistoriasClinicas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HistoriasClinicas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HistoriasClinicas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HistoriasClinicas, bool>> expression0 = entity => entity.HistoriaClinicaPrincipal == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicas"), typeof(HistoriasClinicas)));

        Expression<Func<HistoriasClinicasDiagnosticos, bool>> expression1 = entity => entity.HistoriasClinicasId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicasDiagnosticos"), typeof(HistoriasClinicasDiagnosticos)));

        Expression<Func<HistoriasClinicasNotasAclaratorias, bool>> expression2 = entity => entity.HistoriasClinicasId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicasNotasAclaratorias"), typeof(HistoriasClinicasNotasAclaratorias)));

        Expression<Func<HistoriasClinicasRespuestas, bool>> expression3 = entity => entity.HIstoriasClinicasId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicasRespuestas"), typeof(HistoriasClinicasRespuestas)));

        Expression<Func<Incapacidades, bool>> expression4 = entity => entity.HIstoriasClinicasId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Incapacidades"), typeof(Incapacidades)));

        Expression<Func<IndicacionesMedicas, bool>> expression5 = entity => entity.HistoriasClinicasId == this.Id;
        rules.Add(new ExpRecurso(expression5.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","IndicacionesMedicas"), typeof(IndicacionesMedicas)));

        Expression<Func<OrdenesMedicamentos, bool>> expression6 = entity => entity.HIstoriasClinicasId == this.Id;
        rules.Add(new ExpRecurso(expression6.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesMedicamentos"), typeof(OrdenesMedicamentos)));

        Expression<Func<OrdenesServicios, bool>> expression7 = entity => entity.HIstoriasClinicasId == this.Id;
        rules.Add(new ExpRecurso(expression7.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesServicios"), typeof(OrdenesServicios)));

       return rules;
       }

       #endregion
    }
 }
