using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class TiposDocumentosModel
   {
      public TiposDocumentos Entity { get; set; }

      public TiposDocumentosModel()
      {
         Entity = new TiposDocumentos();
      }

   }

}
