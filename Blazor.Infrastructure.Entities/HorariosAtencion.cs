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
    /// HorariosAtencion object for mapped table HorariosAtencion.
    /// </summary>
    [Table("HorariosAtencion")]
    public partial class HorariosAtencion : BaseEntity
    {

       #region Columnas normales)

       [Column("Fecha", TypeName = "datetime")]
       [DDisplayName("HorariosAtencion.Fecha")]
       public virtual DateTime? Fecha { get; set; }

       [Column("HoraInicio", TypeName = "datetime")]
       [DDisplayName("HorariosAtencion.HoraInicio")]
       [DRequired("HorariosAtencion.HoraInicio")]
       public virtual DateTime HoraInicio { get; set; }

       [Column("HoraFinal", TypeName = "datetime")]
       [DDisplayName("HorariosAtencion.HoraFinal")]
       [DRequired("HorariosAtencion.HoraFinal")]
       public virtual DateTime HoraFinal { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("SedeId")]
       [DDisplayName("HorariosAtencion.SedeId")]
       [DRequired("HorariosAtencion.SedeId")]
       [DRequiredFK("HorariosAtencion.SedeId")]
       public virtual Int64 SedeId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("SedeId")]
       public virtual Sedes Sede { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<HorariosAtencion, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HorariosAtencion, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HorariosAtencion, bool>> expression = null;

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
