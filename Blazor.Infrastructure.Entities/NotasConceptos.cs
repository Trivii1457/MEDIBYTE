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
    /// NotasConceptos object for mapped table NotasConceptos.
    /// </summary>
    [Table("NotasConceptos")]
    public partial class NotasConceptos : BaseEntity
    {

       #region Columnas normales)

       [Column("Codigo")]
       [DDisplayName("NotasConceptos.Codigo")]
       [DRequired("NotasConceptos.Codigo")]
       [DStringLength("NotasConceptos.Codigo",10)]
       public virtual String Codigo { get; set; }

       [Column("Descripcion")]
       [DDisplayName("NotasConceptos.Descripcion")]
       [DRequired("NotasConceptos.Descripcion")]
       [DStringLength("NotasConceptos.Descripcion",255)]
       public virtual String Descripcion { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("DocumentosId")]
       [DDisplayName("NotasConceptos.DocumentosId")]
       [DRequired("NotasConceptos.DocumentosId")]
       [DRequiredFK("NotasConceptos.DocumentosId")]
       public virtual Int64 DocumentosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("DocumentosId")]
       public virtual Documentos Documentos { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<NotasConceptos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<NotasConceptos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<NotasConceptos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Notas, bool>> expression0 = entity => entity.NotasConceptosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Notas"), typeof(Notas)));

       return rules;
       }

       #endregion
    }
 }
