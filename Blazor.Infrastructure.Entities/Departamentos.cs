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
    /// Departamentos object for mapped table Departamentos.
    /// </summary>
    [Table("Departamentos")]
    public partial class Departamentos : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("Departamentos.Nombre")]
       [DRequired("Departamentos.Nombre")]
       [DStringLength("Departamentos.Nombre",150)]
       public virtual String Nombre { get; set; }

       [Column("Codigo")]
       [DDisplayName("Departamentos.Codigo")]
       [DRequired("Departamentos.Codigo")]
       [DStringLength("Departamentos.Codigo",5)]
       public virtual String Codigo { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("PaisesId")]
       [DDisplayName("Departamentos.PaisesId")]
       [DRequired("Departamentos.PaisesId")]
       [DRequiredFK("Departamentos.PaisesId")]
       public virtual Int64 PaisesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("PaisesId")]
       public virtual Paises Paises { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Departamentos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Departamentos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Departamentos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Ciudades, bool>> expression0 = entity => entity.DepartamentosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Ciudades"), typeof(Ciudades)));

       return rules;
       }

       #endregion
    }
 }
