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
    /// Incapacidades object for mapped table Incapacidades.
    /// </summary>
    [Table("Incapacidades")]
    public partial class Incapacidades : BaseEntity
    {

       #region Columnas normales)

       [Column("Fecha", TypeName = "datetime")]
       [DDisplayName("Incapacidades.Fecha")]
       [DRequired("Incapacidades.Fecha")]
       public virtual DateTime Fecha { get; set; }

       [Column("NroOrden")]
       [DDisplayName("Incapacidades.NroOrden")]
       [DRequired("Incapacidades.NroOrden")]
       public virtual Int64 NroOrden { get; set; }

       [Column("NroDias")]
       [DDisplayName("Incapacidades.NroDias")]
       [DRequired("Incapacidades.NroDias")]
       public virtual Int16 NroDias { get; set; }

       [Column("FechaInicio", TypeName = "datetime")]
       [DDisplayName("Incapacidades.FechaInicio")]
       [DRequired("Incapacidades.FechaInicio")]
       public virtual DateTime FechaInicio { get; set; }

       [Column("Observaciones")]
       [DDisplayName("Incapacidades.Observaciones")]
       public virtual String Observaciones { get; set; }

       [Column("EsProrroga")]
       [DDisplayName("Incapacidades.EsProrroga")]
       [DRequired("Incapacidades.EsProrroga")]
       public virtual Boolean EsProrroga { get; set; }

       [Column("FechaFinalizacion", TypeName = "datetime")]
       [DDisplayName("Incapacidades.FechaFinalizacion")]
       [DRequired("Incapacidades.FechaFinalizacion")]
       public virtual DateTime FechaFinalizacion { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("ProfesionalId")]
       [DDisplayName("Incapacidades.ProfesionalId")]
       [DRequired("Incapacidades.ProfesionalId")]
       [DRequiredFK("Incapacidades.ProfesionalId")]
       public virtual Int64 ProfesionalId { get; set; }

       [Column("HIstoriasClinicasId")]
       [DDisplayName("Incapacidades.HIstoriasClinicasId")]
       [DRequired("Incapacidades.HIstoriasClinicasId")]
       [DRequiredFK("Incapacidades.HIstoriasClinicasId")]
       public virtual Int64 HIstoriasClinicasId { get; set; }

       [Column("PacientesId")]
       [DDisplayName("Incapacidades.PacientesId")]
       [DRequired("Incapacidades.PacientesId")]
       [DRequiredFK("Incapacidades.PacientesId")]
       public virtual Int64 PacientesId { get; set; }

       [Column("DiagnosticosId")]
       [DDisplayName("Incapacidades.DiagnosticosId")]
       [DRequired("Incapacidades.DiagnosticosId")]
       [DRequiredFK("Incapacidades.DiagnosticosId")]
       public virtual Int64 DiagnosticosId { get; set; }

       [Column("IncapacidadesOrigenesId")]
       [DDisplayName("Incapacidades.IncapacidadesOrigenesId")]
       [DRequired("Incapacidades.IncapacidadesOrigenesId")]
       [DRequiredFK("Incapacidades.IncapacidadesOrigenesId")]
       public virtual Int64 IncapacidadesOrigenesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("DiagnosticosId")]
       public virtual Diagnosticos Diagnosticos { get; set; }

       [ForeignKey("ProfesionalId")]
       public virtual Empleados Profesional { get; set; }

       [ForeignKey("HIstoriasClinicasId")]
       public virtual HistoriasClinicas HIstoriasClinicas { get; set; }

       [ForeignKey("IncapacidadesOrigenesId")]
       public virtual IncapacidadesOrigenes IncapacidadesOrigenes { get; set; }

       [ForeignKey("PacientesId")]
       public virtual Pacientes Pacientes { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Incapacidades, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Incapacidades, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Incapacidades, bool>> expression = null;

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
