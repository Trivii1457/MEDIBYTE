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
    /// Empleados object for mapped table Empleados.
    /// </summary>
    [Table("Empleados")]
    public partial class Empleados : BaseEntity
    {

       #region Columnas normales)

       [Column("RegistroMedico")]
       [DDisplayName("Empleados.RegistroMedico")]
       [DStringLength("Empleados.RegistroMedico",60)]
       public virtual String RegistroMedico { get; set; }

       [Column("Telefono")]
       [DDisplayName("Empleados.Telefono")]
       [DStringLength("Empleados.Telefono",45)]
       public virtual String Telefono { get; set; }

       [Column("SegundoTelefono")]
       [DDisplayName("Empleados.SegundoTelefono")]
       [DStringLength("Empleados.SegundoTelefono",45)]
       public virtual String SegundoTelefono { get; set; }

       [Column("Direccion")]
       [DDisplayName("Empleados.Direccion")]
       [DStringLength("Empleados.Direccion",250)]
       public virtual String Direccion { get; set; }

       [Column("SegundaDireccion")]
       [DDisplayName("Empleados.SegundaDireccion")]
       [DStringLength("Empleados.SegundaDireccion",250)]
       public virtual String SegundaDireccion { get; set; }

       [Column("AutorizaDescuento")]
       [DDisplayName("Empleados.AutorizaDescuento")]
       [DRequired("Empleados.AutorizaDescuento")]
       public virtual Boolean AutorizaDescuento { get; set; }

       [Column("PrimerNombre")]
       [DDisplayName("Empleados.PrimerNombre")]
       [DRequired("Empleados.PrimerNombre")]
       [DStringLength("Empleados.PrimerNombre",150)]
       public virtual String PrimerNombre { get; set; }

       [Column("SegundoNombre")]
       [DDisplayName("Empleados.SegundoNombre")]
       [DStringLength("Empleados.SegundoNombre",150)]
       public virtual String SegundoNombre { get; set; }

       [Column("PrimerApellido")]
       [DDisplayName("Empleados.PrimerApellido")]
       [DRequired("Empleados.PrimerApellido")]
       [DStringLength("Empleados.PrimerApellido",150)]
       public virtual String PrimerApellido { get; set; }

       [Column("SegundoApellido")]
       [DDisplayName("Empleados.SegundoApellido")]
       [DStringLength("Empleados.SegundoApellido",150)]
       public virtual String SegundoApellido { get; set; }

       [Column("NumeroIdentificacion")]
       [DDisplayName("Empleados.NumeroIdentificacion")]
       [DRequired("Empleados.NumeroIdentificacion")]
       [DStringLength("Empleados.NumeroIdentificacion",45)]
       public virtual String NumeroIdentificacion { get; set; }

       [Column("DV")]
       [DDisplayName("Empleados.DV")]
       [DRequired("Empleados.DV")]
       [DStringLength("Empleados.DV",2)]
       public virtual String DV { get; set; }

       [Column("FechaNacimiento", TypeName = "datetime")]
       [DDisplayName("Empleados.FechaNacimiento")]
       [DRequired("Empleados.FechaNacimiento")]
       public virtual DateTime FechaNacimiento { get; set; }

       [Column("NombreCompleto")]
       [DDisplayName("Empleados.NombreCompleto")]
       [DRequired("Empleados.NombreCompleto")]
       [DStringLength("Empleados.NombreCompleto",600)]
       public virtual String NombreCompleto { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("TipoEmpleados")]
       [DDisplayName("Empleados.TipoEmpleados")]
       [DRequired("Empleados.TipoEmpleados")]
       [DRequiredFK("Empleados.TipoEmpleados")]
       public virtual Int64 TipoEmpleados { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("Empleados.EmpresasId")]
       [DRequired("Empleados.EmpresasId")]
       [DRequiredFK("Empleados.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       [Column("UserId")]
       [DDisplayName("Empleados.UserId")]
       public virtual Int64? UserId { get; set; }

       [Column("FirmaDigitalArchivoId")]
       [DDisplayName("Empleados.FirmaDigitalArchivoId")]
       public virtual Int64? FirmaDigitalArchivoId { get; set; }

       [Column("SelloDigitalArchivoId")]
       [DDisplayName("Empleados.SelloDigitalArchivoId")]
       public virtual Int64? SelloDigitalArchivoId { get; set; }

       [Column("TiposIdentificacionId")]
       [DDisplayName("Empleados.TiposIdentificacionId")]
       [DRequired("Empleados.TiposIdentificacionId")]
       [DRequiredFK("Empleados.TiposIdentificacionId")]
       public virtual Int64 TiposIdentificacionId { get; set; }

       [Column("GenerosId")]
       [DDisplayName("Empleados.GenerosId")]
       [DRequired("Empleados.GenerosId")]
       [DRequiredFK("Empleados.GenerosId")]
       public virtual Int64 GenerosId { get; set; }

       [Column("EstadosCivilesId")]
       [DDisplayName("Empleados.EstadosCivilesId")]
       public virtual Int64? EstadosCivilesId { get; set; }

       [Column("TiposSangreId")]
       [DDisplayName("Empleados.TiposSangreId")]
       public virtual Int64? TiposSangreId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("EstadosCivilesId")]
       public virtual EstadosCiviles EstadosCiviles { get; set; }

       [ForeignKey("FirmaDigitalArchivoId")]
       public virtual Archivos FirmaDigitalArchivo { get; set; }

       [ForeignKey("GenerosId")]
       public virtual Generos Generos { get; set; }

       [ForeignKey("SelloDigitalArchivoId")]
       public virtual Archivos SelloDigitalArchivo { get; set; }

       [ForeignKey("TipoEmpleados")]
       public virtual TipoEmpleados TipoEmplead { get; set; }

       [ForeignKey("TiposIdentificacionId")]
       public virtual TiposIdentificacion TiposIdentificacion { get; set; }

       [ForeignKey("TiposSangreId")]
       public virtual TiposSangre TiposSangre { get; set; }

       [ForeignKey("UserId")]
       public virtual User User { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Empleados, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Empleados, bool>> expression = null;

        expression = entity => entity.TiposIdentificacionId == this.TiposIdentificacionId && entity.NumeroIdentificacion == this.NumeroIdentificacion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Empleados.TiposIdentificacionId" , "Empleados.NumeroIdentificacion" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Empleados, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.TiposIdentificacionId == this.TiposIdentificacionId && entity.NumeroIdentificacion == this.NumeroIdentificacion)
                               && entity.TiposIdentificacionId == this.TiposIdentificacionId && entity.NumeroIdentificacion == this.NumeroIdentificacion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Empleados.TiposIdentificacionId" , "Empleados.NumeroIdentificacion" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdministracionHonorarios, bool>> expression0 = entity => entity.EmpleadosId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdministracionHonorarios"), typeof(AdministracionHonorarios)));

        Expression<Func<Atenciones, bool>> expression1 = entity => entity.EmpleadosId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Atenciones"), typeof(Atenciones)));

        Expression<Func<AtencionesResultado, bool>> expression2 = entity => entity.EmpleadoId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AtencionesResultado"), typeof(AtencionesResultado)));

        Expression<Func<EmpleadoProfesiones, bool>> expression3 = entity => entity.EmpleadosId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EmpleadoProfesiones"), typeof(EmpleadoProfesiones)));

        Expression<Func<EmpleadosEspecialidades, bool>> expression4 = entity => entity.EmpleadosId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EmpleadosEspecialidades"), typeof(EmpleadosEspecialidades)));

        Expression<Func<HistoriasClinicas, bool>> expression5 = entity => entity.ProfesionalId == this.Id;
        rules.Add(new ExpRecurso(expression5.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicas"), typeof(HistoriasClinicas)));

        Expression<Func<HistoriasClinicasNotasAclaratorias, bool>> expression6 = entity => entity.ProfesionalId == this.Id;
        rules.Add(new ExpRecurso(expression6.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HistoriasClinicasNotasAclaratorias"), typeof(HistoriasClinicasNotasAclaratorias)));

        Expression<Func<Incapacidades, bool>> expression7 = entity => entity.ProfesionalId == this.Id;
        rules.Add(new ExpRecurso(expression7.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Incapacidades"), typeof(Incapacidades)));

        Expression<Func<IndicacionesMedicas, bool>> expression8 = entity => entity.ProfesionalId == this.Id;
        rules.Add(new ExpRecurso(expression8.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","IndicacionesMedicas"), typeof(IndicacionesMedicas)));

        Expression<Func<LiquidacionHonorarios, bool>> expression9 = entity => entity.EmpleadosId == this.Id;
        rules.Add(new ExpRecurso(expression9.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","LiquidacionHonorarios"), typeof(LiquidacionHonorarios)));

        Expression<Func<LiquidacionHonorariosDetalle, bool>> expression10 = entity => entity.EmpleadosId == this.Id;
        rules.Add(new ExpRecurso(expression10.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","LiquidacionHonorariosDetalle"), typeof(LiquidacionHonorariosDetalle)));

        Expression<Func<OrdenesMedicamentos, bool>> expression11 = entity => entity.ProfesionalId == this.Id;
        rules.Add(new ExpRecurso(expression11.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesMedicamentos"), typeof(OrdenesMedicamentos)));

        Expression<Func<OrdenesServicios, bool>> expression12 = entity => entity.ProfesionalId == this.Id;
        rules.Add(new ExpRecurso(expression12.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","OrdenesServicios"), typeof(OrdenesServicios)));

        Expression<Func<ProgramacionCitas, bool>> expression13 = entity => entity.EmpleadosId == this.Id;
        rules.Add(new ExpRecurso(expression13.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionCitas"), typeof(ProgramacionCitas)));

        Expression<Func<ProgramacionDisponible, bool>> expression14 = entity => entity.EmpleadosId == this.Id;
        rules.Add(new ExpRecurso(expression14.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionDisponible"), typeof(ProgramacionDisponible)));

        Expression<Func<ServiciosEmpleados, bool>> expression15 = entity => entity.EmpleadosId == this.Id;
        rules.Add(new ExpRecurso(expression15.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ServiciosEmpleados"), typeof(ServiciosEmpleados)));

       return rules;
       }

       #endregion
    }
 }
