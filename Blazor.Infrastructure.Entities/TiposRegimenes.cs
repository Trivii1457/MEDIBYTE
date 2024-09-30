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
    /// TiposRegimenes object for mapped table TiposRegimenes.
    /// </summary>
    [Table("TiposRegimenes")]
    public partial class TiposRegimenes : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("TiposRegimenes.Nombre")]
       [DRequired("TiposRegimenes.Nombre")]
       [DStringLength("TiposRegimenes.Nombre",255)]
       public virtual String Nombre { get; set; }

       [Column("Codigo")]
       [DDisplayName("TiposRegimenes.Codigo")]
       [DRequired("TiposRegimenes.Codigo")]
       [DStringLength("TiposRegimenes.Codigo",5)]
       public virtual String Codigo { get; set; }

       [Column("CodigoRips")]
       [DDisplayName("TiposRegimenes.CodigoRips")]
       [DStringLength("TiposRegimenes.CodigoRips",6)]
       public virtual String CodigoRips { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<TiposRegimenes, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposRegimenes, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<TiposRegimenes, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Pacientes, bool>> expression0 = entity => entity.TiposRegimenesId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Pacientes"), typeof(Pacientes)));

       return rules;
       }

       #endregion
    }
 }
