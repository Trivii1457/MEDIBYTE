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
    /// Estados object for mapped table Estados.
    /// </summary>
    [Table("Estados")]
    public partial class Estados : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("Estados.Nombre")]
       [DRequired("Estados.Nombre")]
       [DStringLength("Estados.Nombre",100)]
       public virtual String Nombre { get; set; }

       [Column("Tipo")]
       [DDisplayName("Estados.Tipo")]
       [DRequired("Estados.Tipo")]
       [DStringLength("Estados.Tipo",30)]
       public virtual String Tipo { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Estados, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Estados, bool>> expression = null;

        expression = entity => entity.Nombre == this.Nombre && entity.Tipo == this.Tipo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Estados.Nombre" , "Estados.Tipo" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Estados, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Nombre == this.Nombre && entity.Tipo == this.Tipo)
                               && entity.Nombre == this.Nombre && entity.Tipo == this.Tipo;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Estados.Nombre" , "Estados.Tipo" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdministracionHonorariosLecturas, bool>> expression0 = entity => entity.TipoPagoLecturaEstadosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdministracionHonorariosLecturas"), typeof(AdministracionHonorariosLecturas)));

        Expression<Func<AdministracionHonorariosServicios, bool>> expression1 = entity => entity.TipoPagoConvenioEstadosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdministracionHonorariosServicios"), typeof(AdministracionHonorariosServicios)));

        Expression<Func<AdministracionHonorariosServicios, bool>> expression2 = entity => entity.TipoPagoParticularEstadosId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdministracionHonorariosServicios"), typeof(AdministracionHonorariosServicios)));

        Expression<Func<Admisiones, bool>> expression3 = entity => entity.ValorPagoEstadosId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<Admisiones, bool>> expression4 = entity => entity.EstadosId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<AtencionesResultado, bool>> expression5 = entity => entity.EstadosId == this.Id;
        rules.Add(new ExpRecurso(expression5.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AtencionesResultado"), typeof(AtencionesResultado)));

        Expression<Func<Consultorios, bool>> expression6 = entity => entity.EstadosId == this.Id;
        rules.Add(new ExpRecurso(expression6.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Consultorios"), typeof(Consultorios)));

        Expression<Func<Convenios, bool>> expression7 = entity => entity.EstadosId == this.Id;
        rules.Add(new ExpRecurso(expression7.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Convenios"), typeof(Convenios)));

        Expression<Func<Entidades, bool>> expression8 = entity => entity.EstadosId == this.Id;
        rules.Add(new ExpRecurso(expression8.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Entidades"), typeof(Entidades)));

        Expression<Func<Facturas, bool>> expression9 = entity => entity.Estadosid == this.Id;
        rules.Add(new ExpRecurso(expression9.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Facturas"), typeof(Facturas)));

        Expression<Func<Filiales, bool>> expression10 = entity => entity.EstadosId == this.Id;
        rules.Add(new ExpRecurso(expression10.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Filiales"), typeof(Filiales)));

        Expression<Func<HistoriasClinicas, bool>> expression11 = entity => entity.DominanciaEstadosId == this.Id;
        rules.Add(new ExpRecurso(expression11.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicas"), typeof(HistoriasClinicas)));

        Expression<Func<HistoriasClinicas, bool>> expression12 = entity => entity.EstadosId == this.Id;
        rules.Add(new ExpRecurso(expression12.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicas"), typeof(HistoriasClinicas)));

        Expression<Func<LiquidacionHonorarios, bool>> expression13 = entity => entity.EstadosId == this.Id;
        rules.Add(new ExpRecurso(expression13.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","LiquidacionHonorarios"), typeof(LiquidacionHonorarios)));

        Expression<Func<ListaPrecios, bool>> expression14 = entity => entity.TipoEstadosId == this.Id;
        rules.Add(new ExpRecurso(expression14.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ListaPrecios"), typeof(ListaPrecios)));

        Expression<Func<Notas, bool>> expression15 = entity => entity.Estadosid == this.Id;
        rules.Add(new ExpRecurso(expression15.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Notas"), typeof(Notas)));

        Expression<Func<ProgramacionCitas, bool>> expression16 = entity => entity.EstadosIdTipoDuracion == this.Id;
        rules.Add(new ExpRecurso(expression16.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionCitas"), typeof(ProgramacionCitas)));

        Expression<Func<ProgramacionCitas, bool>> expression17 = entity => entity.EstadosId == this.Id;
        rules.Add(new ExpRecurso(expression17.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionCitas"), typeof(ProgramacionCitas)));

        Expression<Func<Sedes, bool>> expression18 = entity => entity.EstadosId == this.Id;
        rules.Add(new ExpRecurso(expression18.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Sedes"), typeof(Sedes)));

        Expression<Func<Servicios, bool>> expression19 = entity => entity.EstadosIdTipoDuracion == this.Id;
        rules.Add(new ExpRecurso(expression19.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Servicios"), typeof(Servicios)));

        Expression<Func<Servicios, bool>> expression20 = entity => entity.EstadosId == this.Id;
        rules.Add(new ExpRecurso(expression20.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Servicios"), typeof(Servicios)));

        Expression<Func<ServiciosSedes, bool>> expression21 = entity => entity.EstadosId == this.Id;
        rules.Add(new ExpRecurso(expression21.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ServiciosSedes"), typeof(ServiciosSedes)));

       return rules;
       }

       #endregion
    }
 }
