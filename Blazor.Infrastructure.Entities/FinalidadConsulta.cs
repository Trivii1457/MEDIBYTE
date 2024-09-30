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
    /// FinalidadConsulta object for mapped table FinalidadConsulta.
    /// </summary>
    [Table("FinalidadConsulta")]
    public partial class FinalidadConsulta : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("FinalidadConsulta.Nombre")]
       [DRequired("FinalidadConsulta.Nombre")]
       [DStringLength("FinalidadConsulta.Nombre",100)]
       public virtual String Nombre { get; set; }

       [Column("CodigoRips")]
       [DDisplayName("FinalidadConsulta.CodigoRips")]
       [DRequired("FinalidadConsulta.CodigoRips")]
       [DStringLength("FinalidadConsulta.CodigoRips",2)]
       public virtual String CodigoRips { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<FinalidadConsulta, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<FinalidadConsulta, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<FinalidadConsulta, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Atenciones, bool>> expression0 = entity => entity.FinalidadConsultaId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Atenciones"), typeof(Atenciones)));

       return rules;
       }

       #endregion
    }
 }
