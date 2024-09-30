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
    /// AdministracionHonorariosLecturas object for mapped table AdministracionHonorariosLecturas.
    /// </summary>
    [Table("AdministracionHonorariosLecturas")]
    public partial class AdministracionHonorariosLecturas : BaseEntity
    {

       #region Columnas normales)

       [Column("ValorHonorarioLectura")]
       [DDisplayName("AdministracionHonorariosLecturas.ValorHonorarioLectura")]
       [DRequired("AdministracionHonorariosLecturas.ValorHonorarioLectura")]
       public virtual Decimal ValorHonorarioLectura { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("AdministracionHonorariosId")]
       [DDisplayName("AdministracionHonorariosLecturas.AdministracionHonorariosId")]
       [DRequired("AdministracionHonorariosLecturas.AdministracionHonorariosId")]
       [DRequiredFK("AdministracionHonorariosLecturas.AdministracionHonorariosId")]
       public virtual Int64 AdministracionHonorariosId { get; set; }

       [Column("ServiciosId")]
       [DDisplayName("AdministracionHonorariosLecturas.ServiciosId")]
       [DRequired("AdministracionHonorariosLecturas.ServiciosId")]
       [DRequiredFK("AdministracionHonorariosLecturas.ServiciosId")]
       public virtual Int64 ServiciosId { get; set; }

       [Column("TipoPagoLecturaEstadosId")]
       [DDisplayName("AdministracionHonorariosLecturas.TipoPagoLecturaEstadosId")]
       [DRequired("AdministracionHonorariosLecturas.TipoPagoLecturaEstadosId")]
       [DRequiredFK("AdministracionHonorariosLecturas.TipoPagoLecturaEstadosId")]
       public virtual Int64 TipoPagoLecturaEstadosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("AdministracionHonorariosId")]
       public virtual AdministracionHonorarios AdministracionHonorarios { get; set; }

       [ForeignKey("ServiciosId")]
       public virtual Servicios Servicios { get; set; }

       [ForeignKey("TipoPagoLecturaEstadosId")]
       public virtual Estados TipoPagoLecturaEstados { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<AdministracionHonorariosLecturas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdministracionHonorariosLecturas, bool>> expression = null;

        expression = entity => entity.AdministracionHonorariosId == this.AdministracionHonorariosId && entity.ServiciosId == this.ServiciosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "AdministracionHonorariosLecturas.AdministracionHonorariosId" , "AdministracionHonorariosLecturas.ServiciosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdministracionHonorariosLecturas, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.AdministracionHonorariosId == this.AdministracionHonorariosId && entity.ServiciosId == this.ServiciosId)
                               && entity.AdministracionHonorariosId == this.AdministracionHonorariosId && entity.ServiciosId == this.ServiciosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "AdministracionHonorariosLecturas.AdministracionHonorariosId" , "AdministracionHonorariosLecturas.ServiciosId" )));

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
