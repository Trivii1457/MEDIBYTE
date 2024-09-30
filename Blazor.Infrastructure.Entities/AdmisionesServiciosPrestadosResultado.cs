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
    /// AdmisionesServiciosPrestadosResultado object for mapped table AdmisionesServiciosPrestadosResultado.
    /// </summary>
    [Table("AdmisionesServiciosPrestadosResultado")]
    public partial class AdmisionesServiciosPrestadosResultado : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("AdmisionesServiciosPrestadosResultado.Nombre")]
       [DRequired("AdmisionesServiciosPrestadosResultado.Nombre")]
       [DStringLength("AdmisionesServiciosPrestadosResultado.Nombre",255)]
       public virtual String Nombre { get; set; }

       [Column("Resultado")]
       [DDisplayName("AdmisionesServiciosPrestadosResultado.Resultado")]
       public virtual Byte[] Resultado { get; set; }

       [Column("ResultadoAudio")]
       [DDisplayName("AdmisionesServiciosPrestadosResultado.ResultadoAudio")]
       [DStringLength("AdmisionesServiciosPrestadosResultado.ResultadoAudio",50)]
       public virtual String ResultadoAudio { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("AdmisionesServiciosPrestadosId")]
       [DDisplayName("AdmisionesServiciosPrestadosResultado.AdmisionesServiciosPrestadosId")]
       [DRequired("AdmisionesServiciosPrestadosResultado.AdmisionesServiciosPrestadosId")]
       [DRequiredFK("AdmisionesServiciosPrestadosResultado.AdmisionesServiciosPrestadosId")]
       public virtual Int64 AdmisionesServiciosPrestadosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("AdmisionesServiciosPrestadosId")]
       public virtual AdmisionesServiciosPrestados AdmisionesServiciosPrestados { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<AdmisionesServiciosPrestadosResultado, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdmisionesServiciosPrestadosResultado, bool>> expression = null;

        expression = entity => entity.Nombre == this.Nombre && entity.AdmisionesServiciosPrestadosId == this.AdmisionesServiciosPrestadosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "AdmisionesServiciosPrestadosResultado.Nombre" , "AdmisionesServiciosPrestadosResultado.AdmisionesServiciosPrestadosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdmisionesServiciosPrestadosResultado, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Nombre == this.Nombre && entity.AdmisionesServiciosPrestadosId == this.AdmisionesServiciosPrestadosId)
                               && entity.Nombre == this.Nombre && entity.AdmisionesServiciosPrestadosId == this.AdmisionesServiciosPrestadosId;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "AdmisionesServiciosPrestadosResultado.Nombre" , "AdmisionesServiciosPrestadosResultado.AdmisionesServiciosPrestadosId" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
       return rules;
       }

       #endregion
    }
 }
