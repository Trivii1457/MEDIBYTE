using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class BancosModel
   {
      public Bancos Entity { get; set; }

      public BancosModel()
      {
         Entity = new Bancos();
      }

   }

}
