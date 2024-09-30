using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class IncapacidadesOrigenesModel
   {
      public IncapacidadesOrigenes Entity { get; set; }

      public IncapacidadesOrigenesModel()
      {
         Entity = new IncapacidadesOrigenes();
      }

   }

}
