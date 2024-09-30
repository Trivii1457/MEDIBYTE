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
    /// FacturasGeneracion object for mapped table FacturasGeneracion.
    /// </summary>
    [Table("FacturasGeneracion")]
    public partial class FacturasGeneracion : BaseEntity
    {

       #region Columnas normales)

       [Column("FechaInicio", TypeName = "datetime")]
       [DDisplayName("FacturasGeneracion.FechaInicio")]
       [DRequired("FacturasGeneracion.FechaInicio")]
       public virtual DateTime FechaInicio { get; set; }

       [Column("FechaFinal", TypeName = "datetime")]
       [DDisplayName("FacturasGeneracion.FechaFinal")]
       [DRequired("FacturasGeneracion.FechaFinal")]
       public virtual DateTime FechaFinal { get; set; }

       [Column("OrdenCompra")]
       [DDisplayName("FacturasGeneracion.OrdenCompra")]
       [DStringLength("FacturasGeneracion.OrdenCompra",50)]
       public virtual String OrdenCompra { get; set; }

       [Column("ReferenciaFactura")]
       [DDisplayName("FacturasGeneracion.ReferenciaFactura")]
       [DStringLength("FacturasGeneracion.ReferenciaFactura",50)]
       public virtual String ReferenciaFactura { get; set; }

       [Column("ConveniosId")]
       [DDisplayName("FacturasGeneracion.ConveniosId")]
       [DRequired("FacturasGeneracion.ConveniosId")]
       public virtual Int64 ConveniosId { get; set; }

       [Column("Observaciones")]
       [DDisplayName("FacturasGeneracion.Observaciones")]
       [DStringLength("FacturasGeneracion.Observaciones",2000)]
       public virtual String Observaciones { get; set; }

       [Column("FilialesId")]
       [DDisplayName("FacturasGeneracion.FilialesId")]
       public virtual Int64? FilialesId { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("EmpresasId")]
       [DDisplayName("FacturasGeneracion.EmpresasId")]
       [DRequired("FacturasGeneracion.EmpresasId")]
       [DRequiredFK("FacturasGeneracion.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("EntidadesId")]
       [DDisplayName("FacturasGeneracion.EntidadesId")]
       [DRequired("FacturasGeneracion.EntidadesId")]
       [DRequiredFK("FacturasGeneracion.EntidadesId")]
       public virtual Int64 EntidadesId { get; set; }

       [Column("SedesId")]
       [DDisplayName("FacturasGeneracion.SedesId")]
       [DRequired("FacturasGeneracion.SedesId")]
       [DRequiredFK("FacturasGeneracion.SedesId")]
       public virtual Int64 SedesId { get; set; }

       [Column("PacientesId")]
       [DDisplayName("FacturasGeneracion.PacientesId")]
       public virtual Int64? PacientesId { get; set; }

       [Column("DocumentosId")]
       [DDisplayName("FacturasGeneracion.DocumentosId")]
       [DRequired("FacturasGeneracion.DocumentosId")]
       [DRequiredFK("FacturasGeneracion.DocumentosId")]
       public virtual Int64 DocumentosId { get; set; }

       [Column("MediosPagoId")]
       [DDisplayName("FacturasGeneracion.MediosPagoId")]
       public virtual Int64? MediosPagoId { get; set; }

       [Column("TiposRegimenesId")]
       [DDisplayName("FacturasGeneracion.TiposRegimenesId")]
       public virtual Int64? TiposRegimenesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("DocumentosId")]
       public virtual Documentos Documentos { get; set; }

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("EntidadesId")]
       public virtual Entidades Entidades { get; set; }

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
       Expression<Func<FacturasGeneracion, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<FacturasGeneracion, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<FacturasGeneracion, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdmisionesServiciosPrestados, bool>> expression0 = entity => entity.FacturasGeneracionId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdmisionesServiciosPrestados"), typeof(AdmisionesServiciosPrestados)));

       return rules;
       }

       #endregion
    }
 }
