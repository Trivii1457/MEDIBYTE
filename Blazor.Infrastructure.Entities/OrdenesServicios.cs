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
    /// OrdenesServicios object for mapped table OrdenesServicios.
    /// </summary>
    [Table("OrdenesServicios")]
    public partial class OrdenesServicios : BaseEntity
    {

       #region Columnas normales)

       [Column("Fecha", TypeName = "datetime")]
       [DDisplayName("OrdenesServicios.Fecha")]
       [DRequired("OrdenesServicios.Fecha")]
       public virtual DateTime Fecha { get; set; }

       [Column("NroOrden")]
       [DDisplayName("OrdenesServicios.NroOrden")]
       [DRequired("OrdenesServicios.NroOrden")]
       public virtual Int64 NroOrden { get; set; }

       [Column("FechaVencimiento", TypeName = "datetime")]
       [DDisplayName("OrdenesServicios.FechaVencimiento")]
       [DRequired("OrdenesServicios.FechaVencimiento")]
       public virtual DateTime FechaVencimiento { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("ProfesionalId")]
       [DDisplayName("OrdenesServicios.ProfesionalId")]
       [DRequired("OrdenesServicios.ProfesionalId")]
       [DRequiredFK("OrdenesServicios.ProfesionalId")]
       public virtual Int64 ProfesionalId { get; set; }

       [Column("HIstoriasClinicasId")]
       [DDisplayName("OrdenesServicios.HIstoriasClinicasId")]
       [DRequired("OrdenesServicios.HIstoriasClinicasId")]
       [DRequiredFK("OrdenesServicios.HIstoriasClinicasId")]
       public virtual Int64 HIstoriasClinicasId { get; set; }

       [Column("PacientesId")]
       [DDisplayName("OrdenesServicios.PacientesId")]
       [DRequired("OrdenesServicios.PacientesId")]
       [DRequiredFK("OrdenesServicios.PacientesId")]
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
       Expression<Func<OrdenesServicios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesServicios, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesServicios, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesServiciosDetalles, bool>> expression0 = entity => entity.OrdenesServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesServiciosDetalles"), typeof(OrdenesServiciosDetalles)));

        Expression<Func<OrdenesServiciosDiagnosticos, bool>> expression1 = entity => entity.OrdenesServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesServiciosDiagnosticos"), typeof(OrdenesServiciosDiagnosticos)));

       return rules;
       }

       #endregion
    }
 }
