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
    /// OrdenesServiciosDiagnosticos object for mapped table OrdenesServiciosDiagnosticos.
    /// </summary>
    [Table("OrdenesServiciosDiagnosticos")]
    public partial class OrdenesServiciosDiagnosticos : BaseEntity
    {

       #region Columnas normales)

       [Column("Principal")]
       [DDisplayName("OrdenesServiciosDiagnosticos.Principal")]
       [DRequired("OrdenesServiciosDiagnosticos.Principal")]
       public virtual Boolean Principal { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("DiagnosticosId")]
       [DDisplayName("OrdenesServiciosDiagnosticos.DiagnosticosId")]
       [DRequired("OrdenesServiciosDiagnosticos.DiagnosticosId")]
       [DRequiredFK("OrdenesServiciosDiagnosticos.DiagnosticosId")]
       public virtual Int64 DiagnosticosId { get; set; }

       [Column("OrdenesServiciosId")]
       [DDisplayName("OrdenesServiciosDiagnosticos.OrdenesServiciosId")]
       public virtual Int64? OrdenesServiciosId { get; set; }

       [Column("TiposDiagnosticosId")]
       [DDisplayName("OrdenesServiciosDiagnosticos.TiposDiagnosticosId")]
       [DRequired("OrdenesServiciosDiagnosticos.TiposDiagnosticosId")]
       [DRequiredFK("OrdenesServiciosDiagnosticos.TiposDiagnosticosId")]
       public virtual Int64 TiposDiagnosticosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("DiagnosticosId")]
       public virtual Diagnosticos Diagnosticos { get; set; }

       [ForeignKey("OrdenesServiciosId")]
       public virtual OrdenesServicios OrdenesServicios { get; set; }

       [ForeignKey("TiposDiagnosticosId")]
       public virtual TiposDiagnosticos TiposDiagnosticos { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<OrdenesServiciosDiagnosticos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesServiciosDiagnosticos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<OrdenesServiciosDiagnosticos, bool>> expression = null;

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
