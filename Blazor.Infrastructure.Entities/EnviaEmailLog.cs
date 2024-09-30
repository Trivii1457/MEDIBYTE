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
    /// EnviaEmailLog object for mapped table EnviaEmailLog.
    /// </summary>
    [Table("EnviaEmailLog")]
    public partial class EnviaEmailLog : BaseEntity
    {

        #region Columnas normales)

        [Column("Origen")]
        [DDisplayName("EnviaEmailLog.Origen")]
        [DRequired("EnviaEmailLog.Origen")]
        [DStringLength("EnviaEmailLog.Origen", 50)]
        public virtual String Origen { get; set; }

        [Column("CorreoEnvia")]
        [DDisplayName("EnviaEmailLog.CorreoEnvia")]
        [DRequired("EnviaEmailLog.CorreoEnvia")]
        [DStringLength("EnviaEmailLog.CorreoEnvia", 250)]
        public virtual String CorreoEnvia { get; set; }

        [Column("CorreosDestinatarios")]
        [DDisplayName("EnviaEmailLog.CorreosDestinatarios")]
        [DRequired("EnviaEmailLog.CorreosDestinatarios")]
        public virtual String CorreosDestinatarios { get; set; }

        [Column("Exitoso")]
        [DDisplayName("EnviaEmailLog.Exitoso")]
        [DRequired("EnviaEmailLog.Exitoso")]
        public virtual Boolean Exitoso { get; set; }

        [Column("Error")]
        [DDisplayName("EnviaEmailLog.Error")]
        public virtual String Error { get; set; }

        [Column("ErrorDeDatos")]
        [DDisplayName("EnviaEmailLog.ErrorDeDatos")]
        public virtual String ErrorDeDatos { get; set; }

        [Column("Asunto")]
        [DDisplayName("EnviaEmailLog.Asunto")]
        [DRequired("EnviaEmailLog.Asunto")]
        [DStringLength("EnviaEmailLog.Asunto", 300)]
        public virtual String Asunto { get; set; }

        #endregion

        #region Reglas expression

        public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
        {
            Expression<Func<EnviaEmailLog, bool>> expression = entity => entity.Id == this.Id;
            return expression as Expression<Func<T, bool>>;
        }

        public override List<ExpRecurso> GetAdicionarExpression<T>()
        {
            var rules = new List<ExpRecurso>();
            Expression<Func<EnviaEmailLog, bool>> expression = null;

            return rules;
        }

        public override List<ExpRecurso> GetModificarExpression<T>()
        {
            var rules = new List<ExpRecurso>();
            Expression<Func<EnviaEmailLog, bool>> expression = null;

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
