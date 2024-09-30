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
    /// Consultorios object for mapped table Consultorios.
    /// </summary>
    [Table("Consultorios")]
    public partial class Consultorios : BaseEntity
    {

       #region Columnas normales)

       [Column("Codigo")]
       [DDisplayName("Consultorios.Codigo")]
       [DRequired("Consultorios.Codigo")]
       [DStringLength("Consultorios.Codigo",45)]
       public virtual String Codigo { get; set; }

       [Column("Nombre")]
       [DDisplayName("Consultorios.Nombre")]
       [DStringLength("Consultorios.Nombre",255)]
       public virtual String Nombre { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("SedesId")]
       [DDisplayName("Consultorios.SedesId")]
       [DRequired("Consultorios.SedesId")]
       [DRequiredFK("Consultorios.SedesId")]
       public virtual Int64 SedesId { get; set; }

       [Column("EstadosId")]
       [DDisplayName("Consultorios.EstadosId")]
       [DRequired("Consultorios.EstadosId")]
       [DRequiredFK("Consultorios.EstadosId")]
       public virtual Int64 EstadosId { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("Consultorios.EmpresasId")]
       [DRequired("Consultorios.EmpresasId")]
       [DRequiredFK("Consultorios.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EstadosId")]
       public virtual Estados Estados { get; set; }

       [ForeignKey("SedesId")]
       public virtual Sedes Sedes { get; set; }

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Consultorios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Consultorios, bool>> expression = null;

        expression = entity => entity.SedesId == this.SedesId && entity.Codigo == this.Codigo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Consultorios.SedesId" , "Consultorios.Codigo" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Consultorios, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.SedesId == this.SedesId && entity.Codigo == this.Codigo)
                               && entity.SedesId == this.SedesId && entity.Codigo == this.Codigo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Consultorios.SedesId" , "Consultorios.Codigo" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ProgramacionCitas, bool>> expression0 = entity => entity.ConsultoriosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionCitas"), typeof(ProgramacionCitas)));

        Expression<Func<ProgramacionDisponible, bool>> expression1 = entity => entity.ConsultoriosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionDisponible"), typeof(ProgramacionDisponible)));

        Expression<Func<ServiciosConsultorios, bool>> expression2 = entity => entity.ConsultoriosId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ServiciosConsultorios"), typeof(ServiciosConsultorios)));

       return rules;
       }

       #endregion
    }
 }
