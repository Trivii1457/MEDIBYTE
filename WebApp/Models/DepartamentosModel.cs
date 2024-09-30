using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class DepartamentosModel
   {
      public Departamentos Entity { get; set; }

      public DepartamentosModel()
      {
         Entity = new Departamentos();
      }

   }

}
