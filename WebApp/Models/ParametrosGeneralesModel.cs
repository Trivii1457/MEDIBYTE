using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ParametrosGeneralesModel
   {
      public ParametrosGenerales Entity { get; set; }

      public ParametrosGeneralesModel()
      {
         Entity = new ParametrosGenerales();
      }

   }

}
