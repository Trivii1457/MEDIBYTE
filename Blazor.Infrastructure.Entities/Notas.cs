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
    /// Notas object for mapped table Notas.
    /// </summary>
    [Table("Notas")]
    public partial class Notas : BaseEntity
    {

       #region Columnas normales)

       [Column("Fecha", TypeName = "datetime")]
       [DDisplayName("Notas.Fecha")]
       [DRequired("Notas.Fecha")]
       public virtual DateTime Fecha { get; set; }

       [Column("Consecutivo")]
       [DDisplayName("Notas.Consecutivo")]
       [DRequired("Notas.Consecutivo")]
       public virtual Int64 Consecutivo { get; set; }

       [Column("PeriodoInicial", TypeName = "datetime")]
       [DDisplayName("Notas.PeriodoInicial")]
       public virtual DateTime? PeriodoInicial { get; set; }

       [Column("PeriodoFinal", TypeName = "datetime")]
       [DDisplayName("Notas.PeriodoFinal")]
       public virtual DateTime? PeriodoFinal { get; set; }

       [Column("Referencia")]
       [DDisplayName("Notas.Referencia")]
       [DStringLength("Notas.Referencia",50)]
       public virtual String Referencia { get; set; }

       [Column("OrdenCompra")]
       [DDisplayName("Notas.OrdenCompra")]
       [DStringLength("Notas.OrdenCompra",50)]
       public virtual String OrdenCompra { get; set; }

       [Column("Saldo")]
       [DDisplayName("Notas.Saldo")]
       [DRequired("Notas.Saldo")]
       public virtual Decimal Saldo { get; set; }

       [Column("ValorSubtotal")]
       [DDisplayName("Notas.ValorSubtotal")]
       [DRequired("Notas.ValorSubtotal")]
       public virtual Decimal ValorSubtotal { get; set; }

       [Column("ValorDescuentos")]
       [DDisplayName("Notas.ValorDescuentos")]
       [DRequired("Notas.ValorDescuentos")]
       public virtual Decimal ValorDescuentos { get; set; }

       [Column("ValorImpuestos")]
       [DDisplayName("Notas.ValorImpuestos")]
       [DRequired("Notas.ValorImpuestos")]
       public virtual Decimal ValorImpuestos { get; set; }

       [Column("ValorTotal")]
       [DDisplayName("Notas.ValorTotal")]
       [DRequired("Notas.ValorTotal")]
       public virtual Decimal ValorTotal { get; set; }

       [Column("MontoEscrito")]
       [DDisplayName("Notas.MontoEscrito")]
       [DStringLength("Notas.MontoEscrito",1024)]
       public virtual String MontoEscrito { get; set; }

       [Column("CUFE")]
       [DDisplayName("Notas.CUFE")]
       [DStringLength("Notas.CUFE",255)]
       public virtual String CUFE { get; set; }

       [Column("IssueDate", TypeName = "datetime")]
       [DDisplayName("Notas.IssueDate")]
       public virtual DateTime? IssueDate { get; set; }

       [Column("DIANResponse")]
       [DDisplayName("Notas.DIANResponse")]
       [DStringLength("Notas.DIANResponse",1024)]
       public virtual String DIANResponse { get; set; }

       [Column("XmlUrl")]
       [DDisplayName("Notas.XmlUrl")]
       [DStringLength("Notas.XmlUrl",2048)]
       public virtual String XmlUrl { get; set; }

       [Column("ErrorReference")]
       [DDisplayName("Notas.ErrorReference")]
       public virtual String ErrorReference { get; set; }

       [Column("UrlTracking")]
       [DDisplayName("Notas.UrlTracking")]
       [DStringLength("Notas.UrlTracking",2048)]
       public virtual String UrlTracking { get; set; }

       [Column("IdDbusiness")]
       [DDisplayName("Notas.IdDbusiness")]
       [DStringLength("Notas.IdDbusiness",50)]
       public virtual String IdDbusiness { get; set; }

       [Column("Observaciones")]
       [DDisplayName("Notas.Observaciones")]
       [DStringLength("Notas.Observaciones",2000)]
       public virtual String Observaciones { get; set; }

       [Column("FechaVencimiento", TypeName = "datetime")]
       [DDisplayName("Notas.FechaVencimiento")]
       [DRequired("Notas.FechaVencimiento")]
       public virtual DateTime FechaVencimiento { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("DocumentosId")]
       [DDisplayName("Notas.DocumentosId")]
       [DRequired("Notas.DocumentosId")]
       [DRequiredFK("Notas.DocumentosId")]
       public virtual Int64 DocumentosId { get; set; }

       [Column("SedesId")]
       [DDisplayName("Notas.SedesId")]
       [DRequired("Notas.SedesId")]
       [DRequiredFK("Notas.SedesId")]
       public virtual Int64 SedesId { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("Notas.EmpresasId")]
       [DRequired("Notas.EmpresasId")]
       [DRequiredFK("Notas.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("EntidadesId")]
       [DDisplayName("Notas.EntidadesId")]
       public virtual Int64? EntidadesId { get; set; }

       [Column("PacientesId")]
       [DDisplayName("Notas.PacientesId")]
       public virtual Int64? PacientesId { get; set; }

       [Column("FacturasId")]
       [DDisplayName("Notas.FacturasId")]
       public virtual Int64? FacturasId { get; set; }

       [Column("Estadosid")]
       [DDisplayName("Notas.Estadosid")]
       [DRequired("Notas.Estadosid")]
       [DRequiredFK("Notas.Estadosid")]
       public virtual Int64 Estadosid { get; set; }

       [Column("NotasConceptosId")]
       [DDisplayName("Notas.NotasConceptosId")]
       [DRequired("Notas.NotasConceptosId")]
       [DRequiredFK("Notas.NotasConceptosId")]
       public virtual Int64 NotasConceptosId { get; set; }

       [Column("FormasPagosId")]
       [DDisplayName("Notas.FormasPagosId")]
       [DRequired("Notas.FormasPagosId")]
       [DRequiredFK("Notas.FormasPagosId")]
       public virtual Int64 FormasPagosId { get; set; }

       [Column("MediosPagoId")]
       [DDisplayName("Notas.MediosPagoId")]
       [DRequired("Notas.MediosPagoId")]
       [DRequiredFK("Notas.MediosPagoId")]
       public virtual Int64 MediosPagoId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("DocumentosId")]
       public virtual Documentos Documentos { get; set; }

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("EntidadesId")]
       public virtual Entidades Entidades { get; set; }

       [ForeignKey("Estadosid")]
       public virtual Estados Estados { get; set; }

       [ForeignKey("FacturasId")]
       public virtual Facturas Facturas { get; set; }

       [ForeignKey("FormasPagosId")]
       public virtual FormasPagos FormasPagos { get; set; }

       [ForeignKey("MediosPagoId")]
       public virtual MediosPago MediosPago { get; set; }

       [ForeignKey("NotasConceptosId")]
       public virtual NotasConceptos NotasConceptos { get; set; }

       [ForeignKey("PacientesId")]
       public virtual Pacientes Pacientes { get; set; }

       [ForeignKey("SedesId")]
       public virtual Sedes Sedes { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Notas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Notas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Notas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<NotasDetalles, bool>> expression0 = entity => entity.NotasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","NotasDetalles"), typeof(NotasDetalles)));

       return rules;
       }

       #endregion
    }
 }
