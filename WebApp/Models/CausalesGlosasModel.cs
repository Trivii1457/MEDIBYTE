using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class CausalesGlosasModel
   {
      public CausalesGlosas Entity { get; set; }

      public CausalesGlosasModel()
      {
         Entity = new CausalesGlosas();
      }

   }

}
