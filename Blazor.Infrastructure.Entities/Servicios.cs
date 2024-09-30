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
    /// Servicios object for mapped table Servicios.
    /// </summary>
    [Table("Servicios")]
    public partial class Servicios : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("Servicios.Nombre")]
       [DRequired("Servicios.Nombre")]
       [DStringLength("Servicios.Nombre",500)]
       public virtual String Nombre { get; set; }

       [Column("CodigoInterno")]
       [DDisplayName("Servicios.CodigoInterno")]
       [DStringLength("Servicios.CodigoInterno",45)]
       public virtual String CodigoInterno { get; set; }

       [Column("Duracion")]
       [DDisplayName("Servicios.Duracion")]
       [DRequired("Servicios.Duracion")]
       public virtual Int64 Duracion { get; set; }

       [Column("Preparacion")]
       [DDisplayName("Servicios.Preparacion")]
       public virtual String Preparacion { get; set; }

       [Column("RequiereProfesional")]
       [DDisplayName("Servicios.RequiereProfesional")]
       [DRequired("Servicios.RequiereProfesional")]
       public virtual Boolean RequiereProfesional { get; set; }

       [Column("RequiereLecturaResultados")]
       [DDisplayName("Servicios.RequiereLecturaResultados")]
       [DRequired("Servicios.RequiereLecturaResultados")]
       public virtual Boolean RequiereLecturaResultados { get; set; }

       [Column("TarifaPlena")]
       [DDisplayName("Servicios.TarifaPlena")]
       [DRequired("Servicios.TarifaPlena")]
       public virtual Decimal TarifaPlena { get; set; }

       [Column("PorcImpuesto")]
       [DDisplayName("Servicios.PorcImpuesto")]
       [DRequired("Servicios.PorcImpuesto")]
       public virtual Decimal PorcImpuesto { get; set; }

       [Column("EsQuirurgico")]
       [DDisplayName("Servicios.EsQuirurgico")]
       [DRequired("Servicios.EsQuirurgico")]
       public virtual Boolean EsQuirurgico { get; set; }

       [Column("Codigo")]
       [DDisplayName("Servicios.Codigo")]
       [DRequired("Servicios.Codigo")]
       [DStringLength("Servicios.Codigo",45)]
       public virtual String Codigo { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("TiposServiciosId")]
       [DDisplayName("Servicios.TiposServiciosId")]
       [DRequired("Servicios.TiposServiciosId")]
       [DRequiredFK("Servicios.TiposServiciosId")]
       public virtual Int64 TiposServiciosId { get; set; }

       [Column("EspecialidadesId")]
       [DDisplayName("Servicios.EspecialidadesId")]
       [DRequired("Servicios.EspecialidadesId")]
       [DRequiredFK("Servicios.EspecialidadesId")]
       public virtual Int64 EspecialidadesId { get; set; }

       [Column("EstadosId")]
       [DDisplayName("Servicios.EstadosId")]
       [DRequired("Servicios.EstadosId")]
       [DRequiredFK("Servicios.EstadosId")]
       public virtual Int64 EstadosId { get; set; }

       [Column("CupsId")]
       [DDisplayName("Servicios.CupsId")]
       public virtual Int64? CupsId { get; set; }

       [Column("CategoriasServiciosId")]
       [DDisplayName("Servicios.CategoriasServiciosId")]
       [DRequired("Servicios.CategoriasServiciosId")]
       [DRequiredFK("Servicios.CategoriasServiciosId")]
       public virtual Int64 CategoriasServiciosId { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("Servicios.EmpresasId")]
       [DRequired("Servicios.EmpresasId")]
       [DRequiredFK("Servicios.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("TiposImpuestosId")]
       [DDisplayName("Servicios.TiposImpuestosId")]
       [DRequired("Servicios.TiposImpuestosId")]
       [DRequiredFK("Servicios.TiposImpuestosId")]
       public virtual Int64 TiposImpuestosId { get; set; }

       [Column("EstadosIdTipoDuracion")]
       [DDisplayName("Servicios.EstadosIdTipoDuracion")]
       [DRequired("Servicios.EstadosIdTipoDuracion")]
       [DRequiredFK("Servicios.EstadosIdTipoDuracion")]
       public virtual Int64 EstadosIdTipoDuracion { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EstadosIdTipoDuracion")]
       public virtual Estados EstadosIdTipoDuraci { get; set; }

       [ForeignKey("CategoriasServiciosId")]
       public virtual CategoriasServicios CategoriasServicios { get; set; }

       [ForeignKey("CupsId")]
       public virtual Cups Cups { get; set; }

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("EspecialidadesId")]
       public virtual Especialidades Especialidades { get; set; }

       [ForeignKey("TiposImpuestosId")]
       public virtual EsquemasImpuestos TiposImpuestos { get; set; }

       [ForeignKey("EstadosId")]
       public virtual Estados Estados { get; set; }

       [ForeignKey("TiposServiciosId")]
       public virtual TiposServicios TiposServicios { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Servicios, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Servicios, bool>> expression = null;

        expression = entity => entity.Codigo == this.Codigo && entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Servicios.Codigo" , "Servicios.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Servicios, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Codigo == this.Codigo && entity.Nombre == this.Nombre)
                               && entity.Codigo == this.Codigo && entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Servicios.Codigo" , "Servicios.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdministracionHonorariosLecturas, bool>> expression0 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdministracionHonorariosLecturas"), typeof(AdministracionHonorariosLecturas)));

        Expression<Func<AdministracionHonorariosServicios, bool>> expression1 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdministracionHonorariosServicios"), typeof(AdministracionHonorariosServicios)));

        Expression<Func<AdmisionesServiciosPrestados, bool>> expression2 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdmisionesServiciosPrestados"), typeof(AdmisionesServiciosPrestados)));

        Expression<Func<ConveniosServicios, bool>> expression3 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ConveniosServicios"), typeof(ConveniosServicios)));

        Expression<Func<FacturasDetalles, bool>> expression4 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","FacturasDetalles"), typeof(FacturasDetalles)));

        Expression<Func<ImagenesDiagnosticas, bool>> expression5 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression5.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ImagenesDiagnosticas"), typeof(ImagenesDiagnosticas)));

        Expression<Func<NotasDetalles, bool>> expression7 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression7.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","NotasDetalles"), typeof(NotasDetalles)));

        Expression<Func<PreciosServicios, bool>> expression8 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression8.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","PreciosServicios"), typeof(PreciosServicios)));

        Expression<Func<ProgramacionCitas, bool>> expression9 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression9.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionCitas"), typeof(ProgramacionCitas)));

        Expression<Func<ProgramacionDisponible, bool>> expression10 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression10.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionDisponible"), typeof(ProgramacionDisponible)));

        Expression<Func<ServiciosConsultorios, bool>> expression11 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression11.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ServiciosConsultorios"), typeof(ServiciosConsultorios)));

        Expression<Func<ServiciosEmpleados, bool>> expression12 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression12.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ServiciosEmpleados"), typeof(ServiciosEmpleados)));

        Expression<Func<ServiciosSedes, bool>> expression13 = entity => entity.ServiciosId == this.Id;
        rules.Add(new ExpRecurso(expression13.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ServiciosSedes"), typeof(ServiciosSedes)));

       return rules;
       }

       #endregion
    }
 }
