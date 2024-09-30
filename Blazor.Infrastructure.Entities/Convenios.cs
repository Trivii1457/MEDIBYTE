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
    /// Convenios object for mapped table Convenios.
    /// </summary>
    [Table("Convenios")]
    public partial class Convenios : BaseEntity
    {

       #region Columnas normales)

       [Column("Codigo")]
       [DDisplayName("Convenios.Codigo")]
       [DRequired("Convenios.Codigo")]
       [DStringLength("Convenios.Codigo",45)]
       public virtual String Codigo { get; set; }

       [Column("FechaCaducidad", TypeName = "datetime")]
       [DDisplayName("Convenios.FechaCaducidad")]
       [DRequired("Convenios.FechaCaducidad")]
       public virtual DateTime FechaCaducidad { get; set; }

       [Column("Observaciones")]
       [DDisplayName("Convenios.Observaciones")]
       [DStringLength("Convenios.Observaciones",2000)]
       public virtual String Observaciones { get; set; }

       [Column("DiaFacturacionMes")]
       [DDisplayName("Convenios.DiaFacturacionMes")]
       public virtual Int64? DiaFacturacionMes { get; set; }

       [Column("PeriodicidadPago")]
       [DDisplayName("Convenios.PeriodicidadPago")]
       public virtual Int64? PeriodicidadPago { get; set; }

       [Column("FacturarCodigoInterno")]
       [DDisplayName("Convenios.FacturarCodigoInterno")]
       [DRequired("Convenios.FacturarCodigoInterno")]
       public virtual Boolean FacturarCodigoInterno { get; set; }

       [Column("Nombre")]
       [DDisplayName("Convenios.Nombre")]
       [DRequired("Convenios.Nombre")]
       [DStringLength("Convenios.Nombre",255)]
       public virtual String Nombre { get; set; }

       [Column("FechaInicio", TypeName = "datetime")]
       [DDisplayName("Convenios.FechaInicio")]
       [DRequired("Convenios.FechaInicio")]
       public virtual DateTime FechaInicio { get; set; }

       [Column("NroPacientes")]
       [DDisplayName("Convenios.NroPacientes")]
       public virtual Int32? NroPacientes { get; set; }

       [Column("RequierePacientes")]
       [DDisplayName("Convenios.RequierePacientes")]
       [DRequired("Convenios.RequierePacientes")]
       public virtual Boolean RequierePacientes { get; set; }

       [Column("RequierePresupuesto")]
       [DDisplayName("Convenios.RequierePresupuesto")]
       [DRequired("Convenios.RequierePresupuesto")]
       public virtual Boolean RequierePresupuesto { get; set; }

       [Column("Presupuesto")]
       [DDisplayName("Convenios.Presupuesto")]
       public virtual Decimal? Presupuesto { get; set; }

       [Column("NroPacientesRestantes")]
       [DDisplayName("Convenios.NroPacientesRestantes")]
       public virtual Int32? NroPacientesRestantes { get; set; }

       [Column("SaldoPresupuesto")]
       [DDisplayName("Convenios.SaldoPresupuesto")]
       public virtual Decimal? SaldoPresupuesto { get; set; }

       [Column("FormasPagosId")]
       [DDisplayName("Convenios.FormasPagosId")]
       [DRequired("Convenios.FormasPagosId")]
       public virtual Int64 FormasPagosId { get; set; }

       [Column("TipoFacturaEstadosId")]
       [DDisplayName("Convenios.TipoFacturaEstadosId")]
       [DRequired("Convenios.TipoFacturaEstadosId")]
       public virtual Int64 TipoFacturaEstadosId { get; set; }

       [Column("NroContrato")]
       [DDisplayName("Convenios.NroContrato")]
       [DStringLength("Convenios.NroContrato",100)]
       public virtual String NroContrato { get; set; }

       [Column("NroPoliza")]
       [DDisplayName("Convenios.NroPoliza")]
       [DStringLength("Convenios.NroPoliza",100)]
       public virtual String NroPoliza { get; set; }

       [Column("PlanBeneficio")]
       [DDisplayName("Convenios.PlanBeneficio")]
       [DStringLength("Convenios.PlanBeneficio",250)]
       public virtual String PlanBeneficio { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("EntidadesId")]
       [DDisplayName("Convenios.EntidadesId")]
       [DRequired("Convenios.EntidadesId")]
       [DRequiredFK("Convenios.EntidadesId")]
       public virtual Int64 EntidadesId { get; set; }

       [Column("EstadosId")]
       [DDisplayName("Convenios.EstadosId")]
       [DRequired("Convenios.EstadosId")]
       [DRequiredFK("Convenios.EstadosId")]
       public virtual Int64 EstadosId { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("Convenios.EmpresasId")]
       [DRequired("Convenios.EmpresasId")]
       [DRequiredFK("Convenios.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("ListaPreciosId")]
       [DDisplayName("Convenios.ListaPreciosId")]
       public virtual Int64? ListaPreciosId { get; set; }

       [Column("ModalidadesContratacionId")]
       [DDisplayName("Convenios.ModalidadesContratacionId")]
       [DRequired("Convenios.ModalidadesContratacionId")]
       [DRequiredFK("Convenios.ModalidadesContratacionId")]
       public virtual Int64 ModalidadesContratacionId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EntidadesId")]
       public virtual Entidades Entidades { get; set; }

       [ForeignKey("EstadosId")]
       public virtual Estados Estados { get; set; }

       [ForeignKey("ListaPreciosId")]
       public virtual ListaPrecios ListaPrecios { get; set; }

       [ForeignKey("ModalidadesContratacionId")]
       public virtual ModalidadesContratacion ModalidadesContratacion { get; set; }

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Convenios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Convenios, bool>> expression = null;

        expression = entity => entity.EmpresasId == this.EmpresasId && entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Convenios.EmpresasId" , "Convenios.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Convenios, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.EmpresasId == this.EmpresasId && entity.Nombre == this.Nombre)
                               && entity.EmpresasId == this.EmpresasId && entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Convenios.EmpresasId" , "Convenios.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Admisiones, bool>> expression0 = entity => entity.ConveniosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<ConveniosServicios, bool>> expression1 = entity => entity.ConveniosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ConveniosServicios"), typeof(ConveniosServicios)));

        Expression<Func<Facturas, bool>> expression2 = entity => entity.ConvenioId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Facturas"), typeof(Facturas)));

        Expression<Func<ProgramacionCitas, bool>> expression3 = entity => entity.ConveniosId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionCitas"), typeof(ProgramacionCitas)));

       return rules;
       }

       #endregion
    }
 }
