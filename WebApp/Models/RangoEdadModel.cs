using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class RangoEdadModel
   {
      public RangoEdad Entity { get; set; }

      public RangoEdadModel()
      {
         Entity = new RangoEdad();
      }

   }

}
