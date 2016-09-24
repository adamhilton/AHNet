using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AHNet.Web.Infrastructure.Data;
using AHNet.Web.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using AHNet.Web.Core;
using AHNet.Web.Core.Interfaces;

namespace AHNet.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            CurrentEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AHNetDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DataAccessPostgreSqlProvider")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AHNetDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(options => options.Conventions.Add(new FeatureConvention()))
                .AddRazorOptions(options => {
                    // {0} - Action Name
                    // {1} - Controller Name
                    // {2} - Area Name
                    // {3} - Feature Name
                    // Replace normal view location entirely
                    options.ViewLocationFormats.Clear();
                    options.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
                    options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
                });

            services.AddSingleton(_ => Configuration);

            services.AddTransient<SeedData>();

            services.AddScoped<AHNetDbContext>();
            
            services.AddScoped<BlogPostRepository>();
        }

        public async void Configure(IApplicationBuilder app,
                        IHostingEnvironment env,
                        ILoggerFactory loggerFactory,
                        SeedData seedData)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug(LogLevel.Information);
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                loggerFactory.AddConsole(LogLevel.Error);
                
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Error/{0}");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseCookieAuthentication(GetCookieAuthenticationConfiguration());

            app.UseMvc(ConfigureRoutes);

            await seedData.InitializeAsync();
        }

        private CookieAuthenticationOptions GetCookieAuthenticationConfiguration()
        {
            return new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "Cookie",
                LoginPath = new PathString("/Account/Login"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            };
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
