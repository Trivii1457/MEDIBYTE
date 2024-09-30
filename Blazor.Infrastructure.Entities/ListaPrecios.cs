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
    /// ListaPrecios object for mapped table ListaPrecios.
    /// </summary>
    [Table("ListaPrecios")]
    public partial class ListaPrecios : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("ListaPrecios.Nombre")]
       [DRequired("ListaPrecios.Nombre")]
       [DStringLength("ListaPrecios.Nombre",255)]
       public virtual String Nombre { get; set; }

       [Column("Porcentaje")]
       [DDisplayName("ListaPrecios.Porcentaje")]
       [DRequired("ListaPrecios.Porcentaje")]
       public virtual Decimal Porcentaje { get; set; }

       [Column("Valor")]
       [DDisplayName("ListaPrecios.Valor")]
       [DRequired("ListaPrecios.Valor")]
       public virtual Decimal Valor { get; set; }

       [Column("Tarifa")]
       [DDisplayName("ListaPrecios.Tarifa")]
       [DStringLength("ListaPrecios.Tarifa",100)]
       public virtual String Tarifa { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("EmpresasId")]
       [DDisplayName("ListaPrecios.EmpresasId")]
       public virtual Int64? EmpresasId { get; set; }

       [Column("RelacionaListaPreciosId")]
       [DDisplayName("ListaPrecios.RelacionaListaPreciosId")]
       public virtual Int64? RelacionaListaPreciosId { get; set; }

       [Column("TipoEstadosId")]
       [DDisplayName("ListaPrecios.TipoEstadosId")]
       [DRequired("ListaPrecios.TipoEstadosId")]
       [DRequiredFK("ListaPrecios.TipoEstadosId")]
       public virtual Int64 TipoEstadosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("TipoEstadosId")]
       public virtual Estados TipoEstados { get; set; }

       [ForeignKey("RelacionaListaPreciosId")]
       public virtual ListaPrecios RelacionaListaPrecios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ListaPrecios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ListaPrecios, bool>> expression = null;

        expression = entity => entity.Nombre == this.Nombre && entity.EmpresasId == this.EmpresasId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ListaPrecios.Nombre" , "ListaPrecios.EmpresasId" )));

        expression = entity => entity.Nombre == this.Nombre && entity.EmpresasId == this.EmpresasId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ListaPrecios.Nombre" , "ListaPrecios.EmpresasId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ListaPrecios, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Nombre == this.Nombre && entity.EmpresasId == this.EmpresasId)
                               && entity.Nombre == this.Nombre && entity.EmpresasId == this.EmpresasId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ListaPrecios.Nombre" , "ListaPrecios.EmpresasId" )));

        expression = entity => !(entity.Id == this.Id && entity.Nombre == this.Nombre && entity.EmpresasId == this.EmpresasId)
                               && entity.Nombre == this.Nombre && entity.EmpresasId == this.EmpresasId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ListaPrecios.Nombre" , "ListaPrecios.EmpresasId" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Convenios, bool>> expression0 = entity => entity.ListaPreciosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Convenios"), typeof(Convenios)));

        Expression<Func<ListaPrecios, bool>> expression1 = entity => entity.RelacionaListaPreciosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ListaPrecios"), typeof(ListaPrecios)));

        Expression<Func<PreciosServicios, bool>> expression2 = entity => entity.ListaPreciosId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","PreciosServicios"), typeof(PreciosServicios)));

       return rules;
       }

       #endregion
    }
 }
