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
    /// Filiales object for mapped table Filiales.
    /// </summary>
    [Table("Filiales")]
    public partial class Filiales : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("Filiales.Nombre")]
       [DRequired("Filiales.Nombre")]
       [DStringLength("Filiales.Nombre",250)]
       public virtual String Nombre { get; set; }

       [Column("CorreoElectronico")]
       [DDisplayName("Filiales.CorreoElectronico")]
       [DStringLength("Filiales.CorreoElectronico",200)]
       public virtual String CorreoElectronico { get; set; }

       [Column("Fax")]
       [DDisplayName("Filiales.Fax")]
       [DStringLength("Filiales.Fax",100)]
       public virtual String Fax { get; set; }

       [Column("PaginaWeb")]
       [DDisplayName("Filiales.PaginaWeb")]
       [DStringLength("Filiales.PaginaWeb",255)]
       public virtual String PaginaWeb { get; set; }

       [Column("Direccion")]
       [DDisplayName("Filiales.Direccion")]
       [DStringLength("Filiales.Direccion",255)]
       public virtual String Direccion { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("EntidadesId")]
       [DDisplayName("Filiales.EntidadesId")]
       [DRequired("Filiales.EntidadesId")]
       [DRequiredFK("Filiales.EntidadesId")]
       public virtual Int64 EntidadesId { get; set; }

       [Column("EstadosId")]
       [DDisplayName("Filiales.EstadosId")]
       [DRequired("Filiales.EstadosId")]
       [DRequiredFK("Filiales.EstadosId")]
       public virtual Int64 EstadosId { get; set; }

       [Column("CiudadesId")]
       [DDisplayName("Filiales.CiudadesId")]
       [DRequired("Filiales.CiudadesId")]
       [DRequiredFK("Filiales.CiudadesId")]
       public virtual Int64 CiudadesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("CiudadesId")]
       public virtual Ciudades Ciudades { get; set; }

       [ForeignKey("EntidadesId")]
       public virtual Entidades Entidades { get; set; }

       [ForeignKey("EstadosId")]
       public virtual Estados Estados { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Filiales, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Filiales, bool>> expression = null;

        expression = entity => entity.EntidadesId == this.EntidadesId && entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Filiales.EntidadesId" , "Filiales.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Filiales, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.EntidadesId == this.EntidadesId && entity.Nombre == this.Nombre)
                               && entity.EntidadesId == this.EntidadesId && entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Filiales.EntidadesId" , "Filiales.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Admisiones, bool>> expression0 = entity => entity.FilialesId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<Facturas, bool>> expression1 = entity => entity.FilialesId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Facturas"), typeof(Facturas)));

       return rules;
       }

       #endregion
    }
 }
