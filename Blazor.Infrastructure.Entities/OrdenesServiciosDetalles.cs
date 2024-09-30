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
    /// OrdenesServiciosDetalles object for mapped table OrdenesServiciosDetalles.
    /// </summary>
    [Table("OrdenesServiciosDetalles")]
    public partial class OrdenesServiciosDetalles : BaseEntity
    {

       #region Columnas normales)

       [Column("Cantidad")]
       [DDisplayName("OrdenesServiciosDetalles.Cantidad")]
       [DRequired("OrdenesServiciosDetalles.Cantidad")]
       public virtual Decimal Cantidad { get; set; }

       [Column("Observaciones")]
       [DDisplayName("OrdenesServiciosDetalles.Observaciones")]
       public virtual String Observaciones { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("OrdenesServiciosId")]
       [DDisplayName("OrdenesServiciosDetalles.OrdenesServiciosId")]
       public virtual Int64? OrdenesServiciosId { get; set; }

       [Column("CupsId")]
       [DDisplayName("OrdenesServiciosDetalles.CupsId")]
       [DRequired("OrdenesServiciosDetalles.CupsId")]
       [DRequiredFK("OrdenesServiciosDetalles.CupsId")]
       public virtual Int64 CupsId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("CupsId")]
       public virtual Cups Cups { get; set; }

       [ForeignKey("OrdenesServiciosId")]
       public virtual OrdenesServicios OrdenesServicios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<OrdenesServiciosDetalles, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesServiciosDetalles, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesServiciosDetalles, bool>> expression = null;

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
