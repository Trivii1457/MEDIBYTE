using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ImagenesDiagnosticasModel
   {
      public ImagenesDiagnosticas Entity { get; set; }

      public ImagenesDiagnosticasModel()
      {
         Entity = new ImagenesDiagnosticas();
      }

   }

}
