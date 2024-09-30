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
    /// OrdenesMedicamentosDetalles object for mapped table OrdenesMedicamentosDetalles.
    /// </summary>
    [Table("OrdenesMedicamentosDetalles")]
    public partial class OrdenesMedicamentosDetalles : BaseEntity
    {

       #region Columnas normales)

       [Column("Dosis")]
       [DDisplayName("OrdenesMedicamentosDetalles.Dosis")]
       [DRequired("OrdenesMedicamentosDetalles.Dosis")]
       [DStringLength("OrdenesMedicamentosDetalles.Dosis",50)]
       public virtual String Dosis { get; set; }

       [Column("Frecuencia")]
       [DDisplayName("OrdenesMedicamentosDetalles.Frecuencia")]
       [DRequired("OrdenesMedicamentosDetalles.Frecuencia")]
       [DStringLength("OrdenesMedicamentosDetalles.Frecuencia",50)]
       public virtual String Frecuencia { get; set; }

       [Column("DiasTratamiento")]
       [DDisplayName("OrdenesMedicamentosDetalles.DiasTratamiento")]
       [DRequired("OrdenesMedicamentosDetalles.DiasTratamiento")]
       public virtual Int16 DiasTratamiento { get; set; }

       [Column("Cantidad")]
       [DDisplayName("OrdenesMedicamentosDetalles.Cantidad")]
       [DRequired("OrdenesMedicamentosDetalles.Cantidad")]
       public virtual Decimal Cantidad { get; set; }

       [Column("Observaciones")]
       [DDisplayName("OrdenesMedicamentosDetalles.Observaciones")]
       public virtual String Observaciones { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("MedicamentosId")]
       [DDisplayName("OrdenesMedicamentosDetalles.MedicamentosId")]
       [DRequired("OrdenesMedicamentosDetalles.MedicamentosId")]
       [DRequiredFK("OrdenesMedicamentosDetalles.MedicamentosId")]
       public virtual Int64 MedicamentosId { get; set; }

       [Column("OrdenesMedicamentosId")]
       [DDisplayName("OrdenesMedicamentosDetalles.OrdenesMedicamentosId")]
       [DRequired("OrdenesMedicamentosDetalles.OrdenesMedicamentosId")]
       [DRequiredFK("OrdenesMedicamentosDetalles.OrdenesMedicamentosId")]
       public virtual Int64 OrdenesMedicamentosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("MedicamentosId")]
       public virtual Medicamentos Medicamentos { get; set; }

       [ForeignKey("OrdenesMedicamentosId")]
       public virtual OrdenesMedicamentos OrdenesMedicamentos { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<OrdenesMedicamentosDetalles, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesMedicamentosDetalles, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesMedicamentosDetalles, bool>> expression = null;

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
