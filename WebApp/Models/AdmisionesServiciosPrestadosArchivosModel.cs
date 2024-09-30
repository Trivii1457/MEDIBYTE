using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class AdmisionesServiciosPrestadosArchivosModel
   {
      public AdmisionesServiciosPrestadosArchivos Entity { get; set; }

      public AdmisionesServiciosPrestadosArchivosModel()
      {
         Entity = new AdmisionesServiciosPrestadosArchivos();
      }

   }

}
