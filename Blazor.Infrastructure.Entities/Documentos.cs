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
    /// Documentos object for mapped table Documentos.
    /// </summary>
    [Table("Documentos")]
    public partial class Documentos : BaseEntity
    {

       #region Columnas normales)

       [Column("Prefijo")]
       [DDisplayName("Documentos.Prefijo")]
       [DRequired("Documentos.Prefijo")]
       [DStringLength("Documentos.Prefijo",5)]
       public virtual String Prefijo { get; set; }

       [Column("Descripcion")]
       [DDisplayName("Documentos.Descripcion")]
       [DRequired("Documentos.Descripcion")]
       [DStringLength("Documentos.Descripcion",250)]
       public virtual String Descripcion { get; set; }

       [Column("ResolucionDian")]
       [DDisplayName("Documentos.ResolucionDian")]
       public virtual Int64? ResolucionDian { get; set; }

       [Column("ConsecutivoDesde")]
       [DDisplayName("Documentos.ConsecutivoDesde")]
       public virtual Int32? ConsecutivoDesde { get; set; }

       [Column("ConsecutivoHasta")]
       [DDisplayName("Documentos.ConsecutivoHasta")]
       public virtual Int32? ConsecutivoHasta { get; set; }

       [Column("FechaDesde", TypeName = "datetime")]
       [DDisplayName("Documentos.FechaDesde")]
       public virtual DateTime? FechaDesde { get; set; }

       [Column("FechaHasta", TypeName = "datetime")]
       [DDisplayName("Documentos.FechaHasta")]
       public virtual DateTime? FechaHasta { get; set; }

       [Column("Activo")]
       [DDisplayName("Documentos.Activo")]
       [DRequired("Documentos.Activo")]
       public virtual Boolean Activo { get; set; }

       [Column("LlaveUnica")]
       [DDisplayName("Documentos.LlaveUnica")]
       [DStringLength("Documentos.LlaveUnica",250)]
       public virtual String LlaveUnica { get; set; }

       [Column("Transaccion")]
       [DDisplayName("Documentos.Transaccion")]
       [DRequired("Documentos.Transaccion")]
       public virtual Int16 Transaccion { get; set; }

       [Column("Contingencia")]
       [DDisplayName("Documentos.Contingencia")]
       [DRequired("Documentos.Contingencia")]
       public virtual Boolean Contingencia { get; set; }

       [Column("Codigo85")]
       [DDisplayName("Documentos.Codigo85")]
       [DStringLength("Documentos.Codigo85",2)]
       public virtual String Codigo85 { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Documentos, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Documentos, bool>> expression = null;

        expression = entity => entity.Prefijo == this.Prefijo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Documentos.Prefijo" )));

        expression = entity => entity.Prefijo == this.Prefijo && entity.ResolucionDian == this.ResolucionDian;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Documentos.Prefijo" , "Documentos.ResolucionDian" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Documentos, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Prefijo == this.Prefijo)
                               && entity.Prefijo == this.Prefijo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Documentos.Prefijo" )));

        expression = entity => !(entity.Id == this.Id && entity.Prefijo == this.Prefijo && entity.ResolucionDian == this.ResolucionDian)
                               && entity.Prefijo == this.Prefijo && entity.ResolucionDian == this.ResolucionDian;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Documentos.Prefijo" , "Documentos.ResolucionDian" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Admisiones, bool>> expression0 = entity => entity.DocumentosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<Facturas, bool>> expression1 = entity => entity.DocumentosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Facturas"), typeof(Facturas)));

        Expression<Func<FacturasGeneracion, bool>> expression2 = entity => entity.DocumentosId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","FacturasGeneracion"), typeof(FacturasGeneracion)));

        Expression<Func<Notas, bool>> expression3 = entity => entity.DocumentosId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Notas"), typeof(Notas)));

        Expression<Func<NotasConceptos, bool>> expression4 = entity => entity.DocumentosId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","NotasConceptos"), typeof(NotasConceptos)));

        Expression<Func<SedesDocumentos, bool>> expression5 = entity => entity.DocumentosId == this.Id;
        rules.Add(new ExpRecurso(expression5.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","SedesDocumentos"), typeof(SedesDocumentos)));

       return rules;
       }

       #endregion
    }
 }
