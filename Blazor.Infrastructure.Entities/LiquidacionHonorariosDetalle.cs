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
    /// LiquidacionHonorariosDetalle object for mapped table LiquidacionHonorariosDetalle.
    /// </summary>
    [Table("LiquidacionHonorariosDetalle")]
    public partial class LiquidacionHonorariosDetalle : BaseEntity
    {

       #region Columnas normales)

       [Column("Valor")]
       [DDisplayName("LiquidacionHonorariosDetalle.Valor")]
       [DRequired("LiquidacionHonorariosDetalle.Valor")]
       public virtual Decimal Valor { get; set; }

       [Column("EsLectura")]
       [DDisplayName("LiquidacionHonorariosDetalle.EsLectura")]
       [DRequired("LiquidacionHonorariosDetalle.EsLectura")]
       public virtual Boolean EsLectura { get; set; }

        #endregion

        #region Columnas referenciales)

        [Column("LiquidacionHonorariosId")]
       [DDisplayName("LiquidacionHonorariosDetalle.LiquidacionHonorariosId")]
       [DRequired("LiquidacionHonorariosDetalle.LiquidacionHonorariosId")]
       [DRequiredFK("LiquidacionHonorariosDetalle.LiquidacionHonorariosId")]
       public virtual Int64 LiquidacionHonorariosId { get; set; }

       [Column("EmpleadosId")]
       [DDisplayName("LiquidacionHonorariosDetalle.EmpleadosId")]
       [DRequired("LiquidacionHonorariosDetalle.EmpleadosId")]
       [DRequiredFK("LiquidacionHonorariosDetalle.EmpleadosId")]
       public virtual Int64 EmpleadosId { get; set; }

       [Column("AdmisionesServiciosPrestadosId")]
       [DDisplayName("LiquidacionHonorariosDetalle.AdmisionesServiciosPrestadosId")]
       [DRequired("LiquidacionHonorariosDetalle.AdmisionesServiciosPrestadosId")]
       [DRequiredFK("LiquidacionHonorariosDetalle.AdmisionesServiciosPrestadosId")]
       public virtual Int64 AdmisionesServiciosPrestadosId { get; set; }

       [Column("AtencionesResultadoId")]
       [DDisplayName("LiquidacionHonorariosDetalle.AtencionesResultadoId")]
       public virtual Int64? AtencionesResultadoId { get; set; }

        #endregion

        #region Propiedades referencias de entrada)

        [ForeignKey("AdmisionesServiciosPrestadosId")]
       public virtual AdmisionesServiciosPrestados AdmisionesServiciosPrestados { get; set; }

        [ForeignKey("AtencionesResultadoId")]
       public virtual AtencionesResultado AtencionesResultado { get; set; }

       [ForeignKey("EmpleadosId")]
       public virtual Empleados Empleados { get; set; }

       [ForeignKey("LiquidacionHonorariosId")]
       public virtual LiquidacionHonorarios LiquidacionHonorarios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<LiquidacionHonorariosDetalle, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<LiquidacionHonorariosDetalle, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<LiquidacionHonorariosDetalle, bool>> expression = null;

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

