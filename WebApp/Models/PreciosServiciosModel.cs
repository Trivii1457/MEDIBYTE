using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class PreciosServiciosModel
   {
      public PreciosServicios Entity { get; set; }

        public string SelloPath { get; set; }

        public PreciosServiciosModel()
      {
         Entity = new PreciosServicios();
      }

   }

}
