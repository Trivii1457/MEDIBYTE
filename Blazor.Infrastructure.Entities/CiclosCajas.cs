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
    /// CiclosCajas object for mapped table CiclosCajas.
    /// </summary>
    [Table("CiclosCajas")]
    public partial class CiclosCajas : BaseEntity
    {

       #region Columnas normales)

       [Column("FechaApertura", TypeName = "datetime")]
       [DDisplayName("CiclosCajas.FechaApertura")]
       [DRequired("CiclosCajas.FechaApertura")]
       public virtual DateTime FechaApertura { get; set; }

       [Column("ValorApertura")]
       [DDisplayName("CiclosCajas.ValorApertura")]
       [DRequired("CiclosCajas.ValorApertura")]
       public virtual Decimal ValorApertura { get; set; }

       [Column("FechaCierre", TypeName = "datetime")]
       [DDisplayName("CiclosCajas.FechaCierre")]
       public virtual DateTime? FechaCierre { get; set; }

       [Column("ValorCierre")]
       [DDisplayName("CiclosCajas.ValorCierre")]
       [DRequired("CiclosCajas.ValorCierre")]
       public virtual Decimal ValorCierre { get; set; }

       [Column("TotalFaltante")]
       [DDisplayName("CiclosCajas.TotalFaltante")]
       [DRequired("CiclosCajas.TotalFaltante")]
       public virtual Decimal TotalFaltante { get; set; }

       [Column("TotalSobrante")]
       [DDisplayName("CiclosCajas.TotalSobrante")]
       [DRequired("CiclosCajas.TotalSobrante")]
       public virtual Decimal TotalSobrante { get; set; }

       [Column("TotalRecaudos")]
       [DDisplayName("CiclosCajas.TotalRecaudos")]
       [DRequired("CiclosCajas.TotalRecaudos")]
       public virtual Decimal TotalRecaudos { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("CajasId")]
       [DDisplayName("CiclosCajas.CajasId")]
       [DRequired("CiclosCajas.CajasId")]
       [DRequiredFK("CiclosCajas.CajasId")]
       public virtual Int64 CajasId { get; set; }

       [Column("OpenUsersId")]
       [DDisplayName("CiclosCajas.OpenUsersId")]
       [DRequired("CiclosCajas.OpenUsersId")]
       [DRequiredFK("CiclosCajas.OpenUsersId")]
       public virtual Int64 OpenUsersId { get; set; }

       [Column("CloseUsersId")]
       [DDisplayName("CiclosCajas.CloseUsersId")]
       public virtual Int64? CloseUsersId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("CajasId")]
       public virtual Cajas Cajas { get; set; }

       [ForeignKey("CloseUsersId")]
       public virtual User CloseUsers { get; set; }

       [ForeignKey("OpenUsersId")]
       public virtual User OpenUsers { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<CiclosCajas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CiclosCajas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<CiclosCajas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Recaudos, bool>> expression0 = entity => entity.CiclosCajasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Recaudos"), typeof(Recaudos)));

       return rules;
       }

       #endregion
    }
 }
