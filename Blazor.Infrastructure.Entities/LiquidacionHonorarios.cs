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
    /// LiquidacionHonorarios object for mapped table LiquidacionHonorarios.
    /// </summary>
    [Table("LiquidacionHonorarios")]
    public partial class LiquidacionHonorarios : BaseEntity
    {

       #region Columnas normales)

       [Column("Consecutivo")]
       [DDisplayName("LiquidacionHonorarios.Consecutivo")]
       [DRequired("LiquidacionHonorarios.Consecutivo")]
       public virtual Int32 Consecutivo { get; set; }

       [Column("FechaInicio", TypeName = "datetime")]
       [DDisplayName("LiquidacionHonorarios.FechaInicio")]
       [DRequired("LiquidacionHonorarios.FechaInicio")]
       public virtual DateTime FechaInicio { get; set; }

       [Column("FechaFinal", TypeName = "datetime")]
       [DDisplayName("LiquidacionHonorarios.FechaFinal")]
       [DRequired("LiquidacionHonorarios.FechaFinal")]
       public virtual DateTime FechaFinal { get; set; }

       [Column("ValorTotal")]
       [DDisplayName("LiquidacionHonorarios.ValorTotal")]
       [DRequired("LiquidacionHonorarios.ValorTotal")]
       public virtual Decimal ValorTotal { get; set; }

       [Column("Observaciones")]
       [DDisplayName("LiquidacionHonorarios.Observaciones")]
       [DStringLength("LiquidacionHonorarios.Observaciones",2000)]
       public virtual String Observaciones { get; set; }

       [Column("DetalleAnulacion")]
       [DDisplayName("LiquidacionHonorarios.DetalleAnulacion")]
       [DStringLength("LiquidacionHonorarios.DetalleAnulacion",2000)]
       public virtual String DetalleAnulacion { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("EmpresasId")]
       [DDisplayName("LiquidacionHonorarios.EmpresasId")]
       public virtual Int64? EmpresasId { get; set; }

       [Column("EmpleadosId")]
       [DDisplayName("LiquidacionHonorarios.EmpleadosId")]
       public virtual Int64? EmpleadosId { get; set; }

       [Column("EstadosId")]
       [DDisplayName("LiquidacionHonorarios.EstadosId")]
       [DRequired("LiquidacionHonorarios.EstadosId")]
       [DRequiredFK("LiquidacionHonorarios.EstadosId")]
       public virtual Int64 EstadosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EmpleadosId")]
       public virtual Empleados Empleados { get; set; }

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("EstadosId")]
       public virtual Estados Estados { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<LiquidacionHonorarios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<LiquidacionHonorarios, bool>> expression = null;

        expression = entity => entity.EmpresasId == this.EmpresasId && entity.Consecutivo == this.Consecutivo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "LiquidacionHonorarios.EmpresasId" , "LiquidacionHonorarios.Consecutivo" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<LiquidacionHonorarios, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.EmpresasId == this.EmpresasId && entity.Consecutivo == this.Consecutivo)
                               && entity.EmpresasId == this.EmpresasId && entity.Consecutivo == this.Consecutivo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "LiquidacionHonorarios.EmpresasId" , "LiquidacionHonorarios.Consecutivo" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<LiquidacionHonorariosDetalle, bool>> expression0 = entity => entity.LiquidacionHonorariosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","LiquidacionHonorariosDetalle"), typeof(LiquidacionHonorariosDetalle)));

       return rules;
       }

       #endregion
    }
 }
