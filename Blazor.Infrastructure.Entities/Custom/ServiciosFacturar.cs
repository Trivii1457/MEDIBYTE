using Dominus.Backend.Data;
using Dominus.Backend.DataBase;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    [Table("VServiciosFacturados")]
    public class ServiciosFacturar : BaseEntity
    {
        [Column("AdmisionesId")]
        [DDisplayName("ServiciosFacturar.AdmisionesId")]
        [DRequired("ServiciosFacturar.AdmisionesId")]
        public Int64 AdmisionesId { get; set; }

        [Column("EmpresasId")]
        [DDisplayName("ServiciosFacturar.EmpresasId")]
        [DRequired("ServiciosFacturar.EmpresasId")]
        public Int64 EmpresasId { get; set; }

        [Column("EmpresasRazonSocial")]
        [DDisplayName("ServiciosFacturar.EmpresasRazonSocial")]
        [DStringLength("ServiciosFacturar.EmpresasRazonSocial", 150)]
        public string EmpresasRazonSocial { get; set; }

        [Column("EntidadesId")]
        [DDisplayName("ServiciosFacturar.EntidadesId")]
        public Int64? EntidadesId { get; set; }

        [Column("EntidadesNombre")]
        [DDisplayName("ServiciosFacturar.EntidadesNombre")]
        [DStringLength("ServiciosFacturar.EntidadesNombre", 250)]
        public string EntidadesNombre { get; set; }

        [Column("SedesId")]
        [DDisplayName("ServiciosFacturar.SedesId")]
        [DRequired("ServiciosFacturar.SedesId")]
        public Int64 SedesId { get; set; }

        [Column("SedesNombre")]
        [DDisplayName("ServiciosFacturar.SedesNombre")]
        [DStringLength("ServiciosFacturar.SedesNombre", 250)]
        public string SedesNombre { get; set; }

        [Column("ConvenioCodigo")]
        [DDisplayName("ServiciosFacturar.ConvenioCodigo")]
        [DStringLength("ServiciosFacturar.ConvenioCodigo", 100)]
        public string ConvenioCodigo { get; set; }


        [Column("ConvenioFechaInicio", TypeName = "datetime")]
        [DDisplayName("ServiciosFacturar.ConvenioFechaInicio")]
        public DateTime? ConvenioFechaInicio { get; set; }

        [Column("ConvenioFechaTerminacion", TypeName = "datetime")]
        [DDisplayName("ServiciosFacturar.ConvenioFechaTerminacion")]
        public DateTime? ConvenioFechaTerminacion { get; set; }

        [Column("ConvenioEstadosId")]
        [DDisplayName("ServiciosFacturar.ConvenioEstadosId")]
        public long? ConvenioEstadosId { get; set; }

        [Column("ConveniosEstadosNombre")]
        [DDisplayName("ServiciosFacturar.ConveniosEstadosNombre")]
        [DStringLength("ServiciosFacturar.ConveniosEstadosNombre", 100)]
        public string ConveniosEstadosNombre { get; set; }

        [Column("ServiciosId")]
        [DDisplayName("ServiciosFacturar.ServiciosId")]
        [DRequired("ServiciosFacturar.ServiciosId")]
        public Int64? ServiciosId { get; set; }

        [Column("ServiciosCup")]
        [DDisplayName("ServiciosFacturar.ServiciosCup")]
        [DStringLength("ServiciosFacturar.ServiciosCup", 45)]
        public string ServiciosCup { get; set; }

        [Column("ServiciosNombre")]
        [DDisplayName("ServiciosFacturar.ServiciosNombre")]
        [DStringLength("ServiciosFacturar.ServiciosNombre", 500)]
        public string ServiciosNombre { get; set; }

        [Column("Cantidad")]
        [DDisplayName("ServiciosFacturar.Cantidad")]
        [DRequired("ServiciosFacturar.Cantidad")]
        public short Cantidad { get; set; }

        [Column("ValorServicio")]
        [DDisplayName("ServiciosFacturar.ValorServicio")]
        [DRequired("ServiciosFacturar.ValorServicio")]
        public decimal ValorServicio { get; set; }


        [Column("Facturado")]
        [DDisplayName("ServiciosFacturar.Facturado")]
        [DRequired("ServiciosFacturar.Facturado")]
        public bool Facturado { get; set; }

        [Column("PacientesId")]
        [DDisplayName("ServiciosFacturar.PacientesId")]
        [DRequired("ServiciosFacturar.PacientesId")]
        public Int64? PacientesId { get; set; }

        [Column("PacientesNombres")]
        [DDisplayName("ServiciosFacturar.PacientesNombres")]
        [DStringLength("ServiciosFacturar.PacientesNombres", 100)]
        public string PacientesNombres { get; set; }

        [Column("PacienteNumeroIdentificacion")]
        [DDisplayName("ServiciosFacturar.PacienteNumeroIdentificacion")]
        [DStringLength("ServiciosFacturar.PacienteNumeroIdentificacion", 100)]
        public string PacienteNumeroIdentificacion { get; set; }

        [Column("FechaAtencion")]
        [DDisplayName("ServiciosFacturar.FechaAtencion")]
        public DateTime? FechaAtencion { get; set; }

        [Column("FacturasGeneracionId")]
        [DDisplayName("ServiciosFacturar.FacturasGeneracionId")]
        public virtual Int64? FacturasGeneracionId { get; set; }

        [Column("FacturasId")]
        [DDisplayName("ServiciosFacturar.FacturasId")]
        public virtual Int64? FacturasId { get; set; }

        [Column("ConvenioId")]
        [DDisplayName("ServiciosFacturar.ConvenioId")]
        public virtual Int64? ConvenioId { get; set; }

        [Column("ValorDescuento")]
        [DDisplayName("ServiciosFacturar.ValorDescuento")]
        public virtual decimal ValorDescuento { get; set; }

        [Column("ValorImpuesto")]
        [DDisplayName("ServiciosFacturar.ValorImpuesto")]
        public virtual decimal ValorImpuesto { get; set; }

        [Column("PorcDescAutorizado")]
        [DDisplayName("ServiciosFacturar.PorcDescAutorizado")]
        public virtual decimal PorcDescAutorizado { get; set; }

        [Column("ValorCopagoCuotaModeradora")]
        [DDisplayName("ServiciosFacturar.ValorCopagoCuotaModeradora")]
        public virtual decimal ValorCopagoCuotaModeradora { get; set; }

        [Column("CodigoImpuesto")]
        [DDisplayName("ServiciosFacturar.PorcentajeImpuesto")]
        public string CodigoImpuesto { get; set; }

        [Column("DescripcionImpuesto")]
        [DDisplayName("ServiciosFacturar.PorcentajeImpuesto")]
        public string DescripcionImpuesto { get; set; }

        [Column("PorcImpuesto")]
        [DDisplayName("ServiciosFacturar.PorcImpuesto")]
        public virtual decimal PorcImpuesto { get; set; }

        [Column("FormaPagoId")]
        [DDisplayName("ServiciosFacturar.FormaPagoId")]
        public virtual Int64? FormaPagoId { get; set; }

        [Column("AdmisionFacturada")]
        [DDisplayName("ServiciosFacturar.AdmisionFacturada")]
        public virtual bool AdmisionFacturada { get; set; }


        [Column("FilialesId")]
        [DDisplayName("ServiciosFacturar.FilialesId")]
        public virtual Int64 FilialesId { get; set; }

        [Column("TiposRegimenesId")]
        [DDisplayName("ServiciosFacturar.TiposRegimenesId")]
        public virtual Int64 TiposRegimenesId { get; set; }

        [Column("AtencionEstado")]
        [DDisplayName("ServiciosFacturar.AtencionEstado")]
        public virtual Int64 AtencionEstado { get; set; }

        [NotMapped]
        public decimal SubTotal
        {
            get
            {
                return ValorServicio * Cantidad;
            }
            private set { }
        }

        [NotMapped]
        public decimal Total
        {
            get
            {
                return ((ValorServicio * Cantidad) - ValorDescuento + ValorImpuesto);
            }
            private set { }
        }
    }

    public class TotalesPorTipoImpuesto
    {
        public string CodigoImpuesto { get; set; }

        public string DescripcionImpuesto { get; set; }

        public decimal PorcImpuesto { get; set; }

        public decimal ValorServicios { get; set; }

        public decimal Cantidad { get; set; }

        public decimal ValorDescuento { get; set; }

        public decimal ValorImpuesto { get; set; }


    }
}

