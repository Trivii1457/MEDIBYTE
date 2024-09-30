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
    /// User object for mapped table Users.
    /// </summary>
    [Table("Users")]
    public partial class User : BaseEntity
    {

       #region Columnas normales)

       [Column("UserName")]
       [DDisplayName("Users.UserName")]
       [DRequired("Users.UserName")]
       [DStringLength("Users.UserName",50)]
       public virtual String UserName { get; set; }

       [Column("Password")]
       [DDisplayName("Users.Password")]
       [DRequired("Users.Password")]
       [DStringLength("Users.Password",255)]
       public virtual String Password { get; set; }

       [Column("Email")]
       [DDisplayName("Users.Email")]
       [DRequired("Users.Email")]
       [DStringLength("Users.Email",255)]
       public virtual String Email { get; set; }

       [Column("Names")]
       [DDisplayName("Users.Names")]
       [DStringLength("Users.Names",60)]
       public virtual String Names { get; set; }

       [Column("LastNames")]
       [DDisplayName("Users.LastNames")]
       [DStringLength("Users.LastNames",60)]
       public virtual String LastNames { get; set; }

       [Column("IdentificationNumber")]
       [DDisplayName("Users.IdentificationNumber")]
       [DStringLength("Users.IdentificationNumber",20)]
       public virtual String IdentificationNumber { get; set; }

       [Column("IdentificationTypeId")]
       [DDisplayName("Users.IdentificationTypeId")]
       [DStringLength("Users.IdentificationTypeId",3)]
       public virtual String IdentificationTypeId { get; set; }

       [Column("GenderId")]
       [DDisplayName("Users.GenderId")]
       [DStringLength("Users.GenderId",1)]
       public virtual String GenderId { get; set; }

       [Column("IsActive")]
       [DDisplayName("Users.IsActive")]
       [DRequired("Users.IsActive")]
       public virtual Boolean IsActive { get; set; }

       #endregion

       #region Reglas expression

       public override Expression<Func<T, bool>> PrimaryKeyExpression<T>()
       {
       Expression<Func<User, bool>> expression = entity => entity.Id == this.Id;
       return expression as Expression<Func<T, bool>>;
       }

       public override List<ExpRecurso> GetAdicionarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<User, bool>> expression = null;

        expression = entity => entity.UserName == this.UserName;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Users.UserName" )));

       return rules;
       }

       public override List<ExpRecurso> GetModificarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<User, bool>> expression = null;

        expression = entity => !(entity.Id == this.Id && entity.UserName == this.UserName)
                               && entity.UserName == this.UserName;
        rules.Add(new ExpRecurso(expression.ToExpressionNode() , new Recurso("BLL.BUSINESS.UNIQUE", "Users.UserName" )));

       return rules;
       }

       public override List<ExpRecurso> GetEliminarExpression<T>()
       {
        var rules = new List<ExpRecurso>();
        Expression<Func<Admisiones, bool>> expression0 = entity => entity.UserAproboId == this.Id;
        rules.Add(new ExpRecurso(expression0.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Admisiones"), typeof(Admisiones)));

        Expression<Func<CiclosCajas, bool>> expression1 = entity => entity.CloseUsersId == this.Id;
        rules.Add(new ExpRecurso(expression1.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","CiclosCajas"), typeof(CiclosCajas)));

        Expression<Func<CiclosCajas, bool>> expression2 = entity => entity.OpenUsersId == this.Id;
        rules.Add(new ExpRecurso(expression2.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","CiclosCajas"), typeof(CiclosCajas)));

        Expression<Func<Empleados, bool>> expression3 = entity => entity.UserId == this.Id;
        rules.Add(new ExpRecurso(expression3.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Empleados"), typeof(Empleados)));

        Expression<Func<ProfileUser, bool>> expression4 = entity => entity.UserId == this.Id;
        rules.Add(new ExpRecurso(expression4.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","ProfileUsers"), typeof(ProfileUser)));

        Expression<Func<Sessions, bool>> expression5 = entity => entity.UserId == this.Id;
        rules.Add(new ExpRecurso(expression5.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","Sessions"), typeof(Sessions)));

        Expression<Func<UserOffice, bool>> expression6 = entity => entity.UserId == this.Id;
        rules.Add(new ExpRecurso(expression6.ToExpressionNode() , new Recurso("BLL.BUSINESS.DELETE_REL","UserOffices"), typeof(UserOffice)));

       return rules;
       }

       #endregion
    }
 }
