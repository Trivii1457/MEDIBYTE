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
    /// Facturas object for mapped table Facturas.
    /// </summary>
    [Table("Facturas")]
    public partial class Facturas : BaseEntity
    {

       #region Columnas normales)

       [Column("Fecha", TypeName = "datetime")]
       [DDisplayName("Facturas.Fecha")]
       [DRequired("Facturas.Fecha")]
       public virtual DateTime Fecha { get; set; }

       [Column("NroConsecutivo")]
       [DDisplayName("Facturas.NroConsecutivo")]
       [DRequired("Facturas.NroConsecutivo")]
       public virtual Int64 NroConsecutivo { get; set; }

       [Column("ValorCopagoCuotaModeradora")]
       [DDisplayName("Facturas.ValorCopagoCuotaModeradora")]
       [DRequired("Facturas.ValorCopagoCuotaModeradora")]
       public virtual Decimal ValorCopagoCuotaModeradora { get; set; }

       [Column("ValorSubtotal")]
       [DDisplayName("Facturas.ValorSubtotal")]
       [DRequired("Facturas.ValorSubtotal")]
       public virtual Decimal ValorSubtotal { get; set; }

       [Column("ValorDescuentos")]
       [DDisplayName("Facturas.ValorDescuentos")]
       [DRequired("Facturas.ValorDescuentos")]
       public virtual Decimal ValorDescuentos { get; set; }

       [Column("ValorImpuestos")]
       [DDisplayName("Facturas.ValorImpuestos")]
       [DRequired("Facturas.ValorImpuestos")]
       public virtual Decimal ValorImpuestos { get; set; }

       [Column("ValorTotal")]
       [DDisplayName("Facturas.ValorTotal")]
       [DRequired("Facturas.ValorTotal")]
       public virtual Decimal ValorTotal { get; set; }

       [Column("FehaInicial", TypeName = "datetime")]
       [DDisplayName("Facturas.FehaInicial")]
       [DRequired("Facturas.FehaInicial")]
       public virtual DateTime FehaInicial { get; set; }

       [Column("FechaFinal", TypeName = "datetime")]
       [DDisplayName("Facturas.FechaFinal")]
       [DRequired("Facturas.FechaFinal")]
       public virtual DateTime FechaFinal { get; set; }

       [Column("Saldo")]
       [DDisplayName("Facturas.Saldo")]
       [DRequired("Facturas.Saldo")]
       public virtual Decimal Saldo { get; set; }

       [Column("OrdenCompra")]
       [DDisplayName("Facturas.OrdenCompra")]
       [DStringLength("Facturas.OrdenCompra",50)]
       public virtual String OrdenCompra { get; set; }

       [Column("ReferenciaFactura")]
       [DDisplayName("Facturas.ReferenciaFactura")]
       [DStringLength("Facturas.ReferenciaFactura",50)]
       public virtual String ReferenciaFactura { get; set; }

       [Column("MontoEscrito")]
       [DDisplayName("Facturas.MontoEscrito")]
       [DRequired("Facturas.MontoEscrito")]
       [DStringLength("Facturas.MontoEscrito",1024)]
       public virtual String MontoEscrito { get; set; }

       [Column("CUFE")]
       [DDisplayName("Facturas.CUFE")]
       [DStringLength("Facturas.CUFE",255)]
       public virtual String CUFE { get; set; }

       [Column("IssueDate", TypeName = "datetime")]
       [DDisplayName("Facturas.IssueDate")]
       public virtual DateTime? IssueDate { get; set; }

       [Column("DIANResponse")]
       [DDisplayName("Facturas.DIANResponse")]
       [DStringLength("Facturas.DIANResponse",1024)]
       public virtual String DIANResponse { get; set; }

       [Column("XmlUrl")]
       [DDisplayName("Facturas.XmlUrl")]
       [DStringLength("Facturas.XmlUrl",2048)]
       public virtual String XmlUrl { get; set; }

       [Column("ErrorReference")]
       [DDisplayName("Facturas.ErrorReference")]
       public virtual String ErrorReference { get; set; }

       [Column("UrlTracking")]
       [DDisplayName("Facturas.UrlTracking")]
       [DStringLength("Facturas.UrlTracking",2048)]
       public virtual String UrlTracking { get; set; }

       [Column("IdDbusiness")]
       [DDisplayName("Facturas.IdDbusiness")]
       [DStringLength("Facturas.IdDbusiness",50)]
       public virtual String IdDbusiness { get; set; }

       [Column("EsCopagoModeradora")]
       [DDisplayName("Facturas.EsCopagoModeradora")]
       [DRequired("Facturas.EsCopagoModeradora")]
       public virtual Boolean EsCopagoModeradora { get; set; }

       [Column("Observaciones")]
       [DDisplayName("Facturas.Observaciones")]
       [DStringLength("Facturas.Observaciones",2000)]
       public virtual String Observaciones { get; set; }

       [Column("TieneNotas")]
       [DDisplayName("Facturas.TieneNotas")]
       [DRequired("Facturas.TieneNotas")]
       public virtual Boolean TieneNotas { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("SedesId")]
       [DDisplayName("Facturas.SedesId")]
       [DRequired("Facturas.SedesId")]
       [DRequiredFK("Facturas.SedesId")]
       public virtual Int64 SedesId { get; set; }

       [Column("EntidadesId")]
       [DDisplayName("Facturas.EntidadesId")]
       public virtual Int64? EntidadesId { get; set; }

       [Column("ConvenioId")]
       [DDisplayName("Facturas.ConvenioId")]
       public virtual Int64? ConvenioId { get; set; }

       [Column("Estadosid")]
       [DDisplayName("Facturas.Estadosid")]
       [DRequired("Facturas.Estadosid")]
       [DRequiredFK("Facturas.Estadosid")]
       public virtual Int64 Estadosid { get; set; }

       [Column("FilialesId")]
       [DDisplayName("Facturas.FilialesId")]
       public virtual Int64? FilialesId { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("Facturas.EmpresasId")]
       [DRequired("Facturas.EmpresasId")]
       [DRequiredFK("Facturas.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("PacientesId")]
       [DDisplayName("Facturas.PacientesId")]
       public virtual Int64? PacientesId { get; set; }

       [Column("DocumentosId")]
       [DDisplayName("Facturas.DocumentosId")]
       [DRequired("Facturas.DocumentosId")]
       [DRequiredFK("Facturas.DocumentosId")]
       public virtual Int64 DocumentosId { get; set; }

       [Column("FormasPagosId")]
       [DDisplayName("Facturas.FormasPagosId")]
       [DRequired("Facturas.FormasPagosId")]
       [DRequiredFK("Facturas.FormasPagosId")]
       public virtual Int64 FormasPagosId { get; set; }

       [Column("MediosPagoId")]
       [DDisplayName("Facturas.MediosPagoId")]
       public virtual Int64? MediosPagoId { get; set; }

       [Column("TiposRegimenesId")]
       [DDisplayName("Facturas.TiposRegimenesId")]
       public virtual Int64? TiposRegimenesId { get; set; }

       [Column("AdmisionesId")]
       [DDisplayName("Facturas.AdmisionesId")]
       public virtual Int64? AdmisionesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("AdmisionesId")]
       public virtual Admisiones Admisiones { get; set; }

       [ForeignKey("ConvenioId")]
       public virtual Convenios Convenio { get; set; }

       [ForeignKey("DocumentosId")]
       public virtual Documentos Documentos { get; set; }

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("EntidadesId")]
       public virtual Entidades Entidades { get; set; }

       [ForeignKey("Estadosid")]
       public virtual Estados Estados { get; set; }

       [ForeignKey("FilialesId")]
       public virtual Filiales Filiales { get; set; }

       [ForeignKey("FormasPagosId")]
       public virtual FormasPagos FormasPagos { get; set; }

       [ForeignKey("MediosPagoId")]
       public virtual MediosPago MediosPago { get; set; }

       [ForeignKey("PacientesId")]
       public virtual Pacientes Pacientes { get; set; }

       [ForeignKey("SedesId")]
       public virtual Sedes Sedes { get; set; }

       [ForeignKey("TiposRegimenesId")]
       public virtual TiposRegimenes TiposRegimenes { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Facturas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Facturas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Facturas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Admisiones, bool>> expression0 = entity => entity.FacturaCopagoCuotaModeradoraId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<AdmisionesServiciosPrestados, bool>> expression1 = entity => entity.FacturasId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdmisionesServiciosPrestados"), typeof(AdmisionesServiciosPrestados)));

        Expression<Func<FacturasDetalles, bool>> expression2 = entity => entity.FacturasId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","FacturasDetalles"), typeof(FacturasDetalles)));

        Expression<Func<Glosas, bool>> expression3 = entity => entity.FacturasId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Glosas"), typeof(Glosas)));

        Expression<Func<Notas, bool>> expression4 = entity => entity.FacturasId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Notas"), typeof(Notas)));

        Expression<Func<RadicacionCuentasDetalles, bool>> expression5 = entity => entity.FacurasId == this.Id;
        rules.Add(new ExpRecurso(expression5.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","RadicacionCuentasDetalles"), typeof(RadicacionCuentasDetalles)));

        Expression<Func<RecaudosDetalles, bool>> expression6 = entity => entity.FacturasId == this.Id;
        rules.Add(new ExpRecurso(expression6.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","RecaudosDetalles"), typeof(RecaudosDetalles)));

       return rules;
       }

       #endregion
    }
 }
