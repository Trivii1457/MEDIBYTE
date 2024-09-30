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
    /// Glosas object for mapped table Glosas.
    /// </summary>
    [Table("Glosas")]
    public partial class Glosas : BaseEntity
    {

       #region Columnas normales)

       [Column("ValorGlosado")]
       [DDisplayName("Glosas.ValorGlosado")]
       [DRequired("Glosas.ValorGlosado")]
       public virtual Decimal ValorGlosado { get; set; }

       [Column("ValorAceptado")]
       [DDisplayName("Glosas.ValorAceptado")]
       [DRequired("Glosas.ValorAceptado")]
       public virtual Decimal ValorAceptado { get; set; }

       [Column("ValorGlosadoFinal")]
       [DDisplayName("Glosas.ValorGlosadoFinal")]
       [DRequired("Glosas.ValorGlosadoFinal")]
       public virtual Decimal ValorGlosadoFinal { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("FacturasId")]
       [DDisplayName("Glosas.FacturasId")]
       [DRequired("Glosas.FacturasId")]
       [DRequiredFK("Glosas.FacturasId")]
       public virtual Int64 FacturasId { get; set; }

       [Column("CausalesGlosasId")]
       [DDisplayName("Glosas.CausalesGlosasId")]
       [DRequired("Glosas.CausalesGlosasId")]
       [DRequiredFK("Glosas.CausalesGlosasId")]
       public virtual Int64 CausalesGlosasId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("CausalesGlosasId")]
       public virtual CausalesGlosas CausalesGlosas { get; set; }

       [ForeignKey("FacturasId")]
       public virtual Facturas Facturas { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Glosas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Glosas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Glosas, bool>> expression = null;

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
