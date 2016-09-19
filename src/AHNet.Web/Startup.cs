using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AHNet.Web.Data;
using AHNet.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

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

            services.AddMvc();

            services.AddSingleton(_ => Configuration);

            services.AddTransient<SeedData>();
        }

        public async void Configure(IApplicationBuilder app,
                        IHostingEnvironment env,
                        ILoggerFactory loggerFactory,
                        SeedData seedData)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                LoginPath = new PathString("/Admin/Account/Login"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            };
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute(
                name: "adminRoute",
                template: "{area:exists}/{controller=Dashboard}/{action=Index}");

            routeBuilder.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
