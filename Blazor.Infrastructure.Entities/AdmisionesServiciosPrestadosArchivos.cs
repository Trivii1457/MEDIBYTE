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
    /// AdmisionesServiciosPrestadosArchivos object for mapped table AdmisionesServiciosPrestadosArchivos.
    /// </summary>
    [Table("AdmisionesServiciosPrestadosArchivos")]
    public partial class AdmisionesServiciosPrestadosArchivos : BaseEntity
    {

       #region Columnas normales)

       [Column("ResultadoArchivo")]
       [DDisplayName("AdmisionesServiciosPrestadosArchivos.ResultadoArchivo")]
       public virtual Byte[] ResultadoArchivo { get; set; }

       [Column("TipoContenido")]
       [DDisplayName("AdmisionesServiciosPrestadosArchivos.TipoContenido")]
       [DRequired("AdmisionesServiciosPrestadosArchivos.TipoContenido")]
       [DStringLength("AdmisionesServiciosPrestadosArchivos.TipoContenido",100)]
       public virtual String TipoContenido { get; set; }

       [Column("Nombre")]
       [DDisplayName("AdmisionesServiciosPrestadosArchivos.Nombre")]
       [DRequired("AdmisionesServiciosPrestadosArchivos.Nombre")]
       [DStringLength("AdmisionesServiciosPrestadosArchivos.Nombre",100)]
       public virtual String Nombre { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("AdmisionesServiciosPrestadosId")]
       [DDisplayName("AdmisionesServiciosPrestadosArchivos.AdmisionesServiciosPrestadosId")]
       [DRequired("AdmisionesServiciosPrestadosArchivos.AdmisionesServiciosPrestadosId")]
       [DRequiredFK("AdmisionesServiciosPrestadosArchivos.AdmisionesServiciosPrestadosId")]
       public virtual Int64 AdmisionesServiciosPrestadosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("AdmisionesServiciosPrestadosId")]
       public virtual AdmisionesServiciosPrestados AdmisionesServiciosPrestados { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<AdmisionesServiciosPrestadosArchivos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdmisionesServiciosPrestadosArchivos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdmisionesServiciosPrestadosArchivos, bool>> expression = null;

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
