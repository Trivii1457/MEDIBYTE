using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class DocumentosModel
   {
      public Documentos Entity { get; set; }

      public DocumentosModel()
      {
         Entity = new Documentos();
      }

   }

}
