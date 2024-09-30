using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class HistoriasClinicasRespuestasModel
   {
      public HistoriasClinicasRespuestas Entity { get; set; }

      public HistoriasClinicasRespuestasModel()
      {
         Entity = new HistoriasClinicasRespuestas();
      }

   }

}
