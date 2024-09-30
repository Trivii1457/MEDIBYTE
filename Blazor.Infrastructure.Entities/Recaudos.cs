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
    /// Recaudos object for mapped table Recaudos.
    /// </summary>
    [Table("Recaudos")]
    public partial class Recaudos : BaseEntity
    {

       #region Columnas normales)

       [Column("FechaRecaudo", TypeName = "datetime")]
       [DDisplayName("Recaudos.FechaRecaudo")]
       [DRequired("Recaudos.FechaRecaudo")]
       public virtual DateTime FechaRecaudo { get; set; }

       [Column("ValorTotalRecibido")]
       [DDisplayName("Recaudos.ValorTotalRecibido")]
       [DRequired("Recaudos.ValorTotalRecibido")]
       public virtual Decimal ValorTotalRecibido { get; set; }

       [Column("NroCheque")]
       [DDisplayName("Recaudos.NroCheque")]
       [DStringLength("Recaudos.NroCheque",50)]
       public virtual String NroCheque { get; set; }

       [Column("NroCuentaTarjeta")]
       [DDisplayName("Recaudos.NroCuentaTarjeta")]
       [DStringLength("Recaudos.NroCuentaTarjeta",50)]
       public virtual String NroCuentaTarjeta { get; set; }

       [Column("NroAutorizacion")]
       [DDisplayName("Recaudos.NroAutorizacion")]
       [DStringLength("Recaudos.NroAutorizacion",50)]
       public virtual String NroAutorizacion { get; set; }

       [Column("Referencia")]
       [DDisplayName("Recaudos.Referencia")]
       [DStringLength("Recaudos.Referencia",50)]
       public virtual String Referencia { get; set; }

       [Column("FechaConsignacion", TypeName = "datetime")]
       [DDisplayName("Recaudos.FechaConsignacion")]
       public virtual DateTime? FechaConsignacion { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("CiclosCajasId")]
       [DDisplayName("Recaudos.CiclosCajasId")]
       [DRequired("Recaudos.CiclosCajasId")]
       [DRequiredFK("Recaudos.CiclosCajasId")]
       public virtual Int64 CiclosCajasId { get; set; }

       [Column("MediosPagoId")]
       [DDisplayName("Recaudos.MediosPagoId")]
       [DRequired("Recaudos.MediosPagoId")]
       [DRequiredFK("Recaudos.MediosPagoId")]
       public virtual Int64 MediosPagoId { get; set; }

       [Column("BancosId")]
       [DDisplayName("Recaudos.BancosId")]
       public virtual Int64? BancosId { get; set; }

       [Column("PacientesId")]
       [DDisplayName("Recaudos.PacientesId")]
       public virtual Int64? PacientesId { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("Recaudos.EmpresasId")]
       [DRequired("Recaudos.EmpresasId")]
       [DRequiredFK("Recaudos.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("EntidadesId")]
       [DDisplayName("Recaudos.EntidadesId")]
       public virtual Int64? EntidadesId { get; set; }

       [Column("SedesId")]
       [DDisplayName("Recaudos.SedesId")]
       [DRequired("Recaudos.SedesId")]
       [DRequiredFK("Recaudos.SedesId")]
       public virtual Int64 SedesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("BancosId")]
       public virtual Bancos Bancos { get; set; }

       [ForeignKey("CiclosCajasId")]
       public virtual CiclosCajas CiclosCajas { get; set; }

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

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Recaudos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Recaudos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Recaudos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<RecaudosDetalles, bool>> expression0 = entity => entity.RecaudosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","RecaudosDetalles"), typeof(RecaudosDetalles)));

       return rules;
       }

       #endregion
    }
 }
