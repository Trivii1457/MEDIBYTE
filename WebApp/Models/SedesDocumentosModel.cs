using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class SedesDocumentosModel
   {
      public SedesDocumentos Entity { get; set; }

      public SedesDocumentosModel()
      {
         Entity = new SedesDocumentos();
      }

   }

}
