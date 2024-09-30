using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ImagenesDiagnosticasDetalleModel
   {
      public ImagenesDiagnosticasDetalle Entity { get; set; }

      public ImagenesDiagnosticasDetalleModel()
      {
         Entity = new ImagenesDiagnosticasDetalle();
      }

   }

}
