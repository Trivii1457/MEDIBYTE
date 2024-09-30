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
    /// ModalidadesContratacion object for mapped table ModalidadesContratacion.
    /// </summary>
    [Table("ModalidadesContratacion")]
    public partial class ModalidadesContratacion : BaseEntity
    {

       #region Columnas normales)

       [Column("Descripcion")]
       [DDisplayName("ModalidadesContratacion.Descripcion")]
       [DRequired("ModalidadesContratacion.Descripcion")]
       [DStringLength("ModalidadesContratacion.Descripcion",1024)]
       public virtual String Descripcion { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ModalidadesContratacion, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ModalidadesContratacion, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ModalidadesContratacion, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Convenios, bool>> expression0 = entity => entity.ModalidadesContratacionId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Convenios"), typeof(Convenios)));

       return rules;
       }

       #endregion
    }
 }
