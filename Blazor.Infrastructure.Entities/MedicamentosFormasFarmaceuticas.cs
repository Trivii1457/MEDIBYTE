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
    /// MedicamentosFormasFarmaceuticas object for mapped table MedicamentosFormasFarmaceuticas.
    /// </summary>
    [Table("MedicamentosFormasFarmaceuticas")]
    public partial class MedicamentosFormasFarmaceuticas : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("MedicamentosFormasFarmaceuticas.Nombre")]
       [DRequired("MedicamentosFormasFarmaceuticas.Nombre")]
       [DStringLength("MedicamentosFormasFarmaceuticas.Nombre",250)]
       public virtual String Nombre { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<MedicamentosFormasFarmaceuticas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<MedicamentosFormasFarmaceuticas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<MedicamentosFormasFarmaceuticas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Medicamentos, bool>> expression0 = entity => entity.FormasFarmaceuticasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Medicamentos"), typeof(Medicamentos)));

       return rules;
       }

       #endregion
    }
 }
