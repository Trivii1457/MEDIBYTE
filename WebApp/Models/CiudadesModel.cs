using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class CiudadesModel
   {
      public Ciudades Entity { get; set; }

      public CiudadesModel()
      {
         Entity = new Ciudades();
      }

   }

}
