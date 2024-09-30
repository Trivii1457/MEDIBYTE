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
    /// Cajas object for mapped table Cajas.
    /// </summary>
    [Table("Cajas")]
    public partial class Cajas : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("Cajas.Nombre")]
       [DRequired("Cajas.Nombre")]
       [DStringLength("Cajas.Nombre",150)]
       public virtual String Nombre { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("SedesId")]
       [DDisplayName("Cajas.SedesId")]
       [DRequired("Cajas.SedesId")]
       [DRequiredFK("Cajas.SedesId")]
       public virtual Int64 SedesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("SedesId")]
       public virtual Sedes Sedes { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Cajas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Cajas, bool>> expression = null;

        expression = entity => entity.SedesId == this.SedesId && entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Cajas.SedesId" , "Cajas.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Cajas, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.SedesId == this.SedesId && entity.Nombre == this.Nombre)
                               && entity.SedesId == this.SedesId && entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Cajas.SedesId" , "Cajas.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CiclosCajas, bool>> expression0 = entity => entity.CajasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","CiclosCajas"), typeof(CiclosCajas)));

       return rules;
       }

       #endregion
    }
 }
