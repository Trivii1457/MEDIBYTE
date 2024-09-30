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
    /// Notificaciones object for mapped table Notificaciones.
    /// </summary>
    [Table("Notificaciones")]
    public partial class Notificaciones : BaseEntity
    {

       #region Columnas normales)

       [Column("Origen")]
       [DDisplayName("Notificaciones.Origen")]
       [DRequired("Notificaciones.Origen")]
       [DStringLength("Notificaciones.Origen",150)]
       public virtual String Origen { get; set; }

       [Column("Mensaje")]
       [DDisplayName("Notificaciones.Mensaje")]
       [DRequired("Notificaciones.Mensaje")]
       public virtual String Mensaje { get; set; }

       [Column("Leido")]
       [DDisplayName("Notificaciones.Leido")]
       [DRequired("Notificaciones.Leido")]
       public virtual Boolean Leido { get; set; }

       [Column("UsuarioDestino")]
       [DDisplayName("Notificaciones.UsuarioDestino")]
       public virtual Int64? UsuarioDestino { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Notificaciones, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Notificaciones, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Notificaciones, bool>> expression = null;

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
