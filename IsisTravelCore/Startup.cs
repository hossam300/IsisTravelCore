using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IsisTravelCore.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using IsisTravelCore.Models.Domains;

using System.IO;
using IsisTravelCore.Services;


namespace IsisTravelCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultUI()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Default User settings.
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/LogIn";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });
         //   services.AddJsReport(new LocalReporting().UseBinary(JsReportBinary.GetBinary()).AsUtility().Create());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //var context = new CustomAssemblyLoadContext();
            //context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));
           
          //  services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              //  app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseRequestLocalization();

            // UseCookieAuthentication..
            // UseJwtBearerAuthentication..

            //add userculture provider for authenticated user
            var requestOpt = new RequestLocalizationOptions
            {
                SupportedCultures = new List<CultureInfo> { new CultureInfo("es-CL") },
                SupportedUICultures = new List<CultureInfo> { new CultureInfo("es-CL") }
            };
            requestOpt.RequestCultureProviders.Clear();
            requestOpt.RequestCultureProviders.Add(new SingleCultureProvider());

            app.UseRequestLocalization(requestOpt);


        }
        public class SingleCultureProvider : IRequestCultureProvider
        {
            public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
            {
                return Task.Run(() => new ProviderCultureResult("es-CL", "es-CL"));
            }
        }
    }
}
