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
    /// RegistroIncapacidades object for mapped table RegistroIncapacidades.
    /// </summary>
    [Table("RegistroIncapacidades")]
    public partial class RegistroIncapacidades : BaseEntity
    {

       #region Columnas normales)

       [Column("NroDias")]
       [DDisplayName("RegistroIncapacidades.NroDias")]
       [DRequired("RegistroIncapacidades.NroDias")]
       public virtual Int16 NroDias { get; set; }

       [Column("FechaInicio", TypeName = "datetime")]
       [DDisplayName("RegistroIncapacidades.FechaInicio")]
       [DRequired("RegistroIncapacidades.FechaInicio")]
       public virtual DateTime FechaInicio { get; set; }

       [Column("FechaFinal", TypeName = "datetime")]
       [DDisplayName("RegistroIncapacidades.FechaFinal")]
       [DRequired("RegistroIncapacidades.FechaFinal")]
       public virtual DateTime FechaFinal { get; set; }

       [Column("DiagnosticosId")]
       [DDisplayName("RegistroIncapacidades.DiagnosticosId")]
       [DRequired("RegistroIncapacidades.DiagnosticosId")]
       public virtual Int64 DiagnosticosId { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("AtencionesId")]
       [DDisplayName("RegistroIncapacidades.AtencionesId")]
       [DRequired("RegistroIncapacidades.AtencionesId")]
       [DRequiredFK("RegistroIncapacidades.AtencionesId")]
       public virtual Int64 AtencionesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("AtencionesId")]
       public virtual Atenciones Atenciones { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<RegistroIncapacidades, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<RegistroIncapacidades, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<RegistroIncapacidades, bool>> expression = null;

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
