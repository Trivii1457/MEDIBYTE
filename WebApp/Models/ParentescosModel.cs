using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ParentescosModel
   {
      public Parentescos Entity { get; set; }

      public ParentescosModel()
      {
         Entity = new Parentescos();
      }

   }

}
