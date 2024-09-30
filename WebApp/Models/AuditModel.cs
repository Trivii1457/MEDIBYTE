using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class AuditModel
   {
      public Audit Entity { get; set; }

      public AuditModel()
      {
         Entity = new Audit();
      }

   }

}
