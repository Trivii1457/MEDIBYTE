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
    /// IndicacionesMedicasDetalles object for mapped table IndicacionesMedicasDetalles.
    /// </summary>
    [Table("IndicacionesMedicasDetalles")]
    public partial class IndicacionesMedicasDetalles : BaseEntity
    {

       #region Columnas normales)

       [Column("Servicio")]
       [DDisplayName("IndicacionesMedicasDetalles.Servicio")]
       [DRequired("IndicacionesMedicasDetalles.Servicio")]
       [DStringLength("IndicacionesMedicasDetalles.Servicio",250)]
       public virtual String Servicio { get; set; }

       [Column("Cantidad")]
       [DDisplayName("IndicacionesMedicasDetalles.Cantidad")]
       [DRequired("IndicacionesMedicasDetalles.Cantidad")]
       public virtual Decimal Cantidad { get; set; }

       [Column("Observaciones")]
       [DDisplayName("IndicacionesMedicasDetalles.Observaciones")]
       public virtual String Observaciones { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("IndicacionesMedicasId")]
       [DDisplayName("IndicacionesMedicasDetalles.IndicacionesMedicasId")]
       [DRequired("IndicacionesMedicasDetalles.IndicacionesMedicasId")]
       [DRequiredFK("IndicacionesMedicasDetalles.IndicacionesMedicasId")]
       public virtual Int64 IndicacionesMedicasId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("IndicacionesMedicasId")]
       public virtual IndicacionesMedicas IndicacionesMedicas { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<IndicacionesMedicasDetalles, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<IndicacionesMedicasDetalles, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<IndicacionesMedicasDetalles, bool>> expression = null;

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
