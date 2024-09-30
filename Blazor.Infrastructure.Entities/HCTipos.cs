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
    /// HCTipos object for mapped table HCTipos.
    /// </summary>
    [Table("HCTipos")]
    public partial class HCTipos : BaseEntity
    {

       #region Columnas normales)

       [Column("Descripcion")]
       [DDisplayName("HCTipos.Descripcion")]
       [DRequired("HCTipos.Descripcion")]
       [DStringLength("HCTipos.Descripcion",255)]
       public virtual String Descripcion { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("EspecialidadesId")]
       [DDisplayName("HCTipos.EspecialidadesId")]
       [DRequired("HCTipos.EspecialidadesId")]
       [DRequiredFK("HCTipos.EspecialidadesId")]
       public virtual Int64 EspecialidadesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EspecialidadesId")]
       public virtual Especialidades Especialidades { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<HCTipos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HCTipos, bool>> expression = null;

        expression = entity => entity.EspecialidadesId == this.EspecialidadesId && entity.Descripcion == this.Descripcion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "HCTipos.EspecialidadesId" , "HCTipos.Descripcion" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HCTipos, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.EspecialidadesId == this.EspecialidadesId && entity.Descripcion == this.Descripcion)
                               && entity.EspecialidadesId == this.EspecialidadesId && entity.Descripcion == this.Descripcion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "HCTipos.EspecialidadesId" , "HCTipos.Descripcion" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HCTiposPreguntas, bool>> expression0 = entity => entity.HCTiposId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HCTiposPreguntas"), typeof(HCTiposPreguntas)));

        Expression<Func<HistoriasClinicas, bool>> expression1 = entity => entity.HCTiposId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicas"), typeof(HistoriasClinicas)));

       return rules;
       }

       #endregion
    }
 }
