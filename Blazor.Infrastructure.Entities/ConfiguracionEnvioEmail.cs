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
    /// ConfiguracionEnvioEmail object for mapped table ConfiguracionEnvioEmail.
    /// </summary>
    [Table("ConfiguracionEnvioEmail")]
    public partial class ConfiguracionEnvioEmail : BaseEntity
    {

       #region Columnas normales)

       [Column("CorreoElectronico")]
       [DDisplayName("ConfiguracionEnvioEmail.CorreoElectronico")]
       [DRequired("ConfiguracionEnvioEmail.CorreoElectronico")]
       [DStringLength("ConfiguracionEnvioEmail.CorreoElectronico",150)]
       public virtual String CorreoElectronico { get; set; }

       [Column("Contrasena")]
       [DDisplayName("ConfiguracionEnvioEmail.Contrasena")]
       [DRequired("ConfiguracionEnvioEmail.Contrasena")]
       [DStringLength("ConfiguracionEnvioEmail.Contrasena",150)]
       public virtual String Contrasena { get; set; }

       [Column("ServidorSMTP")]
       [DDisplayName("ConfiguracionEnvioEmail.ServidorSMTP")]
       [DRequired("ConfiguracionEnvioEmail.ServidorSMTP")]
       [DStringLength("ConfiguracionEnvioEmail.ServidorSMTP",150)]
       public virtual String ServidorSMTP { get; set; }

       [Column("Puerto")]
       [DDisplayName("ConfiguracionEnvioEmail.Puerto")]
       [DRequired("ConfiguracionEnvioEmail.Puerto")]
       public virtual Int16 Puerto { get; set; }

       [Column("UsaSSL")]
       [DDisplayName("ConfiguracionEnvioEmail.UsaSSL")]
       [DRequired("ConfiguracionEnvioEmail.UsaSSL")]
       public virtual Boolean UsaSSL { get; set; }

       [Column("Origen")]
       [DDisplayName("ConfiguracionEnvioEmail.Origen")]
       [DRequired("ConfiguracionEnvioEmail.Origen")]
       [DStringLength("ConfiguracionEnvioEmail.Origen",50)]
       public virtual String Origen { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ConfiguracionEnvioEmail, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ConfiguracionEnvioEmail, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ConfiguracionEnvioEmail, bool>> expression = null;

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
