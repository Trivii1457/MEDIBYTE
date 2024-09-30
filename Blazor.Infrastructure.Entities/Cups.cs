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
    /// Cups object for mapped table Cups.
    /// </summary>
    [Table("Cups")]
    public partial class Cups : BaseEntity
    {

       #region Columnas normales)

       [Column("Codigo")]
       [DDisplayName("Cups.Codigo")]
       [DRequired("Cups.Codigo")]
       [DStringLength("Cups.Codigo",45)]
       public virtual String Codigo { get; set; }

       [Column("Descripcion")]
       [DDisplayName("Cups.Descripcion")]
       [DRequired("Cups.Descripcion")]
       [DStringLength("Cups.Descripcion",500)]
       public virtual String Descripcion { get; set; }

       [Column("PBS")]
       [DDisplayName("Cups.PBS")]
       [DRequired("Cups.PBS")]
       public virtual Boolean PBS { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Cups, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Cups, bool>> expression = null;

        expression = entity => entity.Codigo == this.Codigo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Cups.Codigo" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Cups, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Codigo == this.Codigo)
                               && entity.Codigo == this.Codigo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Cups.Codigo" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesServiciosDetalles, bool>> expression0 = entity => entity.CupsId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesServiciosDetalles"), typeof(OrdenesServiciosDetalles)));

        Expression<Func<Servicios, bool>> expression1 = entity => entity.CupsId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Servicios"), typeof(Servicios)));

       return rules;
       }

       #endregion
    }
 }
