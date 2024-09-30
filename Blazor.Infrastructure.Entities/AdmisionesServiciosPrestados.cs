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
    /// AdmisionesServiciosPrestados object for mapped table AdmisionesServiciosPrestados.
    /// </summary>
    [Table("AdmisionesServiciosPrestados")]
    public partial class AdmisionesServiciosPrestados : BaseEntity
    {

        #region Columnas normales)

        [Column("ValorServicio")]
        [DDisplayName("AdmisionesServiciosPrestados.ValorServicio")]
        [DRequired("AdmisionesServiciosPrestados.ValorServicio")]
        public virtual Decimal ValorServicio { get; set; }

        [Column("Observaciones")]
        [DDisplayName("AdmisionesServiciosPrestados.Observaciones")]
        [DStringLength("AdmisionesServiciosPrestados.Observaciones", 1024)]
        public virtual String Observaciones { get; set; }

        [Column("Recomendaciones")]
        [DDisplayName("AdmisionesServiciosPrestados.Recomendaciones")]
        [DStringLength("AdmisionesServiciosPrestados.Recomendaciones", 1024)]
        public virtual String Recomendaciones { get; set; }

        [Column("Cantidad")]
        [DDisplayName("AdmisionesServiciosPrestados.Cantidad")]
        [DRequired("AdmisionesServiciosPrestados.Cantidad")]
        public virtual Int16 Cantidad { get; set; }

        [Column("Facturado")]
        [DDisplayName("AdmisionesServiciosPrestados.Facturado")]
        [DRequired("AdmisionesServiciosPrestados.Facturado")]
        public virtual Boolean Facturado { get; set; }

        [Column("PorcImpuesto")]
        [DDisplayName("AdmisionesServiciosPrestados.PorcImpuesto")]
        [DRequired("AdmisionesServiciosPrestados.PorcImpuesto")]
        public virtual Decimal PorcImpuesto { get; set; }

        [Column("LecturaRealizada")]
        [DDisplayName("AdmisionesServiciosPrestados.LecturaRealizada")]
        [DRequired("AdmisionesServiciosPrestados.LecturaRealizada")]
        public virtual Boolean LecturaRealizada { get; set; }

        #endregion

        #region Columnas referenciales)

        [Column("AdmisionesId")]
        [DDisplayName("AdmisionesServiciosPrestados.AdmisionesId")]
        [DRequired("AdmisionesServiciosPrestados.AdmisionesId")]
        [DRequiredFK("AdmisionesServiciosPrestados.AdmisionesId")]
        public virtual Int64 AdmisionesId { get; set; }

        [Column("ServiciosId")]
        [DDisplayName("AdmisionesServiciosPrestados.ServiciosId")]
        [DRequired("AdmisionesServiciosPrestados.ServiciosId")]
        [DRequiredFK("AdmisionesServiciosPrestados.ServiciosId")]
        public virtual Int64 ServiciosId { get; set; }

        [Column("AtencionesId")]
        [DDisplayName("AdmisionesServiciosPrestados.AtencionesId")]
        public virtual Int64? AtencionesId { get; set; }

        [Column("FacturasGeneracionId")]
        [DDisplayName("AdmisionesServiciosPrestados.FacturasGeneracionId")]
        public virtual Int64? FacturasGeneracionId { get; set; }

        [Column("FacturasId")]
        [DDisplayName("AdmisionesServiciosPrestados.FacturasId")]
        public virtual Int64? FacturasId { get; set; }

        #endregion

        #region Propiedades referencias de entrada)

        [ForeignKey("AdmisionesId")]
        public virtual Admisiones Admisiones { get; set; }

        [ForeignKey("AtencionesId")]
        public virtual Atenciones Atenciones { get; set; }

        [ForeignKey("FacturasId")]
        public virtual Facturas Facturas { get; set; }

        [ForeignKey("FacturasGeneracionId")]
        public virtual FacturasGeneracion FacturasGeneracion { get; set; }

        [ForeignKey("ServiciosId")]
        public virtual Servicios Servicios { get; set; }

        #endregion

        #region Reglas expression

        public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
        {
            Expression<Func<AdmisionesServiciosPrestados, bool>> expression = entity => entity.Id == this.Id;
            return expression as Expression<Func<T, bool>>;
        }

        public override List<ExpRecurso> GetAdicionarExpression<T>()
        {
            var rules = new List<ExpRecurso>();
            Expression<Func<AdmisionesServiciosPrestados, bool>> expression = null;

            return rules;
        }

        public override List<ExpRecurso> GetModificarExpression<T>()
        {
            var rules = new List<ExpRecurso>();
            Expression<Func<AdmisionesServiciosPrestados, bool>> expression = null;

            return rules;
        }

        public override List<ExpRecurso> GetEliminarExpression<T>()
        {
            var rules = new List<ExpRecurso>();
            Expression<Func<AdmisionesServiciosPrestadosArchivos, bool>> expression0 = entity => entity.AdmisionesServiciosPrestadosId == this.Id;
            rules.Add(new ExpRecurso(expression0.ToExpressionNode(), new Recurso("BLL.BUSINESS.DELETE_REL", "AdmisionesServiciosPrestadosArchivos"), typeof(AdmisionesServiciosPrestadosArchivos)));

            Expression<Func<AdmisionesServiciosPrestadosResultado, bool>> expression1 = entity => entity.AdmisionesServiciosPrestadosId == this.Id;
            rules.Add(new ExpRecurso(expression1.ToExpressionNode(), new Recurso("BLL.BUSINESS.DELETE_REL", "AdmisionesServiciosPrestadosResultado"), typeof(AdmisionesServiciosPrestadosResultado)));

            Expression<Func<AtencionesResultado, bool>> expression2 = entity => entity.AdmisionesServiciosPrestadosId == this.Id;
            rules.Add(new ExpRecurso(expression2.ToExpressionNode(), new Recurso("BLL.BUSINESS.DELETE_REL", "AtencionesResultado"), typeof(AtencionesResultado)));

            Expression<Func<ImagenesDiagnosticasDetalle, bool>> expression3 = entity => entity.AdmisionesServiciosPrestadosId == this.Id;
            rules.Add(new ExpRecurso(expression3.ToExpressionNode(), new Recurso("BLL.BUSINESS.DELETE_REL", "ImagenesDiagnosticasDetalle"), typeof(ImagenesDiagnosticasDetalle)));

            Expression<Func<LiquidacionHonorariosDetalle, bool>> expression4 = entity => entity.AdmisionesServiciosPrestadosId == this.Id;
            rules.Add(new ExpRecurso(expression4.ToExpressionNode(), new Recurso("BLL.BUSINESS.DELETE_REL", "LiquidacionHonorariosDetalle"), typeof(LiquidacionHonorariosDetalle)));

            return rules;
        }

        #endregion
    }
}
