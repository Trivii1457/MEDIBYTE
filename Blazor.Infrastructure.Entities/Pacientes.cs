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
    /// Pacientes object for mapped table Pacientes.
    /// </summary>
    [Table("Pacientes")]
    public partial class Pacientes : BaseEntity
    {

       #region Columnas normales)

       [Column("CorreoElectronico")]
       [DDisplayName("Pacientes.CorreoElectronico")]
       [DStringLength("Pacientes.CorreoElectronico",200)]
       public virtual String CorreoElectronico { get; set; }

       [Column("Observaciones")]
       [DDisplayName("Pacientes.Observaciones")]
       public virtual String Observaciones { get; set; }

       [Column("Telefono")]
       [DDisplayName("Pacientes.Telefono")]
       [DRequired("Pacientes.Telefono")]
       [DStringLength("Pacientes.Telefono",45)]
       public virtual String Telefono { get; set; }

       [Column("Direccion")]
       [DDisplayName("Pacientes.Direccion")]
       [DRequired("Pacientes.Direccion")]
       [DStringLength("Pacientes.Direccion",250)]
       public virtual String Direccion { get; set; }

       [Column("PrimerNombre")]
       [DDisplayName("Pacientes.PrimerNombre")]
       [DRequired("Pacientes.PrimerNombre")]
       [DStringLength("Pacientes.PrimerNombre",150)]
       public virtual String PrimerNombre { get; set; }

       [Column("SegundoNombre")]
       [DDisplayName("Pacientes.SegundoNombre")]
       [DStringLength("Pacientes.SegundoNombre",150)]
       public virtual String SegundoNombre { get; set; }

       [Column("PrimerApellido")]
       [DDisplayName("Pacientes.PrimerApellido")]
       [DRequired("Pacientes.PrimerApellido")]
       [DStringLength("Pacientes.PrimerApellido",150)]
       public virtual String PrimerApellido { get; set; }

       [Column("SegundoApellido")]
       [DDisplayName("Pacientes.SegundoApellido")]
       [DStringLength("Pacientes.SegundoApellido",150)]
       public virtual String SegundoApellido { get; set; }

       [Column("NumeroIdentificacion")]
       [DDisplayName("Pacientes.NumeroIdentificacion")]
       [DRequired("Pacientes.NumeroIdentificacion")]
       [DStringLength("Pacientes.NumeroIdentificacion",45)]
       public virtual String NumeroIdentificacion { get; set; }

       [Column("DV")]
       [DDisplayName("Pacientes.DV")]
       [DRequired("Pacientes.DV")]
       [DStringLength("Pacientes.DV",2)]
       public virtual String DV { get; set; }

       [Column("FechaNacimiento", TypeName = "datetime")]
       [DDisplayName("Pacientes.FechaNacimiento")]
       [DRequired("Pacientes.FechaNacimiento")]
       public virtual DateTime FechaNacimiento { get; set; }

       [Column("NombreCompleto")]
       [DDisplayName("Pacientes.NombreCompleto")]
       [DRequired("Pacientes.NombreCompleto")]
       [DStringLength("Pacientes.NombreCompleto",600)]
       public virtual String NombreCompleto { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("CiudadesId")]
       [DDisplayName("Pacientes.CiudadesId")]
       [DRequired("Pacientes.CiudadesId")]
       [DRequiredFK("Pacientes.CiudadesId")]
       public virtual Int64 CiudadesId { get; set; }

       [Column("TiposAfiliadosId")]
       [DDisplayName("Pacientes.TiposAfiliadosId")]
       public virtual Int64? TiposAfiliadosId { get; set; }

       [Column("TiposRegimenesId")]
       [DDisplayName("Pacientes.TiposRegimenesId")]
       public virtual Int64? TiposRegimenesId { get; set; }

       [Column("CategoriasIngresosId")]
       [DDisplayName("Pacientes.CategoriasIngresosId")]
       public virtual Int64? CategoriasIngresosId { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("Pacientes.EmpresasId")]
       [DRequired("Pacientes.EmpresasId")]
       [DRequiredFK("Pacientes.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("TiposIdentificacionId")]
       [DDisplayName("Pacientes.TiposIdentificacionId")]
       [DRequired("Pacientes.TiposIdentificacionId")]
       [DRequiredFK("Pacientes.TiposIdentificacionId")]
       public virtual Int64 TiposIdentificacionId { get; set; }

       [Column("GenerosId")]
       [DDisplayName("Pacientes.GenerosId")]
       [DRequired("Pacientes.GenerosId")]
       [DRequiredFK("Pacientes.GenerosId")]
       public virtual Int64 GenerosId { get; set; }

       [Column("EstadosCivilesId")]
       [DDisplayName("Pacientes.EstadosCivilesId")]
       public virtual Int64? EstadosCivilesId { get; set; }

       [Column("TiposSangreId")]
       [DDisplayName("Pacientes.TiposSangreId")]
       public virtual Int64? TiposSangreId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("CategoriasIngresosId")]
       public virtual CategoriasIngresos CategoriasIngresos { get; set; }

       [ForeignKey("CiudadesId")]
       public virtual Ciudades Ciudades { get; set; }

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("EstadosCivilesId")]
       public virtual EstadosCiviles EstadosCiviles { get; set; }

       [ForeignKey("GenerosId")]
       public virtual Generos Generos { get; set; }

       [ForeignKey("TiposAfiliadosId")]
       public virtual TiposAfiliados TiposAfiliados { get; set; }

       [ForeignKey("TiposIdentificacionId")]
       public virtual TiposIdentificacion TiposIdentificacion { get; set; }

       [ForeignKey("TiposRegimenesId")]
       public virtual TiposRegimenes TiposRegimenes { get; set; }

       [ForeignKey("TiposSangreId")]
       public virtual TiposSangre TiposSangre { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Pacientes, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Pacientes, bool>> expression = null;

        expression = entity => entity.TiposIdentificacionId == this.TiposIdentificacionId && entity.NumeroIdentificacion == this.NumeroIdentificacion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Pacientes.TiposIdentificacionId" , "Pacientes.NumeroIdentificacion" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Pacientes, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.TiposIdentificacionId == this.TiposIdentificacionId && entity.NumeroIdentificacion == this.NumeroIdentificacion)
                               && entity.TiposIdentificacionId == this.TiposIdentificacionId && entity.NumeroIdentificacion == this.NumeroIdentificacion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Pacientes.TiposIdentificacionId" , "Pacientes.NumeroIdentificacion" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Admisiones, bool>> expression0 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<EntregaResultados, bool>> expression1 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EntregaResultados"), typeof(EntregaResultados)));

        Expression<Func<Facturas, bool>> expression2 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Facturas"), typeof(Facturas)));

        Expression<Func<FacturasGeneracion, bool>> expression3 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","FacturasGeneracion"), typeof(FacturasGeneracion)));

        Expression<Func<HistoriasClinicas, bool>> expression4 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicas"), typeof(HistoriasClinicas)));

        Expression<Func<ImagenesDiagnosticas, bool>> expression5 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression5.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ImagenesDiagnosticas"), typeof(ImagenesDiagnosticas)));

        Expression<Func<Incapacidades, bool>> expression6 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression6.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Incapacidades"), typeof(Incapacidades)));

        Expression<Func<IndicacionesMedicas, bool>> expression7 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression7.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","IndicacionesMedicas"), typeof(IndicacionesMedicas)));

        Expression<Func<Notas, bool>> expression8 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression8.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Notas"), typeof(Notas)));

        Expression<Func<NotasDetalles, bool>> expression9 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression9.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","NotasDetalles"), typeof(NotasDetalles)));

        Expression<Func<OrdenesMedicamentos, bool>> expression10 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression10.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesMedicamentos"), typeof(OrdenesMedicamentos)));

        Expression<Func<OrdenesServicios, bool>> expression11 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression11.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesServicios"), typeof(OrdenesServicios)));

        Expression<Func<PacientesEntidades, bool>> expression12 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression12.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","PacientesEntidades"), typeof(PacientesEntidades)));

        Expression<Func<ProgramacionCitas, bool>> expression13 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression13.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionCitas"), typeof(ProgramacionCitas)));

        Expression<Func<Recaudos, bool>> expression14 = entity => entity.PacientesId == this.Id;
        rules.Add(new ExpRecurso(expression14.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Recaudos"), typeof(Recaudos)));

       return rules;
       }

       #endregion
    }
 }
