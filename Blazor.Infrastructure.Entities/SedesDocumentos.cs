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
    /// SedesDocumentos object for mapped table SedesDocumentos.
    /// </summary>
    [Table("SedesDocumentos")]
    public partial class SedesDocumentos : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("SedesId")]
       [DDisplayName("SedesDocumentos.SedesId")]
       [DRequired("SedesDocumentos.SedesId")]
       [DRequiredFK("SedesDocumentos.SedesId")]
       public virtual Int64 SedesId { get; set; }

       [Column("DocumentosId")]
       [DDisplayName("SedesDocumentos.DocumentosId")]
       [DRequired("SedesDocumentos.DocumentosId")]
       [DRequiredFK("SedesDocumentos.DocumentosId")]
       public virtual Int64 DocumentosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("DocumentosId")]
       public virtual Documentos Documentos { get; set; }

       [ForeignKey("SedesId")]
       public virtual Sedes Sedes { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<SedesDocumentos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<SedesDocumentos, bool>> expression = null;

        expression = entity => entity.DocumentosId == this.DocumentosId && entity.SedesId == this.SedesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "SedesDocumentos.DocumentosId" , "SedesDocumentos.SedesId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<SedesDocumentos, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.DocumentosId == this.DocumentosId && entity.SedesId == this.SedesId)
                               && entity.DocumentosId == this.DocumentosId && entity.SedesId == this.SedesId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "SedesDocumentos.DocumentosId" , "SedesDocumentos.SedesId" )));

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
