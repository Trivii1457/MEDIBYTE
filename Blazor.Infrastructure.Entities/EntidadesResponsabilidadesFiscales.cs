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
    /// EntidadesResponsabilidadesFiscales object for mapped table EntidadesResponsabilidadesFiscales.
    /// </summary>
    [Table("EntidadesResponsabilidadesFiscales")]
    public partial class EntidadesResponsabilidadesFiscales : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("EntidadesId")]
       [DDisplayName("EntidadesResponsabilidadesFiscales.EntidadesId")]
       [DRequired("EntidadesResponsabilidadesFiscales.EntidadesId")]
       [DRequiredFK("EntidadesResponsabilidadesFiscales.EntidadesId")]
       public virtual Int64 EntidadesId { get; set; }

       [Column("ResponsabilidadesFiscalesId")]
       [DDisplayName("EntidadesResponsabilidadesFiscales.ResponsabilidadesFiscalesId")]
       [DRequired("EntidadesResponsabilidadesFiscales.ResponsabilidadesFiscalesId")]
       [DRequiredFK("EntidadesResponsabilidadesFiscales.ResponsabilidadesFiscalesId")]
       public virtual Int64 ResponsabilidadesFiscalesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EntidadesId")]
       public virtual Entidades Entidades { get; set; }

       [ForeignKey("ResponsabilidadesFiscalesId")]
       public virtual ResponsabilidadesFiscales ResponsabilidadesFiscales { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<EntidadesResponsabilidadesFiscales, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EntidadesResponsabilidadesFiscales, bool>> expression = null;

        expression = entity => entity.EntidadesId == this.EntidadesId && entity.ResponsabilidadesFiscalesId == this.ResponsabilidadesFiscalesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "EntidadesResponsabilidadesFiscales.EntidadesId" , "EntidadesResponsabilidadesFiscales.ResponsabilidadesFiscalesId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EntidadesResponsabilidadesFiscales, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.EntidadesId == this.EntidadesId && entity.ResponsabilidadesFiscalesId == this.ResponsabilidadesFiscalesId)
                               && entity.EntidadesId == this.EntidadesId && entity.ResponsabilidadesFiscalesId == this.ResponsabilidadesFiscalesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "EntidadesResponsabilidadesFiscales.EntidadesId" , "EntidadesResponsabilidadesFiscales.ResponsabilidadesFiscalesId" )));

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
