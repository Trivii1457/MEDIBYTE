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
    /// IncapacidadesOrigenes object for mapped table IncapacidadesOrigenes.
    /// </summary>
    [Table("IncapacidadesOrigenes")]
    public partial class IncapacidadesOrigenes : BaseEntity
    {

       #region Columnas normales)

       [Column("Descripcion")]
       [DDisplayName("IncapacidadesOrigenes.Descripcion")]
       [DRequired("IncapacidadesOrigenes.Descripcion")]
       [DStringLength("IncapacidadesOrigenes.Descripcion",255)]
       public virtual String Descripcion { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<IncapacidadesOrigenes, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<IncapacidadesOrigenes, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<IncapacidadesOrigenes, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Incapacidades, bool>> expression0 = entity => entity.IncapacidadesOrigenesId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Incapacidades"), typeof(Incapacidades)));

       return rules;
       }

       #endregion
    }
 }
