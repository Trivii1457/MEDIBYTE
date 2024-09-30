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
    /// EntregaResultados object for mapped table EntregaResultados.
    /// </summary>
    [Table("EntregaResultados")]
    public partial class EntregaResultados : BaseEntity
    {

       #region Columnas normales)

       [Column("Fecha", TypeName = "datetime")]
       [DDisplayName("EntregaResultados.Fecha")]
       [DRequired("EntregaResultados.Fecha")]
       public virtual DateTime Fecha { get; set; }

       [Column("NumeroIdentificacion")]
       [DDisplayName("EntregaResultados.NumeroIdentificacion")]
       [DRequired("EntregaResultados.NumeroIdentificacion")]
       [DStringLength("EntregaResultados.NumeroIdentificacion",20)]
       public virtual String NumeroIdentificacion { get; set; }

       [Column("Nombres")]
       [DDisplayName("EntregaResultados.Nombres")]
       [DRequired("EntregaResultados.Nombres")]
       [DStringLength("EntregaResultados.Nombres",60)]
       public virtual String Nombres { get; set; }

       [Column("Apellidos")]
       [DDisplayName("EntregaResultados.Apellidos")]
       [DRequired("EntregaResultados.Apellidos")]
       [DStringLength("EntregaResultados.Apellidos",60)]
       public virtual String Apellidos { get; set; }

       [Column("Telefono")]
       [DDisplayName("EntregaResultados.Telefono")]
       [DStringLength("EntregaResultados.Telefono",20)]
       public virtual String Telefono { get; set; }

       [Column("Observaciones")]
       [DDisplayName("EntregaResultados.Observaciones")]
       [DStringLength("EntregaResultados.Observaciones",1024)]
       public virtual String Observaciones { get; set; }

       [Column("Email")]
       [DDisplayName("EntregaResultados.Email")]
       [DStringLength("EntregaResultados.Email",255)]
       public virtual String Email { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("TiposIdentificacionid")]
       [DDisplayName("EntregaResultados.TiposIdentificacionid")]
       [DRequired("EntregaResultados.TiposIdentificacionid")]
       [DRequiredFK("EntregaResultados.TiposIdentificacionid")]
       public virtual Int64 TiposIdentificacionid { get; set; }

       [Column("ParentescosId")]
       [DDisplayName("EntregaResultados.ParentescosId")]
       [DRequired("EntregaResultados.ParentescosId")]
       [DRequiredFK("EntregaResultados.ParentescosId")]
       public virtual Int64 ParentescosId { get; set; }

       [Column("MediosEntragasId")]
       [DDisplayName("EntregaResultados.MediosEntragasId")]
       [DRequired("EntregaResultados.MediosEntragasId")]
       [DRequiredFK("EntregaResultados.MediosEntragasId")]
       public virtual Int64 MediosEntragasId { get; set; }

       [Column("ContanciaArchivosId")]
       [DDisplayName("EntregaResultados.ContanciaArchivosId")]
       public virtual Int64? ContanciaArchivosId { get; set; }

       [Column("PacientesId")]
       [DDisplayName("EntregaResultados.PacientesId")]
       [DRequired("EntregaResultados.PacientesId")]
       [DRequiredFK("EntregaResultados.PacientesId")]
       public virtual Int64 PacientesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("ContanciaArchivosId")]
       public virtual Archivos ContanciaArchivos { get; set; }

       [ForeignKey("MediosEntragasId")]
       public virtual MediosEntregas MediosEntragas { get; set; }

       [ForeignKey("PacientesId")]
       public virtual Pacientes Pacientes { get; set; }

       [ForeignKey("ParentescosId")]
       public virtual Parentescos Parentescos { get; set; }

       [ForeignKey("TiposIdentificacionid")]
       public virtual TiposIdentificacion TiposIdentificacion { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<EntregaResultados, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EntregaResultados, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EntregaResultados, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EntregaResultadosDetalles, bool>> expression0 = entity => entity.EntregaResultadosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EntregaResultadosDetalles"), typeof(EntregaResultadosDetalles)));

       return rules;
       }

       #endregion
    }
 }
