using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebReports;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using Dominus.Backend.Application;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Blazor.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IFileProvider FileProvider { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDevExpressControls();

            services.ConfigureReportingServices(configurator =>
            {
                configurator.ConfigureReportDesigner(designerConfigurator =>
                {
                    designerConfigurator.RegisterDataSourceWizardConfigFileConnectionStringsProvider();
                });
                configurator.ConfigureWebDocumentViewer(viewerConfigurator =>
                {
                    viewerConfigurator.UseCachedReportSourceBuilder();
                });
            });

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                DevExpress.Printing.CrossPlatform.CustomEngineHelper.RegisterCustomDrawingEngine(
                    typeof(
                        DevExpress.CrossPlatform.Printing.DrawingEngine.PangoCrossPlatformEngine
                    ));
            }

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue; // if don't set default value is: 128 MB
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });

            //services.Configure<KestrelServerOptions>(options =>
            //{
            //    options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 M
            //});

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Login";
                    options.LogoutPath = "/Login/LogOff";
                    options.Cookie.Name = "auth_net_webapp";
                    options.SlidingExpiration = true;

                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<AppState>();

            services.AddMvc(options => options.MaxModelValidationErrors = 200)
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    //options.SerializerSettings.TypeNameHandling = TypeNameHandling.Objects; //Esto hace que se agregue la propriedad $type Si esta configurado como ".Object" a las respuestas JSON
                    //options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None; // Esto hace que se agregue la propiedad $id a las respuestas JSON
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver(); //Esto hace que las propiedades de las respuestas JSON se conviertan en CamelCase 
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss.fff";
                })
                .AddJsonOptions(configure =>
                {
                    configure.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    configure.JsonSerializerOptions.PropertyNamingPolicy = null;
                    configure.JsonSerializerOptions.AllowTrailingCommas = true;
                }).AddXmlSerializerFormatters();

       

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize, // Esto permite evitar los loops para la serializacion de las foraneas
                PreserveReferencesHandling = PreserveReferencesHandling.None
            };

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowFromAll",
            //        builder => builder
            //        .AllowAnyMethod()
            //        .AllowAnyOrigin()
            //        .AllowAnyHeader());
            //});

            services.ConfigureReportingServices(configurator => {
                configurator.DisableCheckForCustomControllers();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            var reportDirectory = Path.Combine(env.ContentRootPath, "Reports");
            DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(new ReportStorageWebExtension1(reportDirectory));
            var reportingLogger = loggerFactory.CreateLogger("DXReporting");
            DevExpress.XtraReports.Web.ClientControls.LoggerService.Initialize((exception, message) =>
            {
                var logMessage = $"[{DateTime.Now}]: Exception occurred. Message: '{message}'. Exception Details:\r\n{exception}";
                reportingLogger.LogError(logMessage);
            });
            DevExpress.XtraReports.Configuration.Settings.Default.UserDesignerOptions.DataBindingMode = DevExpress.XtraReports.UI.DataBindingMode.Expressions;
            app.UseDevExpressControls();

            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseHsts();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            string pathDicomFiles = Path.Combine(env.ContentRootPath, "ArchivosImagenesDiagnosticas");
            if (!Directory.Exists(pathDicomFiles))
            {
                Directory.CreateDirectory(pathDicomFiles);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/octet-stream",
                FileProvider = new PhysicalFileProvider(pathDicomFiles),
                RequestPath = "/ArchivosImagenesDiagnosticas"
            });

            app.UseCookiePolicy();
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
            });

            //app.UseCors("AllowFromAll");

            DApp.LoadTenants(Configuration);

            //DApp.DataBaseSettings = Configuration.GetSection("DatabaseConfig").Get<List<DataBaseSetting>>();
            //if (DApp.DataBaseSettings != null && DApp.DataBaseSettings.Count > 0)
            //{
            //    foreach (var item in DApp.DataBaseSettings)
            //    {
            //        int itries = 0;
            //        Dictionary<string, string> resources = null;
            //        while (resources == null)
            //        {
            //            try
            //            {
            //                resources = new Dominus.Backend.DataBase.BusinessLogic(item).GetBusinessLogic<LanguageResource>().FindAll(x => x.LanguageId == int.Parse(DApp.DefaultLanguage.Id)).ToDictionary(y => y.ResourceKey, z => z.ResourceValue);
            //                DApp.DefaultLanguage.AddResource(DApp.DefaultLanguage.Id, resources);
            //            }
            //            catch (Exception e)
            //            {
            //                itries++;
            //                //Serilog.Log.Error(e.Message);
            //            }
            //            if (itries >= 5)
            //                return;
            //        }

            //        DApp.DataBaseSettings.Find(x => x.Name == item.Name).MenuActionName = new UserBusinessLogic(item).GetMenuActionsNames();
            //        DApp.DataBaseSettings.Find(x => x.Name == item.Name).Name = item.Name.ToLower();
            //        new UserBusinessLogic(item).UpdateSecurityNavigation(null);
            //    }
            //}
            //else
            //    throw new Exception("Error en la configuracion de la base de datos.");


            foreach (var item in DApp.Tenants)
            {
                item.DataBaseSetting.MenuActionName = new UserBusinessLogic(item.DataBaseSetting).GetMenuActionsNames();
            }


            RetryPolicy retryIfException = Policy.Handle<Exception>().WaitAndRetry(5, retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(100, retryAttempt) / 100));
            Dictionary<string, string> resources = null;
            retryIfException.Execute(() => resources = new Dominus.Backend.DataBase.BusinessLogic(DApp.Tenants.FirstOrDefault().DataBaseSetting).GetBusinessLogic<LanguageResource>().FindAll(x => x.LanguageId == int.Parse(DApp.DefaultLanguage.Id)).ToDictionary(y => y.ResourceKey, z => z.ResourceValue));
            //DApp.DefaultLanguage.AddResource(DApp.DefaultLanguage.Id.ToString(), resources);

            DApp.DefaultLanguage.SetAllResources();

            DApp.CloudStorageAccount = Configuration["CloudStorageAccount"];

        }
    }
}
