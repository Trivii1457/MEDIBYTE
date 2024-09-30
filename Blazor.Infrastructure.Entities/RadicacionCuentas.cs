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
    /// RadicacionCuentas object for mapped table RadicacionCuentas.
    /// </summary>
    [Table("RadicacionCuentas")]
    public partial class RadicacionCuentas : BaseEntity
    {

       #region Columnas normales)

       [Column("FechaRadicacion", TypeName = "datetime")]
       [DDisplayName("RadicacionCuentas.FechaRadicacion")]
       public virtual DateTime? FechaRadicacion { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("EmpresasId")]
       [DDisplayName("RadicacionCuentas.EmpresasId")]
       [DRequired("RadicacionCuentas.EmpresasId")]
       [DRequiredFK("RadicacionCuentas.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("SedesId")]
       [DDisplayName("RadicacionCuentas.SedesId")]
       [DRequired("RadicacionCuentas.SedesId")]
       [DRequiredFK("RadicacionCuentas.SedesId")]
       public virtual Int64 SedesId { get; set; }

       [Column("EntidadesId")]
       [DDisplayName("RadicacionCuentas.EntidadesId")]
       [DRequired("RadicacionCuentas.EntidadesId")]
       [DRequiredFK("RadicacionCuentas.EntidadesId")]
       public virtual Int64 EntidadesId { get; set; }

       [Column("RadicacionArchivosId")]
       [DDisplayName("RadicacionCuentas.RadicacionArchivosId")]
       public virtual Int64? RadicacionArchivosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("RadicacionArchivosId")]
       public virtual Archivos RadicacionArchivos { get; set; }

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("EntidadesId")]
       public virtual Entidades Entidades { get; set; }

       [ForeignKey("SedesId")]
       public virtual Sedes Sedes { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<RadicacionCuentas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<RadicacionCuentas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<RadicacionCuentas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<RadicacionCuentasDetalles, bool>> expression0 = entity => entity.RadicacionCuentasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","RadicacionCuentasDetalles"), typeof(RadicacionCuentasDetalles)));

       return rules;
       }

       #endregion
    }
 }
