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
    /// Medicamentos object for mapped table Medicamentos.
    /// </summary>
    [Table("Medicamentos")]
    public partial class Medicamentos : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("Medicamentos.Nombre")]
       [DRequired("Medicamentos.Nombre")]
       [DStringLength("Medicamentos.Nombre",250)]
       public virtual String Nombre { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("ConcentracionesId")]
       [DDisplayName("Medicamentos.ConcentracionesId")]
       [DRequired("Medicamentos.ConcentracionesId")]
       [DRequiredFK("Medicamentos.ConcentracionesId")]
       public virtual Int64 ConcentracionesId { get; set; }

       [Column("FormasFarmaceuticasId")]
       [DDisplayName("Medicamentos.FormasFarmaceuticasId")]
       [DRequired("Medicamentos.FormasFarmaceuticasId")]
       [DRequiredFK("Medicamentos.FormasFarmaceuticasId")]
       public virtual Int64 FormasFarmaceuticasId { get; set; }

       [Column("ViaAdministracionId")]
       [DDisplayName("Medicamentos.ViaAdministracionId")]
       [DRequired("Medicamentos.ViaAdministracionId")]
       [DRequiredFK("Medicamentos.ViaAdministracionId")]
       public virtual Int64 ViaAdministracionId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("ConcentracionesId")]
       public virtual MedicamentosConcentraciones Concentraciones { get; set; }

       [ForeignKey("FormasFarmaceuticasId")]
       public virtual MedicamentosFormasFarmaceuticas FormasFarmaceuticas { get; set; }

       [ForeignKey("ViaAdministracionId")]
       public virtual MedicamentosViaAdministracion ViaAdministracion { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Medicamentos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Medicamentos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Medicamentos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesMedicamentosDetalles, bool>> expression0 = entity => entity.MedicamentosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesMedicamentosDetalles"), typeof(OrdenesMedicamentosDetalles)));

       return rules;
       }

       #endregion
    }
 }
