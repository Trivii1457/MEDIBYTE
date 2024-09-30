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
    /// ImagenesDiagnosticas object for mapped table ImagenesDiagnosticas.
    /// </summary>
    [Table("ImagenesDiagnosticas")]
    public partial class ImagenesDiagnosticas : BaseEntity
    {

       #region Columnas normales)

       [Column("Fecha", TypeName = "datetime")]
       [DDisplayName("ImagenesDiagnosticas.Fecha")]
       [DRequired("ImagenesDiagnosticas.Fecha")]
       public virtual DateTime Fecha { get; set; }

       [Column("Observaciones")]
       [DDisplayName("ImagenesDiagnosticas.Observaciones")]
       [DStringLength("ImagenesDiagnosticas.Observaciones",1000)]
       public virtual String Observaciones { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("PacientesId")]
       [DDisplayName("ImagenesDiagnosticas.PacientesId")]
       [DRequired("ImagenesDiagnosticas.PacientesId")]
       [DRequiredFK("ImagenesDiagnosticas.PacientesId")]
       public virtual Int64 PacientesId { get; set; }

       [Column("ServiciosId")]
       [DDisplayName("ImagenesDiagnosticas.ServiciosId")]
       [DRequired("ImagenesDiagnosticas.ServiciosId")]
       [DRequiredFK("ImagenesDiagnosticas.ServiciosId")]
       public virtual Int64 ServiciosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("PacientesId")]
       public virtual Pacientes Pacientes { get; set; }

       [ForeignKey("ServiciosId")]
       public virtual Servicios Servicios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ImagenesDiagnosticas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ImagenesDiagnosticas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ImagenesDiagnosticas, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ImagenesDiagnosticasDetalle, bool>> expression0 = entity => entity.ImagenesDiagnosticasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ImagenesDiagnosticasDetalle"), typeof(ImagenesDiagnosticasDetalle)));

       return rules;
       }

       #endregion
    }
 }
