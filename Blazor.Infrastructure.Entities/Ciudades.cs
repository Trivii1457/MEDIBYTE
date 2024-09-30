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
    /// Ciudades object for mapped table Ciudades.
    /// </summary>
    [Table("Ciudades")]
    public partial class Ciudades : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("Ciudades.Nombre")]
       [DRequired("Ciudades.Nombre")]
       [DStringLength("Ciudades.Nombre",150)]
       public virtual String Nombre { get; set; }

       [Column("Codigo")]
       [DDisplayName("Ciudades.Codigo")]
       [DRequired("Ciudades.Codigo")]
       [DStringLength("Ciudades.Codigo",5)]
       public virtual String Codigo { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("DepartamentosId")]
       [DDisplayName("Ciudades.DepartamentosId")]
       [DRequired("Ciudades.DepartamentosId")]
       [DRequiredFK("Ciudades.DepartamentosId")]
       public virtual Int64 DepartamentosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("DepartamentosId")]
       public virtual Departamentos Departamentos { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Ciudades, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Ciudades, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Ciudades, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Empresas, bool>> expression0 = entity => entity.CiudadesId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Empresas"), typeof(Empresas)));

        Expression<Func<Entidades, bool>> expression1 = entity => entity.CiudadesId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Entidades"), typeof(Entidades)));

        Expression<Func<Filiales, bool>> expression2 = entity => entity.CiudadesId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Filiales"), typeof(Filiales)));

        Expression<Func<Pacientes, bool>> expression3 = entity => entity.CiudadesId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Pacientes"), typeof(Pacientes)));

        Expression<Func<Sedes, bool>> expression4 = entity => entity.CiudadesId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Sedes"), typeof(Sedes)));

       return rules;
       }

       #endregion
    }
 }
