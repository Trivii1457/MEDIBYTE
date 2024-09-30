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
    /// AdministracionHonorariosServicios object for mapped table AdministracionHonorariosServicios.
    /// </summary>
    [Table("AdministracionHonorariosServicios")]
    public partial class AdministracionHonorariosServicios : BaseEntity
    {

       #region Columnas normales)

       [Column("ValorHonorarioConvenio")]
       [DDisplayName("AdministracionHonorariosServicios.ValorHonorarioConvenio")]
       [DRequired("AdministracionHonorariosServicios.ValorHonorarioConvenio")]
       public virtual Decimal ValorHonorarioConvenio { get; set; }

       [Column("ValorHonorarioParticular")]
       [DDisplayName("AdministracionHonorariosServicios.ValorHonorarioParticular")]
       [DRequired("AdministracionHonorariosServicios.ValorHonorarioParticular")]
       public virtual Decimal ValorHonorarioParticular { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("AdministracionHonorariosId")]
       [DDisplayName("AdministracionHonorariosServicios.AdministracionHonorariosId")]
       [DRequired("AdministracionHonorariosServicios.AdministracionHonorariosId")]
       [DRequiredFK("AdministracionHonorariosServicios.AdministracionHonorariosId")]
       public virtual Int64 AdministracionHonorariosId { get; set; }

       [Column("ServiciosId")]
       [DDisplayName("AdministracionHonorariosServicios.ServiciosId")]
       [DRequired("AdministracionHonorariosServicios.ServiciosId")]
       [DRequiredFK("AdministracionHonorariosServicios.ServiciosId")]
       public virtual Int64 ServiciosId { get; set; }

       [Column("TipoPagoConvenioEstadosId")]
       [DDisplayName("AdministracionHonorariosServicios.TipoPagoConvenioEstadosId")]
       [DRequired("AdministracionHonorariosServicios.TipoPagoConvenioEstadosId")]
       [DRequiredFK("AdministracionHonorariosServicios.TipoPagoConvenioEstadosId")]
       public virtual Int64 TipoPagoConvenioEstadosId { get; set; }

       [Column("TipoPagoParticularEstadosId")]
       [DDisplayName("AdministracionHonorariosServicios.TipoPagoParticularEstadosId")]
       [DRequired("AdministracionHonorariosServicios.TipoPagoParticularEstadosId")]
       [DRequiredFK("AdministracionHonorariosServicios.TipoPagoParticularEstadosId")]
       public virtual Int64 TipoPagoParticularEstadosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("AdministracionHonorariosId")]
       public virtual AdministracionHonorarios AdministracionHonorarios { get; set; }

       [ForeignKey("ServiciosId")]
       public virtual Servicios Servicios { get; set; }

       [ForeignKey("TipoPagoConvenioEstadosId")]
       public virtual Estados TipoPagoConvenioEstados { get; set; }

       [ForeignKey("TipoPagoParticularEstadosId")]
       public virtual Estados TipoPagoParticularEstados { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<AdministracionHonorariosServicios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdministracionHonorariosServicios, bool>> expression = null;

        expression = entity => entity.AdministracionHonorariosId == this.AdministracionHonorariosId && entity.ServiciosId == this.ServiciosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "AdministracionHonorariosServicios.AdministracionHonorariosId" , "AdministracionHonorariosServicios.ServiciosId" )));

        expression = entity => entity.AdministracionHonorariosId == this.AdministracionHonorariosId && entity.ServiciosId == this.ServiciosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "AdministracionHonorariosServicios.AdministracionHonorariosId" , "AdministracionHonorariosServicios.ServiciosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdministracionHonorariosServicios, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.AdministracionHonorariosId == this.AdministracionHonorariosId && entity.ServiciosId == this.ServiciosId)
                               && entity.AdministracionHonorariosId == this.AdministracionHonorariosId && entity.ServiciosId == this.ServiciosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "AdministracionHonorariosServicios.AdministracionHonorariosId" , "AdministracionHonorariosServicios.ServiciosId" )));

        expression = entity => !(entity.Id == this.Id && entity.AdministracionHonorariosId == this.AdministracionHonorariosId && entity.ServiciosId == this.ServiciosId)
                               && entity.AdministracionHonorariosId == this.AdministracionHonorariosId && entity.ServiciosId == this.ServiciosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "AdministracionHonorariosServicios.AdministracionHonorariosId" , "AdministracionHonorariosServicios.ServiciosId" )));

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
