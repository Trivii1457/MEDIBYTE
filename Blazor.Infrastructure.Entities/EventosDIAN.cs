using Dominus.Backend.Data;
using Dominus.Backend.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Blazor.Infrastructure.Entities
{
    /// <summary>
    /// EventosDIAN object for mapped table EventosDIAN.
    /// </summary>
    [Table("EventosDIAN")]
    public partial class EventosDIAN : BaseEntity
    {

       #region Columnas normales)

       [Column("TipoEvento")]
       [DDisplayName("EventosDIAN.TipoEvento")]
       [DRequired("EventosDIAN.TipoEvento")]
       [DStringLength("EventosDIAN.TipoEvento",10)]
       public virtual String TipoEvento { get; set; }

       [Column("CodigoEvento")]
       [DDisplayName("EventosDIAN.CodigoEvento")]
       [DRequired("EventosDIAN.CodigoEvento")]
       [DStringLength("EventosDIAN.CodigoEvento",10)]
       public virtual String CodigoEvento { get; set; }

       [Column("Descripcion")]
       [DDisplayName("EventosDIAN.Descripcion")]
       [DRequired("EventosDIAN.Descripcion")]
       [DStringLength("EventosDIAN.Descripcion",128)]
       public virtual String Descripcion { get; set; }

       [Column("Uri")]
       [DDisplayName("EventosDIAN.Uri")]
       [DRequired("EventosDIAN.Uri")]
       [DStringLength("EventosDIAN.Uri",1024)]
       public virtual String Uri { get; set; }

       [Column("NumDocEmisor")]
       [DDisplayName("EventosDIAN.NumDocEmisor")]
       [DRequired("EventosDIAN.NumDocEmisor")]
       [DStringLength("EventosDIAN.NumDocEmisor",20)]
       public virtual String NumDocEmisor { get; set; }

       [Column("NumDocReceptor")]
       [DDisplayName("EventosDIAN.NumDocReceptor")]
       [DRequired("EventosDIAN.NumDocReceptor")]
       [DStringLength("EventosDIAN.NumDocReceptor",20)]
       public virtual String NumDocReceptor { get; set; }

       [Column("TipoDoc")]
       [DDisplayName("EventosDIAN.TipoDoc")]
       [DRequired("EventosDIAN.TipoDoc")]
       [DStringLength("EventosDIAN.TipoDoc",10)]
       public virtual String TipoDoc { get; set; }

       [Column("SubtipoDoc")]
       [DDisplayName("EventosDIAN.SubtipoDoc")]
       [DRequired("EventosDIAN.SubtipoDoc")]
       [DStringLength("EventosDIAN.SubtipoDoc",10)]
       public virtual String SubtipoDoc { get; set; }

       [Column("NroId")]
       [DDisplayName("EventosDIAN.NroId")]
       [DRequired("EventosDIAN.NroId")]
       [DStringLength("EventosDIAN.NroId",50)]
       public virtual String NroId { get; set; }

       [Column("Uuid")]
       [DDisplayName("EventosDIAN.Uuid")]
       [DRequired("EventosDIAN.Uuid")]
       [DStringLength("EventosDIAN.Uuid",255)]
       public virtual String Uuid { get; set; }

       [Column("FechaEmision", TypeName = "datetime")]
       [DDisplayName("EventosDIAN.FechaEmision")]
       [DRequired("EventosDIAN.FechaEmision")]
       public virtual DateTime FechaEmision { get; set; }

       [Column("FechaEvento", TypeName = "datetime")]
       [DDisplayName("EventosDIAN.FechaEvento")]
       [DRequired("EventosDIAN.FechaEvento")]
       public virtual DateTime FechaEvento { get; set; }

       [Column("Observacion")]
       [DDisplayName("EventosDIAN.Observacion")]
       [DStringLength("EventosDIAN.Observacion",1024)]
       public virtual String Observacion { get; set; }

       [Column("XmlDoc")]
       [DDisplayName("EventosDIAN.XmlDoc")]
       [DRequired("EventosDIAN.XmlDoc")]
       [DStringLength("EventosDIAN.XmlDoc",1024)]
       public virtual String XmlDoc { get; set; }

       [Column("Pdf")]
       [DDisplayName("EventosDIAN.Pdf")]
       [DRequired("EventosDIAN.Pdf")]
       [DStringLength("EventosDIAN.Pdf",1024)]
       public virtual String Pdf { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<EventosDIAN, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EventosDIAN, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<EventosDIAN, bool>> expression = null;

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
       return rules;
       }

       #endregion
    }
 }
