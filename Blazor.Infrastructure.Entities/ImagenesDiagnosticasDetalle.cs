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
    /// ImagenesDiagnosticasDetalle object for mapped table ImagenesDiagnosticasDetalle.
    /// </summary>
    [Table("ImagenesDiagnosticasDetalle")]
    public partial class ImagenesDiagnosticasDetalle : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("ImagenesDiagnosticasDetalle.Nombre")]
       [DRequired("ImagenesDiagnosticasDetalle.Nombre")]
       [DStringLength("ImagenesDiagnosticasDetalle.Nombre",1000)]
       public virtual String Nombre { get; set; }

       [Column("SubioAStorage")]
       [DDisplayName("ImagenesDiagnosticasDetalle.SubioAStorage")]
       [DRequired("ImagenesDiagnosticasDetalle.SubioAStorage")]
       public virtual Boolean SubioAStorage { get; set; }

       [Column("Peso")]
       [DDisplayName("ImagenesDiagnosticasDetalle.Peso")]
       [DRequired("ImagenesDiagnosticasDetalle.Peso")]
       public virtual Int64 Peso { get; set; }

       [Column("ErrorNoSubio")]
       [DDisplayName("ImagenesDiagnosticasDetalle.ErrorNoSubio")]
       [DStringLength("ImagenesDiagnosticasDetalle.ErrorNoSubio",2000)]
       public virtual String ErrorNoSubio { get; set; }

       [Column("NombreArchivoAzureBlob")]
       [DDisplayName("ImagenesDiagnosticasDetalle.NombreArchivoAzureBlob")]
       [DRequired("ImagenesDiagnosticasDetalle.NombreArchivoAzureBlob")]
       [DStringLength("ImagenesDiagnosticasDetalle.NombreArchivoAzureBlob",100)]
       public virtual String NombreArchivoAzureBlob { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("ImagenesDiagnosticasId")]
       [DDisplayName("ImagenesDiagnosticasDetalle.ImagenesDiagnosticasId")]
       public virtual Int64? ImagenesDiagnosticasId { get; set; }

       [Column("AdmisionesServiciosPrestadosId")]
       [DDisplayName("ImagenesDiagnosticasDetalle.AdmisionesServiciosPrestadosId")]
       public virtual Int64? AdmisionesServiciosPrestadosId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("AdmisionesServiciosPrestadosId")]
       public virtual AdmisionesServiciosPrestados AdmisionesServiciosPrestados { get; set; }

       [ForeignKey("ImagenesDiagnosticasId")]
       public virtual ImagenesDiagnosticas ImagenesDiagnosticas { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<ImagenesDiagnosticasDetalle, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ImagenesDiagnosticasDetalle, bool>> expression = null;

        expression = entity => entity.NombreArchivoAzureBlob == this.NombreArchivoAzureBlob;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ImagenesDiagnosticasDetalle.NombreArchivoAzureBlob" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<ImagenesDiagnosticasDetalle, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.NombreArchivoAzureBlob == this.NombreArchivoAzureBlob)
                               && entity.NombreArchivoAzureBlob == this.NombreArchivoAzureBlob;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "ImagenesDiagnosticasDetalle.NombreArchivoAzureBlob" )));

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
