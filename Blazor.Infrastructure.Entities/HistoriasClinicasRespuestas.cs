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
    /// HistoriasClinicasRespuestas object for mapped table HistoriasClinicasRespuestas.
    /// </summary>
    [Table("HistoriasClinicasRespuestas")]
    public partial class HistoriasClinicasRespuestas : BaseEntity
    {

       #region Columnas normales)

       [Column("RespuestaAbierta")]
       [DDisplayName("HistoriasClinicasRespuestas.RespuestaAbierta")]
       public virtual String RespuestaAbierta { get; set; }

       [Column("RespuestaSeleccion")]
       [DDisplayName("HistoriasClinicasRespuestas.RespuestaSeleccion")]
       [DStringLength("HistoriasClinicasRespuestas.RespuestaSeleccion",50)]
       public virtual String RespuestaSeleccion { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("HIstoriasClinicasId")]
       [DDisplayName("HistoriasClinicasRespuestas.HIstoriasClinicasId")]
       [DRequired("HistoriasClinicasRespuestas.HIstoriasClinicasId")]
       [DRequiredFK("HistoriasClinicasRespuestas.HIstoriasClinicasId")]
       public virtual Int64 HIstoriasClinicasId { get; set; }

       [Column("HCRespuestasId")]
       [DDisplayName("HistoriasClinicasRespuestas.HCRespuestasId")]
       [DRequired("HistoriasClinicasRespuestas.HCRespuestasId")]
       [DRequiredFK("HistoriasClinicasRespuestas.HCRespuestasId")]
       public virtual Int64 HCRespuestasId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("HCRespuestasId")]
       public virtual HCRespuestas HCRespuestas { get; set; }

       [ForeignKey("HIstoriasClinicasId")]
       public virtual HistoriasClinicas HIstoriasClinicas { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<HistoriasClinicasRespuestas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HistoriasClinicasRespuestas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HistoriasClinicasRespuestas, bool>> expression = null;

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
