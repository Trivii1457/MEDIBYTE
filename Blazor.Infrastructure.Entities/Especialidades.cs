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
    /// Especialidades object for mapped table Especialidades.
    /// </summary>
    [Table("Especialidades")]
    public partial class Especialidades : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("Especialidades.Nombre")]
       [DRequired("Especialidades.Nombre")]
       [DStringLength("Especialidades.Nombre",500)]
       public virtual String Nombre { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Especialidades, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Especialidades, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Especialidades, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EmpleadosEspecialidades, bool>> expression0 = entity => entity.EspecialidadesId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EmpleadosEspecialidades"), typeof(EmpleadosEspecialidades)));

        Expression<Func<HCTipos, bool>> expression1 = entity => entity.EspecialidadesId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HCTipos"), typeof(HCTipos)));

        Expression<Func<ProgramacionCitas, bool>> expression2 = entity => entity.EspecialidadesId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionCitas"), typeof(ProgramacionCitas)));

        Expression<Func<Servicios, bool>> expression3 = entity => entity.EspecialidadesId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Servicios"), typeof(Servicios)));

       return rules;
       }

       #endregion
    }
 }
