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
    /// EmpresasResponsabilidadesFiscales object for mapped table EmpresasResponsabilidadesFiscales.
    /// </summary>
    [Table("EmpresasResponsabilidadesFiscales")]
    public partial class EmpresasResponsabilidadesFiscales : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("EmpresasId")]
       [DDisplayName("EmpresasResponsabilidadesFiscales.EmpresasId")]
       [DRequired("EmpresasResponsabilidadesFiscales.EmpresasId")]
       [DRequiredFK("EmpresasResponsabilidadesFiscales.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("ResponsabilidadesFiscalesId")]
       [DDisplayName("EmpresasResponsabilidadesFiscales.ResponsabilidadesFiscalesId")]
       [DRequired("EmpresasResponsabilidadesFiscales.ResponsabilidadesFiscalesId")]
       [DRequiredFK("EmpresasResponsabilidadesFiscales.ResponsabilidadesFiscalesId")]
       public virtual Int64 ResponsabilidadesFiscalesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("ResponsabilidadesFiscalesId")]
       public virtual ResponsabilidadesFiscales ResponsabilidadesFiscales { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<EmpresasResponsabilidadesFiscales, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EmpresasResponsabilidadesFiscales, bool>> expression = null;

        expression = entity => entity.EmpresasId == this.EmpresasId && entity.ResponsabilidadesFiscalesId == this.ResponsabilidadesFiscalesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "EmpresasResponsabilidadesFiscales.EmpresasId" , "EmpresasResponsabilidadesFiscales.ResponsabilidadesFiscalesId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EmpresasResponsabilidadesFiscales, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.EmpresasId == this.EmpresasId && entity.ResponsabilidadesFiscalesId == this.ResponsabilidadesFiscalesId)
                               && entity.EmpresasId == this.EmpresasId && entity.ResponsabilidadesFiscalesId == this.ResponsabilidadesFiscalesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "EmpresasResponsabilidadesFiscales.EmpresasId" , "EmpresasResponsabilidadesFiscales.ResponsabilidadesFiscalesId" )));

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
