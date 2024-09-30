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
    /// EntregaResultadosDetalles object for mapped table EntregaResultadosDetalles.
    /// </summary>
    [Table("EntregaResultadosDetalles")]
    public partial class EntregaResultadosDetalles : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("EntregaResultadosId")]
       [DDisplayName("EntregaResultadosDetalles.EntregaResultadosId")]
       [DRequired("EntregaResultadosDetalles.EntregaResultadosId")]
       [DRequiredFK("EntregaResultadosDetalles.EntregaResultadosId")]
       public virtual Int64 EntregaResultadosId { get; set; }

       [Column("AtencionesResultadoId")]
       [DDisplayName("EntregaResultadosDetalles.AtencionesResultadoId")]
       [DRequired("EntregaResultadosDetalles.AtencionesResultadoId")]
       [DRequiredFK("EntregaResultadosDetalles.AtencionesResultadoId")]
       public virtual Int64 AtencionesResultadoId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("AtencionesResultadoId")]
       public virtual AtencionesResultado AtencionesResultado { get; set; }

       [ForeignKey("EntregaResultadosId")]
       public virtual EntregaResultados EntregaResultados { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<EntregaResultadosDetalles, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EntregaResultadosDetalles, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EntregaResultadosDetalles, bool>> expression = null;

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
