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
    /// Empresas object for mapped table Empresas.
    /// </summary>
    [Table("Empresas")]
    public partial class Empresas : BaseEntity
    {

       #region Columnas normales)

       [Column("RazonSocial")]
       [DDisplayName("Empresas.RazonSocial")]
       [DRequired("Empresas.RazonSocial")]
       [DStringLength("Empresas.RazonSocial",150)]
       public virtual String RazonSocial { get; set; }

       [Column("NumeroIdentificacion")]
       [DDisplayName("Empresas.NumeroIdentificacion")]
       [DRequired("Empresas.NumeroIdentificacion")]
       [DStringLength("Empresas.NumeroIdentificacion",45)]
       public virtual String NumeroIdentificacion { get; set; }

       [Column("DV")]
       [DDisplayName("Empresas.DV")]
       [DRequired("Empresas.DV")]
       [DStringLength("Empresas.DV",2)]
       public virtual String DV { get; set; }

       [Column("Direccion")]
       [DDisplayName("Empresas.Direccion")]
       [DStringLength("Empresas.Direccion",200)]
       public virtual String Direccion { get; set; }

       [Column("Telefono")]
       [DDisplayName("Empresas.Telefono")]
       [DStringLength("Empresas.Telefono",10)]
       public virtual String Telefono { get; set; }

       [Column("Celular")]
       [DDisplayName("Empresas.Celular")]
       [DStringLength("Empresas.Celular",10)]
       public virtual String Celular { get; set; }

       [Column("CorreoElectronico")]
       [DDisplayName("Empresas.CorreoElectronico")]
       [DStringLength("Empresas.CorreoElectronico",200)]
       public virtual String CorreoElectronico { get; set; }

       [Column("PaginaWeb")]
       [DDisplayName("Empresas.PaginaWeb")]
       [DStringLength("Empresas.PaginaWeb",100)]
       public virtual String PaginaWeb { get; set; }

       [Column("NumeroIdentificacionRepresentanteLegal")]
       [DDisplayName("Empresas.NumeroIdentificacionRepresentanteLegal")]
       [DStringLength("Empresas.NumeroIdentificacionRepresentanteLegal",45)]
       public virtual String NumeroIdentificacionRepresentanteLegal { get; set; }

       [Column("NombresApellidosRepresentanteLegal")]
       [DDisplayName("Empresas.NombresApellidosRepresentanteLegal")]
       [DStringLength("Empresas.NombresApellidosRepresentanteLegal",100)]
       public virtual String NombresApellidosRepresentanteLegal { get; set; }

       [Column("CIIU")]
       [DDisplayName("Empresas.CIIU")]
       [DRequired("Empresas.CIIU")]
       [DStringLength("Empresas.CIIU",20)]
       public virtual String CIIU { get; set; }

       [Column("CodigoPostal")]
       [DDisplayName("Empresas.CodigoPostal")]
       [DRequired("Empresas.CodigoPostal")]
       [DStringLength("Empresas.CodigoPostal",20)]
       public virtual String CodigoPostal { get; set; }

       [Column("Alias")]
       [DDisplayName("Empresas.Alias")]
       [DRequired("Empresas.Alias")]
       [DStringLength("Empresas.Alias",50)]
       public virtual String Alias { get; set; }

       [Column("CodigoReps")]
       [DDisplayName("Empresas.CodigoReps")]
       [DStringLength("Empresas.CodigoReps",45)]
       public virtual String CodigoReps { get; set; }

       [Column("Codigo85")]
       [DDisplayName("Empresas.Codigo85")]
       [DStringLength("Empresas.Codigo85",2)]
       public virtual String Codigo85 { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("TiposIdentificacionId")]
       [DDisplayName("Empresas.TiposIdentificacionId")]
       [DRequired("Empresas.TiposIdentificacionId")]
       [DRequiredFK("Empresas.TiposIdentificacionId")]
       public virtual Int64 TiposIdentificacionId { get; set; }

       [Column("CiudadesId")]
       [DDisplayName("Empresas.CiudadesId")]
       [DRequired("Empresas.CiudadesId")]
       [DRequiredFK("Empresas.CiudadesId")]
       public virtual Int64 CiudadesId { get; set; }

       [Column("TiposIdentificacionRepresentanteLegalId")]
       [DDisplayName("Empresas.TiposIdentificacionRepresentanteLegalId")]
       public virtual Int64? TiposIdentificacionRepresentanteLegalId { get; set; }

       [Column("TiposPersonasId")]
       [DDisplayName("Empresas.TiposPersonasId")]
       [DRequired("Empresas.TiposPersonasId")]
       [DRequiredFK("Empresas.TiposPersonasId")]
       public virtual Int64 TiposPersonasId { get; set; }

       [Column("LogoArchivosId")]
       [DDisplayName("Empresas.LogoArchivosId")]
       public virtual Int64? LogoArchivosId { get; set; }

       [Column("TiposRegimenContableId")]
       [DDisplayName("Empresas.TiposRegimenContableId")]
       [DRequired("Empresas.TiposRegimenContableId")]
       [DRequiredFK("Empresas.TiposRegimenContableId")]
       public virtual Int64 TiposRegimenContableId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("CiudadesId")]
       public virtual Ciudades Ciudades { get; set; }

       [ForeignKey("LogoArchivosId")]
       public virtual Archivos LogoArchivos { get; set; }

       [ForeignKey("TiposIdentificacionId")]
       public virtual TiposIdentificacion TiposIdentificacion { get; set; }

       [ForeignKey("TiposIdentificacionRepresentanteLegalId")]
       public virtual TiposIdentificacion TiposIdentificacionRepresentanteLegal { get; set; }

       [ForeignKey("TiposPersonasId")]
       public virtual TiposPersonas TiposPersonas { get; set; }

       [ForeignKey("TiposRegimenContableId")]
       public virtual TiposRegimenContable TiposRegimenContable { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Empresas, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Empresas, bool>> expression = null;

        expression = entity => entity.Alias == this.Alias;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Empresas.Alias" )));

        expression = entity => entity.TiposIdentificacionId == this.TiposIdentificacionId && entity.NumeroIdentificacion == this.NumeroIdentificacion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Empresas.TiposIdentificacionId" , "Empresas.NumeroIdentificacion" )));

        expression = entity => entity.RazonSocial == this.RazonSocial;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Empresas.RazonSocial" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Empresas, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Alias == this.Alias)
                               && entity.Alias == this.Alias;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Empresas.Alias" )));

        expression = entity => !(entity.Id == this.Id && entity.TiposIdentificacionId == this.TiposIdentificacionId && entity.NumeroIdentificacion == this.NumeroIdentificacion)
                               && entity.TiposIdentificacionId == this.TiposIdentificacionId && entity.NumeroIdentificacion == this.NumeroIdentificacion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Empresas.TiposIdentificacionId" , "Empresas.NumeroIdentificacion" )));

        expression = entity => !(entity.Id == this.Id && entity.RazonSocial == this.RazonSocial)
                               && entity.RazonSocial == this.RazonSocial;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Empresas.RazonSocial" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<AdministracionHonorarios, bool>> expression0 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","AdministracionHonorarios"), typeof(AdministracionHonorarios)));

        Expression<Func<Admisiones, bool>> expression1 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<Consultorios, bool>> expression2 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Consultorios"), typeof(Consultorios)));

        Expression<Func<Convenios, bool>> expression3 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Convenios"), typeof(Convenios)));

        Expression<Func<Empleados, bool>> expression4 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Empleados"), typeof(Empleados)));

        Expression<Func<EmpresasEsquemasImpuestos, bool>> expression5 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression5.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EmpresasEsquemasImpuestos"), typeof(EmpresasEsquemasImpuestos)));

        Expression<Func<EmpresasResponsabilidadesFiscales, bool>> expression6 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression6.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EmpresasResponsabilidadesFiscales"), typeof(EmpresasResponsabilidadesFiscales)));

        Expression<Func<Facturas, bool>> expression7 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression7.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Facturas"), typeof(Facturas)));

        Expression<Func<FacturasGeneracion, bool>> expression8 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression8.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","FacturasGeneracion"), typeof(FacturasGeneracion)));

        Expression<Func<LiquidacionHonorarios, bool>> expression9 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression9.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","LiquidacionHonorarios"), typeof(LiquidacionHonorarios)));

        Expression<Func<ListaPrecios, bool>> expression10 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression10.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ListaPrecios"), typeof(ListaPrecios)));

        Expression<Func<Notas, bool>> expression11 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression11.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Notas"), typeof(Notas)));

        Expression<Func<Pacientes, bool>> expression12 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression12.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Pacientes"), typeof(Pacientes)));

        Expression<Func<ProgramacionCitas, bool>> expression13 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression13.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionCitas"), typeof(ProgramacionCitas)));

        Expression<Func<RadicacionCuentas, bool>> expression14 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression14.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","RadicacionCuentas"), typeof(RadicacionCuentas)));

        Expression<Func<Recaudos, bool>> expression15 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression15.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Recaudos"), typeof(Recaudos)));

        Expression<Func<Rips, bool>> expression16 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression16.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Rips"), typeof(Rips)));

        Expression<Func<Sedes, bool>> expression17 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression17.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Sedes"), typeof(Sedes)));

        Expression<Func<Servicios, bool>> expression18 = entity => entity.EmpresasId == this.Id;
        rules.Add(new ExpRecurso(expression18.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Servicios"), typeof(Servicios)));

       return rules;
       }

       #endregion
    }
 }
