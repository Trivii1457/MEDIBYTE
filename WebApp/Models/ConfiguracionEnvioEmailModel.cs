using Blazor.Infrastructure.Entities;

namespace Blazor.WebApp.Models
{

   public partial class ConfiguracionEnvioEmailModel
   {
      public ConfiguracionEnvioEmail Entity { get; set; }

      public ConfiguracionEnvioEmailModel()
      {
         Entity = new ConfiguracionEnvioEmail();
      }

   }

}
