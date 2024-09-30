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
    /// Sedes object for mapped table Sedes.
    /// </summary>
    [Table("Sedes")]
    public partial class Sedes : BaseEntity
    {

       #region Columnas normales)

       [Column("Nombre")]
       [DDisplayName("Sedes.Nombre")]
       [DRequired("Sedes.Nombre")]
       [DStringLength("Sedes.Nombre",250)]
       public virtual String Nombre { get; set; }

       [Column("CorreoElectronico")]
       [DDisplayName("Sedes.CorreoElectronico")]
       [DStringLength("Sedes.CorreoElectronico",200)]
       public virtual String CorreoElectronico { get; set; }

       [Column("Fax")]
       [DDisplayName("Sedes.Fax")]
       [DStringLength("Sedes.Fax",100)]
       public virtual String Fax { get; set; }

       [Column("PaginaWeb")]
       [DDisplayName("Sedes.PaginaWeb")]
       [DStringLength("Sedes.PaginaWeb",255)]
       public virtual String PaginaWeb { get; set; }

       [Column("Direccion")]
       [DDisplayName("Sedes.Direccion")]
       [DStringLength("Sedes.Direccion",255)]
       public virtual String Direccion { get; set; }

       [Column("NroMatriculaMercantil")]
       [DDisplayName("Sedes.NroMatriculaMercantil")]
       [DRequired("Sedes.NroMatriculaMercantil")]
       [DStringLength("Sedes.NroMatriculaMercantil",20)]
       public virtual String NroMatriculaMercantil { get; set; }

       [Column("CodigoPostal")]
       [DDisplayName("Sedes.CodigoPostal")]
       [DRequired("Sedes.CodigoPostal")]
       [DStringLength("Sedes.CodigoPostal",20)]
       public virtual String CodigoPostal { get; set; }

       [Column("Codigo")]
       [DDisplayName("Sedes.Codigo")]
       [DRequired("Sedes.Codigo")]
       [DStringLength("Sedes.Codigo",20)]
       public virtual String Codigo { get; set; }

       [Column("CO")]
       [DDisplayName("Sedes.CO")]
       [DStringLength("Sedes.CO",3)]
       public virtual String CO { get; set; }

       [Column("Codigo85")]
       [DDisplayName("Sedes.Codigo85")]
       [DStringLength("Sedes.Codigo85",3)]
       public virtual String Codigo85 { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("EstadosId")]
       [DDisplayName("Sedes.EstadosId")]
       [DRequired("Sedes.EstadosId")]
       [DRequiredFK("Sedes.EstadosId")]
       public virtual Int64 EstadosId { get; set; }

       [Column("CiudadesId")]
       [DDisplayName("Sedes.CiudadesId")]
       [DRequired("Sedes.CiudadesId")]
       [DRequiredFK("Sedes.CiudadesId")]
       public virtual Int64 CiudadesId { get; set; }

       [Column("EmpresasId")]
       [DDisplayName("Sedes.EmpresasId")]
       [DRequired("Sedes.EmpresasId")]
       [DRequiredFK("Sedes.EmpresasId")]
       public virtual Int64 EmpresasId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("CiudadesId")]
       public virtual Ciudades Ciudades { get; set; }

       [ForeignKey("EmpresasId")]
       public virtual Empresas Empresas { get; set; }

       [ForeignKey("EstadosId")]
       public virtual Estados Estados { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Sedes, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Sedes, bool>> expression = null;

        expression = entity => entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Sedes.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Sedes, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.Nombre == this.Nombre)
                               && entity.Nombre == this.Nombre;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Sedes.Nombre" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Cajas, bool>> expression0 = entity => entity.SedesId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Cajas"), typeof(Cajas)));

        Expression<Func<Consultorios, bool>> expression1 = entity => entity.SedesId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Consultorios"), typeof(Consultorios)));

        Expression<Func<Facturas, bool>> expression2 = entity => entity.SedesId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Facturas"), typeof(Facturas)));

        Expression<Func<FacturasGeneracion, bool>> expression3 = entity => entity.SedesId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","FacturasGeneracion"), typeof(FacturasGeneracion)));

        Expression<Func<HorariosAtencion, bool>> expression4 = entity => entity.SedeId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","HorariosAtencion"), typeof(HorariosAtencion)));

        Expression<Func<Notas, bool>> expression5 = entity => entity.SedesId == this.Id;
        rules.Add(new ExpRecurso(expression5.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Notas"), typeof(Notas)));

        Expression<Func<ProgramacionCitas, bool>> expression6 = entity => entity.SedesId == this.Id;
        rules.Add(new ExpRecurso(expression6.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionCitas"), typeof(ProgramacionCitas)));

        Expression<Func<RadicacionCuentas, bool>> expression7 = entity => entity.SedesId == this.Id;
        rules.Add(new ExpRecurso(expression7.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","RadicacionCuentas"), typeof(RadicacionCuentas)));

        Expression<Func<Recaudos, bool>> expression8 = entity => entity.SedesId == this.Id;
        rules.Add(new ExpRecurso(expression8.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Recaudos"), typeof(Recaudos)));

        Expression<Func<SedesDocumentos, bool>> expression9 = entity => entity.SedesId == this.Id;
        rules.Add(new ExpRecurso(expression9.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","SedesDocumentos"), typeof(SedesDocumentos)));

        Expression<Func<ServiciosSedes, bool>> expression10 = entity => entity.SedesId == this.Id;
        rules.Add(new ExpRecurso(expression10.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ServiciosSedes"), typeof(ServiciosSedes)));

       return rules;
       }

       #endregion
    }
 }
