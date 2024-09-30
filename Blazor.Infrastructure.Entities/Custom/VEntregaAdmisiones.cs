using Dominus.Backend.Data;
using Dominus.Backend.DataBase;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    [Table("VEntregaAdmisionesReporte")]
    public class VEntregaAdmisiones : BaseEntity
    {
        [Column("ADMISION_ID")]
        public long ADMISION_ID { get; set; }

        [Column("ADMISION_ESTADO")]
        public string ADMISION_ESTADO { get; set; }

        [Column("USUARIO_CREO")]
        public string USUARIO_CREO { get; set; }

        [Column("ENTIDAD_NOMBRE")]
        public string ENTIDAD_NOMBRE { get; set; }

        [Column("CONVENIO_NOMBRE")]
        public string CONVENIO_NOMBRE { get; set; }

        [Column("FILIAL_NOMBRE")]
        public string FILIAL_NOMBRE { get; set; }

        [Column("TIPO_USUARIO")]
        public string TIPO_USUARIO { get; set; }

        [Column("PACIENTE_TIPO_IDENTIFICACION")]
        public string PACIENTE_TIPO_IDENTIFICACION { get; set; }

        [Column("PACIENTE_NUMERO_IDENTIFICACION")]
        public string PACIENTE_NUMERO_IDENTIFICACION { get; set; }

        [Column("PACIENTE_NOMBRES")]
        public string PACIENTE_NOMBRES { get; set; }

        [Column("NUMERO_AUTORIZACION")]
        public string NUMERO_AUTORIZACION { get; set; }

        [Column("FECHA_AUTORIZACION")]
        public DateTime? FECHA_AUTORIZACION { get; set; }

        [Column("FACTURA_USUARIO_CREO")]
        public string FACTURA_USUARIO_CREO { get; set; }

        [Column("FACTURA_DOCUMENTO")]
        public string FACTURA_DOCUMENTO { get; set; }

        [Column("CAJA_NOMBRE")]
        public string CAJA_NOMBRE { get; set; }

        [Column("CICLO_CAJA_ID")]
        public long? CICLO_CAJA_ID { get; set; }

        [Column("RECAUDO_VALOR_APLICADO")]
        public decimal RECAUDO_VALOR_APLICADO { get; set; }

        [Column("COP_MOD_REC_COM")]
        public decimal COP_MOD_REC_COM { get; set; }

        [Column("COPAGO")]
        public decimal COPAGO { get; set; }

        [Column("CUOTA_MODERADORA")]
        public decimal CUOTA_MODERADORA { get; set; }

        [Column("CUOTA_RECUPERACION")]
        public decimal CUOTA_RECUPERACION { get; set; }

        [Column("PAGO_COMPARTIDO")]
        public decimal PAGO_COMPARTIDO { get; set; }

        [Column("PAGO_PARTICULAR")]
        public decimal PAGO_PARTICULAR { get; set; }

        [Column("PORC_DESCUENTO")]
        public decimal PORC_DESCUENTO { get; set; }

        [Column("VALOR_DESCUENTO")]
        public decimal VALOR_DESCUENTO { get; set; }

        [Column("VALOR_ENTIDAD")]
        public decimal VALOR_ENTIDAD { get; set; }

        [Column("CUPS")]
        public string CUPS { get; set; }

        [Column("SERVICIO_NOMBRE")]
        public string SERVICIO_NOMBRE { get; set; }

        [Column("SERVICIO_CANTIDAD")]
        public short SERVICIO_CANTIDAD { get; set; }

        [Column("SERVICIO_VALOR")]
        public decimal SERVICIO_VALOR { get; set; }

        [Column("SERVICIOS_VALOR_TOTAL")]
        public decimal SERVICIOS_VALOR_TOTAL { get; set; }

        [Column("ADMISION_FECHA_CREACION")]
        public DateTime ADMISION_FECHA_CREACION { get; set; }

        [Column("SEDE_ID")]
        public long SEDE_ID { get; set; }

        [Column("SEDE_NOMBRE")]
        public string SEDE_NOMBRE { get; set; }

    }
}

