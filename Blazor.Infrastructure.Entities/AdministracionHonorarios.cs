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
    /// AdministracionHonorarios object for mapped table AdministracionHonorarios.
    /// </summary>
    [Table("AdministracionHonorarios")]
    public partial class AdministracionHonorarios : BaseEntity
    {

       #region Columnas normales)

       #endregion

       #region Columnas referenciales)

       [Column("EmpleadosId")]
       [DDisplayName("AdministracionHonorarios.EmpleadosId")]
       [DRequired("AdministracionHonorarios.EmpleadosId")]
       [DRequiredFK("AdministracionHonorarios.EmpleadosId")]
       public virtual Int64 EmpleadosId { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("AdministracionHonorarios.EmpresasId")]
       public virtual Int64? EmpresasId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EmpleadosId")]
       public virtual Empleados Empleados { get; set; }

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<AdministracionHonorarios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdministracionHonorarios, bool>> expression = null;

        expression = entity => entity.EmpleadosId == this.EmpleadosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "AdministracionHonorarios.EmpleadosId" )));

        expression = entity => entity.EmpresasId == this.EmpresasId && entity.EmpleadosId == this.EmpleadosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "AdministracionHonorarios.EmpresasId" , "AdministracionHonorarios.EmpleadosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdministracionHonorarios, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.EmpleadosId == this.EmpleadosId)
                               && entity.EmpleadosId == this.EmpleadosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "AdministracionHonorarios.EmpleadosId" )));

        expression = entity => !(entity.Id == this.Id && entity.EmpresasId == this.EmpresasId && entity.EmpleadosId == this.EmpleadosId)
                               && entity.EmpresasId == this.EmpresasId && entity.EmpleadosId == this.EmpleadosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "AdministracionHonorarios.EmpresasId" , "AdministracionHonorarios.EmpleadosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdministracionHonorariosLecturas, bool>> expression0 = entity => entity.AdministracionHonorariosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdministracionHonorariosLecturas"), typeof(AdministracionHonorariosLecturas)));

        Expression<Func<AdministracionHonorariosServicios, bool>> expression1 = entity => entity.AdministracionHonorariosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdministracionHonorariosServicios"), typeof(AdministracionHonorariosServicios)));

       return rules;
       }

       #endregion
    }
 }
