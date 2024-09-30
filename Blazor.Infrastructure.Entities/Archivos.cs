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
    /// Archivos object for mapped table Archivos.
    /// </summary>
    [Table("Archivos")]
    public partial class Archivos : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("Archivos.Nombre")]
       [DStringLength("Archivos.Nombre",100)]
       public virtual String Nombre { get; set; }

       [Column("TipoContenido")]
       [DDisplayName("Archivos.TipoContenido")]
       [DStringLength("Archivos.TipoContenido",100)]
       public virtual String TipoContenido { get; set; }

       [Column("Archivo")]
       [DDisplayName("Archivos.Archivo")]
       public virtual Byte[] Archivo { get; set; }

       [Column("Maestro")]
       [DDisplayName("Archivos.Maestro")]
       [DRequired("Archivos.Maestro")]
       [DStringLength("Archivos.Maestro",100)]
       public virtual String Maestro { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Archivos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Archivos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Archivos, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Admisiones, bool>> expression0 = entity => entity.ExoneracionArchivoId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<Empleados, bool>> expression1 = entity => entity.FirmaDigitalArchivoId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Empleados"), typeof(Empleados)));

        Expression<Func<Empleados, bool>> expression2 = entity => entity.SelloDigitalArchivoId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Empleados"), typeof(Empleados)));

        Expression<Func<Empresas, bool>> expression3 = entity => entity.LogoArchivosId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Empresas"), typeof(Empresas)));

        Expression<Func<EntregaResultados, bool>> expression4 = entity => entity.ContanciaArchivosId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EntregaResultados"), typeof(EntregaResultados)));

        Expression<Func<RadicacionCuentas, bool>> expression5 = entity => entity.RadicacionArchivosId == this.Id;
        rules.Add(new ExpRecurso(expression5.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","RadicacionCuentas"), typeof(RadicacionCuentas)));

       return rules;
       }

       #endregion
    }
 }
