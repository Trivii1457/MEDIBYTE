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
    /// ProgramacionCitas object for mapped table ProgramacionCitas.
    /// </summary>
    [Table("ProgramacionCitas")]
    public partial class ProgramacionCitas : BaseEntity
    {

       #region Columnas normales)

       [Column("Consecutivo")]
       [DDisplayName("ProgramacionCitas.Consecutivo")]
       [DRequired("ProgramacionCitas.Consecutivo")]
       public virtual Int32 Consecutivo { get; set; }

       [Column("FechaInicio", TypeName = "datetime")]
       [DDisplayName("ProgramacionCitas.FechaInicio")]
       [DRequired("ProgramacionCitas.FechaInicio")]
       public virtual DateTime FechaInicio { get; set; }

       [Column("FechaFinal", TypeName = "datetime")]
       [DDisplayName("ProgramacionCitas.FechaFinal")]
       [DRequired("ProgramacionCitas.FechaFinal")]
       public virtual DateTime FechaFinal { get; set; }

       [Column("Cantidad")]
       [DDisplayName("ProgramacionCitas.Cantidad")]
       [DRequired("ProgramacionCitas.Cantidad")]
       public virtual Int16 Cantidad { get; set; }

       [Column("Observaciones")]
       [DDisplayName("ProgramacionCitas.Observaciones")]
       public virtual String Observaciones { get; set; }

       [Column("Duracion")]
       [DDisplayName("ProgramacionCitas.Duracion")]
       [DRequired("ProgramacionCitas.Duracion")]
       public virtual Int64 Duracion { get; set; }

       [Column("MotivoCancelacion")]
       [DDisplayName("ProgramacionCitas.MotivoCancelacion")]
       [DStringLength("ProgramacionCitas.MotivoCancelacion",1000)]
       public virtual String MotivoCancelacion { get; set; }

       [Column("MotivoReprogramacion")]
       [DDisplayName("ProgramacionCitas.MotivoReprogramacion")]
       [DStringLength("ProgramacionCitas.MotivoReprogramacion",1000)]
       public virtual String MotivoReprogramacion { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("PacientesId")]
       [DDisplayName("ProgramacionCitas.PacientesId")]
       [DRequired("ProgramacionCitas.PacientesId")]
       [DRequiredFK("ProgramacionCitas.PacientesId")]
       public virtual Int64 PacientesId { get; set; }

       [Column("ServiciosId")]
       [DDisplayName("ProgramacionCitas.ServiciosId")]
       [DRequired("ProgramacionCitas.ServiciosId")]
       [DRequiredFK("ProgramacionCitas.ServiciosId")]
       public virtual Int64 ServiciosId { get; set; }

       [Column("EspecialidadesId")]
       [DDisplayName("ProgramacionCitas.EspecialidadesId")]
       [DRequired("ProgramacionCitas.EspecialidadesId")]
       [DRequiredFK("ProgramacionCitas.EspecialidadesId")]
       public virtual Int64 EspecialidadesId { get; set; }

       [Column("TiposCitasId")]
       [DDisplayName("ProgramacionCitas.TiposCitasId")]
       [DRequired("ProgramacionCitas.TiposCitasId")]
       [DRequiredFK("ProgramacionCitas.TiposCitasId")]
       public virtual Int64 TiposCitasId { get; set; }

       [Column("SedesId")]
       [DDisplayName("ProgramacionCitas.SedesId")]
       [DRequired("ProgramacionCitas.SedesId")]
       [DRequiredFK("ProgramacionCitas.SedesId")]
       public virtual Int64 SedesId { get; set; }

       [Column("ConsultoriosId")]
       [DDisplayName("ProgramacionCitas.ConsultoriosId")]
       [DRequired("ProgramacionCitas.ConsultoriosId")]
       [DRequiredFK("ProgramacionCitas.ConsultoriosId")]
       public virtual Int64 ConsultoriosId { get; set; }

       [Column("EmpleadosId")]
       [DDisplayName("ProgramacionCitas.EmpleadosId")]
       public virtual Int64? EmpleadosId { get; set; }

       [Column("EntidadesId")]
       [DDisplayName("ProgramacionCitas.EntidadesId")]
       public virtual Int64? EntidadesId { get; set; }

       [Column("ConveniosId")]
       [DDisplayName("ProgramacionCitas.ConveniosId")]
       public virtual Int64? ConveniosId { get; set; }

       [Column("EstadosId")]
       [DDisplayName("ProgramacionCitas.EstadosId")]
       [DRequired("ProgramacionCitas.EstadosId")]
       [DRequiredFK("ProgramacionCitas.EstadosId")]
       public virtual Int64 EstadosId { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("ProgramacionCitas.EmpresasId")]
       [DRequired("ProgramacionCitas.EmpresasId")]
       [DRequiredFK("ProgramacionCitas.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("EstadosIdTipoDuracion")]
       [DDisplayName("ProgramacionCitas.EstadosIdTipoDuracion")]
       [DRequired("ProgramacionCitas.EstadosIdTipoDuracion")]
       [DRequiredFK("ProgramacionCitas.EstadosIdTipoDuracion")]
       public virtual Int64 EstadosIdTipoDuracion { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EstadosIdTipoDuracion")]
       public virtual Estados EstadosIdTipoDuraci { get; set; }

       [ForeignKey("ConsultoriosId")]
       public virtual Consultorios Consultorios { get; set; }

       [ForeignKey("ConveniosId")]
       public virtual Convenios Convenios { get; set; }

       [ForeignKey("EmpleadosId")]
       public virtual Empleados Empleados { get; set; }

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("EntidadesId")]
       public virtual Entidades Entidades { get; set; }

       [ForeignKey("EspecialidadesId")]
       public virtual Especialidades Especialidades { get; set; }

       [ForeignKey("EstadosId")]
       public virtual Estados Estados { get; set; }

       [ForeignKey("PacientesId")]
       public virtual Pacientes Pacientes { get; set; }

       [ForeignKey("SedesId")]
       public virtual Sedes Sedes { get; set; }

       [ForeignKey("ServiciosId")]
       public virtual Servicios Servicios { get; set; }

       [ForeignKey("TiposCitasId")]
       public virtual TiposCitas TiposCitas { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ProgramacionCitas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProgramacionCitas, bool>> expression = null;

        expression = entity => entity.Consecutivo == this.Consecutivo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ProgramacionCitas.Consecutivo" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProgramacionCitas, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Consecutivo == this.Consecutivo)
                               && entity.Consecutivo == this.Consecutivo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ProgramacionCitas.Consecutivo" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Admisiones, bool>> expression0 = entity => entity.ProgramacionCitasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

       return rules;
       }

       #endregion
    }
 }
