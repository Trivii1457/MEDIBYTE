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
    /// CategoriasIngresosDetalles object for mapped table CategoriasIngresosDetalles.
    /// </summary>
    [Table("CategoriasIngresosDetalles")]
    public partial class CategoriasIngresosDetalles : BaseEntity
    {

       #region Columnas normales)

       [Column("FechaInicial", TypeName = "datetime")]
       [DDisplayName("CategoriasIngresosDetalles.FechaInicial")]
       [DRequired("CategoriasIngresosDetalles.FechaInicial")]
       public virtual DateTime FechaInicial { get; set; }

       [Column("FechaFinal", TypeName = "datetime")]
       [DDisplayName("CategoriasIngresosDetalles.FechaFinal")]
       [DRequired("CategoriasIngresosDetalles.FechaFinal")]
       public virtual DateTime FechaFinal { get; set; }

       [Column("PorcentajeCopago")]
       [DDisplayName("CategoriasIngresosDetalles.PorcentajeCopago")]
       [DRequired("CategoriasIngresosDetalles.PorcentajeCopago")]
       public virtual Decimal PorcentajeCopago { get; set; }

       [Column("CopagoMaximoEvento")]
       [DDisplayName("CategoriasIngresosDetalles.CopagoMaximoEvento")]
       [DRequired("CategoriasIngresosDetalles.CopagoMaximoEvento")]
       public virtual Decimal CopagoMaximoEvento { get; set; }

       [Column("CopagoMaximoAno")]
       [DDisplayName("CategoriasIngresosDetalles.CopagoMaximoAno")]
       [DRequired("CategoriasIngresosDetalles.CopagoMaximoAno")]
       public virtual Decimal CopagoMaximoAno { get; set; }

       [Column("CuotaModeradora")]
       [DDisplayName("CategoriasIngresosDetalles.CuotaModeradora")]
       [DRequired("CategoriasIngresosDetalles.CuotaModeradora")]
       public virtual Decimal CuotaModeradora { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("CategoriasIngresosId")]
       [DDisplayName("CategoriasIngresosDetalles.CategoriasIngresosId")]
       [DRequired("CategoriasIngresosDetalles.CategoriasIngresosId")]
       [DRequiredFK("CategoriasIngresosDetalles.CategoriasIngresosId")]
       public virtual Int64 CategoriasIngresosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("CategoriasIngresosId")]
       public virtual CategoriasIngresos CategoriasIngresos { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<CategoriasIngresosDetalles, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CategoriasIngresosDetalles, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CategoriasIngresosDetalles, bool>> expression = null;

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
