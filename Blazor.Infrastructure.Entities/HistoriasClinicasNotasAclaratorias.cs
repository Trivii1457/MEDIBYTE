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
    /// HistoriasClinicasNotasAclaratorias object for mapped table HistoriasClinicasNotasAclaratorias.
    /// </summary>
    [Table("HistoriasClinicasNotasAclaratorias")]
    public partial class HistoriasClinicasNotasAclaratorias : BaseEntity
    {

       #region Columnas normales)

       [Column("Aclaracion")]
       [DDisplayName("HistoriasClinicasNotasAclaratorias.Aclaracion")]
       public virtual String Aclaracion { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("HistoriasClinicasId")]
       [DDisplayName("HistoriasClinicasNotasAclaratorias.HistoriasClinicasId")]
       [DRequired("HistoriasClinicasNotasAclaratorias.HistoriasClinicasId")]
       [DRequiredFK("HistoriasClinicasNotasAclaratorias.HistoriasClinicasId")]
       public virtual Int64 HistoriasClinicasId { get; set; }

       [Column("ProfesionalId")]
       [DDisplayName("HistoriasClinicasNotasAclaratorias.ProfesionalId")]
       [DRequired("HistoriasClinicasNotasAclaratorias.ProfesionalId")]
       [DRequiredFK("HistoriasClinicasNotasAclaratorias.ProfesionalId")]
       public virtual Int64 ProfesionalId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("ProfesionalId")]
       public virtual Empleados Profesional { get; set; }

       [ForeignKey("HistoriasClinicasId")]
       public virtual HistoriasClinicas HistoriasClinicas { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<HistoriasClinicasNotasAclaratorias, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HistoriasClinicasNotasAclaratorias, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<HistoriasClinicasNotasAclaratorias, bool>> expression = null;

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
