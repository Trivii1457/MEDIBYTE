using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class FilialesModel
   {
      public Filiales Entity { get; set; }

        public long PaisId { get; set; }
        public long DepartamentoId { get; set; }

        public FilialesModel()
      {
         Entity = new Filiales();
      }

   }

}
