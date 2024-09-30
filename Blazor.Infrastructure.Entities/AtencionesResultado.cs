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
    /// AtencionesResultado object for mapped table AtencionesResultado.
    /// </summary>
    [Table("AtencionesResultado")]
    public partial class AtencionesResultado : BaseEntity
    {

       #region Columnas normales)

       [Column("Resultado")]
       [DDisplayName("AtencionesResultado.Resultado")]
       public virtual String Resultado { get; set; }

       [Column("ResultadoAudio")]
       [DDisplayName("AtencionesResultado.ResultadoAudio")]
       public virtual Byte[] ResultadoAudio { get; set; }

       [Column("Entregado")]
       [DDisplayName("AtencionesResultado.Entregado")]
       [DRequired("AtencionesResultado.Entregado")]
       public virtual Boolean Entregado { get; set; }

       [Column("FechaLectura", TypeName = "datetime")]
       [DDisplayName("AtencionesResultado.FechaLectura")]
       [DRequired("AtencionesResultado.FechaLectura")]
       public virtual DateTime FechaLectura { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("EstadosId")]
       [DDisplayName("AtencionesResultado.EstadosId")]
       [DRequired("AtencionesResultado.EstadosId")]
       [DRequiredFK("AtencionesResultado.EstadosId")]
       public virtual Int64 EstadosId { get; set; }

       [Column("AdmisionesServiciosPrestadosId")]
       [DDisplayName("AtencionesResultado.AdmisionesServiciosPrestadosId")]
       [DRequired("AtencionesResultado.AdmisionesServiciosPrestadosId")]
       [DRequiredFK("AtencionesResultado.AdmisionesServiciosPrestadosId")]
       public virtual Int64 AdmisionesServiciosPrestadosId { get; set; }

       [Column("EmpleadoId")]
       [DDisplayName("AtencionesResultado.EmpleadoId")]
       public virtual Int64? EmpleadoId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("AdmisionesServiciosPrestadosId")]
       public virtual AdmisionesServiciosPrestados AdmisionesServiciosPrestados { get; set; }

       [ForeignKey("EmpleadoId")]
       public virtual Empleados Empleado { get; set; }

       [ForeignKey("EstadosId")]
       public virtual Estados Estados { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<AtencionesResultado, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AtencionesResultado, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AtencionesResultado, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EntregaResultadosDetalles, bool>> expression0 = entity => entity.AtencionesResultadoId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EntregaResultadosDetalles"), typeof(EntregaResultadosDetalles)));

        Expression<Func<LiquidacionHonorariosDetalle, bool>> expression1 = entity => entity.AtencionesResultadoId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","LiquidacionHonorariosDetalle"), typeof(LiquidacionHonorariosDetalle)));

       return rules;
       }

       #endregion
    }
 }
