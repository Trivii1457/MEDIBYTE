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
    /// ResponsabilidadesFiscales object for mapped table ResponsabilidadesFiscales.
    /// </summary>
    [Table("ResponsabilidadesFiscales")]
    public partial class ResponsabilidadesFiscales : BaseEntity
    {

       #region Columnas normales)

       [Column("Codigo")]
       [DDisplayName("ResponsabilidadesFiscales.Codigo")]
       [DRequired("ResponsabilidadesFiscales.Codigo")]
       [DStringLength("ResponsabilidadesFiscales.Codigo",10)]
       public virtual String Codigo { get; set; }

       [Column("Descripcion")]
       [DDisplayName("ResponsabilidadesFiscales.Descripcion")]
       [DRequired("ResponsabilidadesFiscales.Descripcion")]
       [DStringLength("ResponsabilidadesFiscales.Descripcion",255)]
       public virtual String Descripcion { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ResponsabilidadesFiscales, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ResponsabilidadesFiscales, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ResponsabilidadesFiscales, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EmpresasResponsabilidadesFiscales, bool>> expression0 = entity => entity.ResponsabilidadesFiscalesId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EmpresasResponsabilidadesFiscales"), typeof(EmpresasResponsabilidadesFiscales)));

        Expression<Func<EntidadesResponsabilidadesFiscales, bool>> expression1 = entity => entity.ResponsabilidadesFiscalesId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EntidadesResponsabilidadesFiscales"), typeof(EntidadesResponsabilidadesFiscales)));

       return rules;
       }

       #endregion
    }
 }
