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
    /// CategoriasServicios object for mapped table CategoriasServicios.
    /// </summary>
    [Table("CategoriasServicios")]
    public partial class CategoriasServicios : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("CategoriasServicios.Nombre")]
       [DRequired("CategoriasServicios.Nombre")]
       [DStringLength("CategoriasServicios.Nombre",250)]
       public virtual String Nombre { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("CategoriasServicios.EmpresasId")]
       [DRequired("CategoriasServicios.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<CategoriasServicios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CategoriasServicios, bool>> expression = null;

        expression = entity => entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "CategoriasServicios.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CategoriasServicios, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Nombre == this.Nombre)
                               && entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "CategoriasServicios.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Servicios, bool>> expression0 = entity => entity.CategoriasServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Servicios"), typeof(Servicios)));

       return rules;
       }

       #endregion
    }
 }
