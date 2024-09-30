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
    /// Atenciones object for mapped table Atenciones.
    /// </summary>
    [Table("Atenciones")]
    public partial class Atenciones : BaseEntity
    {

       #region Columnas normales)

       [Column("FechaAtencion", TypeName = "datetime")]
       [DDisplayName("Atenciones.FechaAtencion")]
       [DRequired("Atenciones.FechaAtencion")]
       public virtual DateTime FechaAtencion { get; set; }

       [Column("EstadosId")]
       [DDisplayName("Atenciones.EstadosId")]
       [DRequired("Atenciones.EstadosId")]
       public virtual Int64 EstadosId { get; set; }

       [Column("DetalleAnulacion")]
       [DDisplayName("Atenciones.DetalleAnulacion")]
       [DStringLength("Atenciones.DetalleAnulacion",1000)]
       public virtual String DetalleAnulacion { get; set; }

       [Column("NotaProcedimientos")]
       [DDisplayName("Atenciones.NotaProcedimientos")]
       public virtual String NotaProcedimientos { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("AdmisionesId")]
       [DDisplayName("Atenciones.AdmisionesId")]
       [DRequired("Atenciones.AdmisionesId")]
       [DRequiredFK("Atenciones.AdmisionesId")]
       public virtual Int64 AdmisionesId { get; set; }

       [Column("EmpleadosId")]
       [DDisplayName("Atenciones.EmpleadosId")]
       [DRequired("Atenciones.EmpleadosId")]
       [DRequiredFK("Atenciones.EmpleadosId")]
       public virtual Int64 EmpleadosId { get; set; }

       [Column("FinalidadConsultaId")]
       [DDisplayName("Atenciones.FinalidadConsultaId")]
       public virtual Int64? FinalidadConsultaId { get; set; }

       [Column("FinalidadProcedimientoId")]
       [DDisplayName("Atenciones.FinalidadProcedimientoId")]
       public virtual Int64? FinalidadProcedimientoId { get; set; }

       [Column("CausasExternasId")]
       [DDisplayName("Atenciones.CausasExternasId")]
       public virtual Int64? CausasExternasId { get; set; }

       [Column("AmbitoRealizacionProcedimientoId")]
       [DDisplayName("Atenciones.AmbitoRealizacionProcedimientoId")]
       public virtual Int64? AmbitoRealizacionProcedimientoId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("AdmisionesId")]
       public virtual Admisiones Admisiones { get; set; }

       [ForeignKey("AmbitoRealizacionProcedimientoId")]
       public virtual AmbitoRealizacionProcedimiento AmbitoRealizacionProcedimiento { get; set; }

       [ForeignKey("CausasExternasId")]
       public virtual CausasExternas CausasExternas { get; set; }

       [ForeignKey("EmpleadosId")]
       public virtual Empleados Empleados { get; set; }

       [ForeignKey("FinalidadConsultaId")]
       public virtual FinalidadConsulta FinalidadConsulta { get; set; }

       [ForeignKey("FinalidadProcedimientoId")]
       public virtual FinalidadProcedimiento FinalidadProcedimiento { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Atenciones, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Atenciones, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Atenciones, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdmisionesServiciosPrestados, bool>> expression0 = entity => entity.AtencionesId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdmisionesServiciosPrestados"), typeof(AdmisionesServiciosPrestados)));

        Expression<Func<HistoriasClinicas, bool>> expression1 = entity => entity.AtencionesId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicas"), typeof(HistoriasClinicas)));

        Expression<Func<RegistroIncapacidades, bool>> expression2 = entity => entity.AtencionesId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","RegistroIncapacidades"), typeof(RegistroIncapacidades)));

       return rules;
       }

       #endregion
    }
 }
