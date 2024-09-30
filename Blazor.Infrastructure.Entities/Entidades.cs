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
    /// Entidades object for mapped table Entidades.
    /// </summary>
    [Table("Entidades")]
    public partial class Entidades : BaseEntity
    {

       #region Columnas normales)

       [Column("Alias")]
       [DDisplayName("Entidades.Alias")]
       [DStringLength("Entidades.Alias",250)]
       public virtual String Alias { get; set; }

       [Column("Nombre")]
       [DDisplayName("Entidades.Nombre")]
       [DRequired("Entidades.Nombre")]
       [DStringLength("Entidades.Nombre",250)]
       public virtual String Nombre { get; set; }

       [Column("NumeroIdentificacion")]
       [DDisplayName("Entidades.NumeroIdentificacion")]
       [DRequired("Entidades.NumeroIdentificacion")]
       [DStringLength("Entidades.NumeroIdentificacion",45)]
       public virtual String NumeroIdentificacion { get; set; }

       [Column("CodigoReps")]
       [DDisplayName("Entidades.CodigoReps")]
       [DStringLength("Entidades.CodigoReps",45)]
       public virtual String CodigoReps { get; set; }

       [Column("Sesion")]
       [DDisplayName("Entidades.Sesion")]
       [DStringLength("Entidades.Sesion",100)]
       public virtual String Sesion { get; set; }

       [Column("CorreoElectronico")]
       [DDisplayName("Entidades.CorreoElectronico")]
       [DStringLength("Entidades.CorreoElectronico",200)]
       public virtual String CorreoElectronico { get; set; }

       [Column("Fax")]
       [DDisplayName("Entidades.Fax")]
       [DStringLength("Entidades.Fax",100)]
       public virtual String Fax { get; set; }

       [Column("PaginaWeb")]
       [DDisplayName("Entidades.PaginaWeb")]
       [DStringLength("Entidades.PaginaWeb",255)]
       public virtual String PaginaWeb { get; set; }

       [Column("Direccion")]
       [DDisplayName("Entidades.Direccion")]
       [DStringLength("Entidades.Direccion",255)]
       public virtual String Direccion { get; set; }

       [Column("Telefono")]
       [DDisplayName("Entidades.Telefono")]
       [DStringLength("Entidades.Telefono",100)]
       public virtual String Telefono { get; set; }

       [Column("DV")]
       [DDisplayName("Entidades.DV")]
       [DStringLength("Entidades.DV",2)]
       public virtual String DV { get; set; }

       [Column("EsFijo")]
       [DDisplayName("Entidades.EsFijo")]
       public virtual Boolean EsFijo { get; set; }

       [Column("FechaCorte", TypeName = "datetime")]
       [DDisplayName("Entidades.FechaCorte")]
       public virtual DateTime? FechaCorte { get; set; }

       [Column("PorcentajeRetefuente")]
       [DDisplayName("Entidades.PorcentajeRetefuente")]
       [DRequired("Entidades.PorcentajeRetefuente")]
       public virtual Decimal PorcentajeRetefuente { get; set; }

       [Column("PorcentajeReteIca")]
       [DDisplayName("Entidades.PorcentajeReteIca")]
       [DRequired("Entidades.PorcentajeReteIca")]
       public virtual Decimal PorcentajeReteIca { get; set; }

       [Column("NroMatriculaMercantil")]
       [DDisplayName("Entidades.NroMatriculaMercantil")]
       [DRequired("Entidades.NroMatriculaMercantil")]
       [DStringLength("Entidades.NroMatriculaMercantil",20)]
       public virtual String NroMatriculaMercantil { get; set; }

       [Column("CodigoPostal")]
       [DDisplayName("Entidades.CodigoPostal")]
       [DRequired("Entidades.CodigoPostal")]
       [DStringLength("Entidades.CodigoPostal",20)]
       public virtual String CodigoPostal { get; set; }

       [Column("EsResponsabledeIva")]
       [DDisplayName("Entidades.EsResponsabledeIva")]
       [DRequired("Entidades.EsResponsabledeIva")]
       [DStringLength("Entidades.EsResponsabledeIva",2)]
       public virtual String EsResponsabledeIva { get; set; }

       [Column("ConsecutivoRips")]
       [DDisplayName("Entidades.ConsecutivoRips")]
       public virtual Int32? ConsecutivoRips { get; set; }

       #endregion

       #region Columnas referenciales)

       [Column("TiposIdentificacionId")]
       [DDisplayName("Entidades.TiposIdentificacionId")]
       [DRequired("Entidades.TiposIdentificacionId")]
       [DRequiredFK("Entidades.TiposIdentificacionId")]
       public virtual Int64 TiposIdentificacionId { get; set; }

       [Column("EstadosId")]
       [DDisplayName("Entidades.EstadosId")]
       [DRequired("Entidades.EstadosId")]
       [DRequiredFK("Entidades.EstadosId")]
       public virtual Int64 EstadosId { get; set; }

       [Column("CiudadesId")]
       [DDisplayName("Entidades.CiudadesId")]
       [DRequired("Entidades.CiudadesId")]
       [DRequiredFK("Entidades.CiudadesId")]
       public virtual Int64 CiudadesId { get; set; }

       [Column("TiposPersonasId")]
       [DDisplayName("Entidades.TiposPersonasId")]
       [DRequired("Entidades.TiposPersonasId")]
       [DRequiredFK("Entidades.TiposPersonasId")]
       public virtual Int64 TiposPersonasId { get; set; }

       [Column("TipoEntidadesId")]
       [DDisplayName("Entidades.TipoEntidadesId")]
       [DRequired("Entidades.TipoEntidadesId")]
       [DRequiredFK("Entidades.TipoEntidadesId")]
       public virtual Int64 TipoEntidadesId { get; set; }

       #endregion

       #region Propiedades referencias de entrada)

       [ForeignKey("CiudadesId")]
       public virtual Ciudades Ciudades { get; set; }

       [ForeignKey("EstadosId")]
       public virtual Estados Estados { get; set; }

       [ForeignKey("TipoEntidadesId")]
       public virtual TipoEntidades TipoEntidades { get; set; }

       [ForeignKey("TiposIdentificacionId")]
       public virtual TiposIdentificacion TiposIdentificacion { get; set; }

       [ForeignKey("TiposPersonasId")]
       public virtual TiposPersonas TiposPersonas { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<Entidades, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Entidades, bool>> expression = null;

        expression = entity => entity.CodigoReps == this.CodigoReps && entity.Alias == this.Alias;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Entidades.CodigoReps" , "Entidades.Alias" )));

        expression = entity => entity.TiposIdentificacionId == this.TiposIdentificacionId && entity.NumeroIdentificacion == this.NumeroIdentificacion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Entidades.TiposIdentificacionId" , "Entidades.NumeroIdentificacion" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Entidades, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.CodigoReps == this.CodigoReps && entity.Alias == this.Alias)
                               && entity.CodigoReps == this.CodigoReps && entity.Alias == this.Alias;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Entidades.CodigoReps" , "Entidades.Alias" )));

        expression = entity => !(entity.Id == this.Id && entity.TiposIdentificacionId == this.TiposIdentificacionId && entity.NumeroIdentificacion == this.NumeroIdentificacion)
                               && entity.TiposIdentificacionId == this.TiposIdentificacionId && entity.NumeroIdentificacion == this.NumeroIdentificacion;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Entidades.TiposIdentificacionId" , "Entidades.NumeroIdentificacion" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Convenios, bool>> expression0 = entity => entity.EntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Convenios"), typeof(Convenios)));

        Expression<Func<EntidadesEsquemasImpuestos, bool>> expression1 = entity => entity.EntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EntidadesEsquemasImpuestos"), typeof(EntidadesEsquemasImpuestos)));

        Expression<Func<EntidadesResponsabilidadesFiscales, bool>> expression2 = entity => entity.EntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","EntidadesResponsabilidadesFiscales"), typeof(EntidadesResponsabilidadesFiscales)));

        Expression<Func<Facturas, bool>> expression3 = entity => entity.EntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Facturas"), typeof(Facturas)));

        Expression<Func<FacturasGeneracion, bool>> expression4 = entity => entity.EntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","FacturasGeneracion"), typeof(FacturasGeneracion)));

        Expression<Func<Filiales, bool>> expression5 = entity => entity.EntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression5.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Filiales"), typeof(Filiales)));

        Expression<Func<Notas, bool>> expression6 = entity => entity.EntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression6.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Notas"), typeof(Notas)));

        Expression<Func<PacientesEntidades, bool>> expression7 = entity => entity.EntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression7.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","PacientesEntidades"), typeof(PacientesEntidades)));

        Expression<Func<ProgramacionCitas, bool>> expression8 = entity => entity.EntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression8.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProgramacionCitas"), typeof(ProgramacionCitas)));

        Expression<Func<RadicacionCuentas, bool>> expression9 = entity => entity.EntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression9.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","RadicacionCuentas"), typeof(RadicacionCuentas)));

        Expression<Func<Recaudos, bool>> expression10 = entity => entity.EntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression10.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Recaudos"), typeof(Recaudos)));

        Expression<Func<Rips, bool>> expression11 = entity => entity.EntidadesId == this.Id;
        rules.Add(new ExpRecurso(expression11.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Rips"), typeof(Rips)));

       return rules;
       }

       #endregion
    }
 }
