using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.IO;

namespace Blazor.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("es");
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;

            var hostsettings = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true)
               .Build();
            CreateWebHostBuilder(args, hostsettings).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfigurationRoot hostsettings) =>
          WebHost.CreateDefaultBuilder(args)
            .UseConfiguration(hostsettings)
            .UseStartup<Startup>();
    }
}
