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
    /// Rips object for mapped table Rips.
    /// </summary>
    [Table("Rips")]
    public partial class Rips : BaseEntity
    {

       #region Columnas normales)

       [Column("GenararCT")]
       [DDisplayName("Rips.GenararCT")]
       [DRequired("Rips.GenararCT")]
       public virtual Boolean GenararCT { get; set; }

       [Column("GenerarAF")]
       [DDisplayName("Rips.GenerarAF")]
       [DRequired("Rips.GenerarAF")]
       public virtual Boolean GenerarAF { get; set; }

       [Column("GenerarUS")]
       [DDisplayName("Rips.GenerarUS")]
       [DRequired("Rips.GenerarUS")]
       public virtual Boolean GenerarUS { get; set; }

       [Column("GenerarAC")]
       [DDisplayName("Rips.GenerarAC")]
       [DRequired("Rips.GenerarAC")]
       public virtual Boolean GenerarAC { get; set; }

       [Column("GenerarAP")]
       [DDisplayName("Rips.GenerarAP")]
       [DRequired("Rips.GenerarAP")]
       public virtual Boolean GenerarAP { get; set; }

       [Column("GenerarAU")]
       [DDisplayName("Rips.GenerarAU")]
       [DRequired("Rips.GenerarAU")]
       public virtual Boolean GenerarAU { get; set; }

       [Column("GenerarAH")]
       [DDisplayName("Rips.GenerarAH")]
       [DRequired("Rips.GenerarAH")]
       public virtual Boolean GenerarAH { get; set; }

       [Column("GenerarAN")]
       [DDisplayName("Rips.GenerarAN")]
       [DRequired("Rips.GenerarAN")]
       public virtual Boolean GenerarAN { get; set; }

       [Column("GenerarAM")]
       [DDisplayName("Rips.GenerarAM")]
       [DRequired("Rips.GenerarAM")]
       public virtual Boolean GenerarAM { get; set; }

       [Column("GenerarAT")]
       [DDisplayName("Rips.GenerarAT")]
       [DRequired("Rips.GenerarAT")]
       public virtual Boolean GenerarAT { get; set; }

       [Column("FechaRemision", TypeName = "datetime")]
       [DDisplayName("Rips.FechaRemision")]
       [DRequired("Rips.FechaRemision")]
       public virtual DateTime FechaRemision { get; set; }

       [Column("Periodo", TypeName = "datetime")]
       [DDisplayName("Rips.Periodo")]
       [DRequired("Rips.Periodo")]
       public virtual DateTime Periodo { get; set; }

       [Column("Consecutivo")]
       [DDisplayName("Rips.Consecutivo")]
       [DRequired("Rips.Consecutivo")]
       public virtual Int64 Consecutivo { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("EmpresasId")]
       [DDisplayName("Rips.EmpresasId")]
       [DRequired("Rips.EmpresasId")]
       [DRequiredFK("Rips.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("EntidadesId")]
       [DDisplayName("Rips.EntidadesId")]
       [DRequired("Rips.EntidadesId")]
       [DRequiredFK("Rips.EntidadesId")]
       public virtual Int64 EntidadesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("EntidadesId")]
       public virtual Entidades Entidades { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Rips, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Rips, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Rips, bool>> expression = null;

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
