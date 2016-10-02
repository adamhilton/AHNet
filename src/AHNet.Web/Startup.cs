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
using AHNet.Web.Core.AutoMapper;
using AutoMapper;
using Sakura.AspNetCore.Mvc;

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

            InitializeMapperConfiguration();
        }

        private void InitializeMapperConfiguration()
        {
            _mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfileConfiguration()); });
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        private MapperConfiguration _mapperConfiguration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AHNetDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DataAccessPostgreSqlProvider")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AHNetDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvcWithFeatureRouting();

            services.AddRouting( options => {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });

            services.AddSingleton(_ => Configuration);

            services.AddTransient<SeedData>();

            services.AddScoped<BlogPostRepository>();

            services.AddScoped<ContentTagRepository>();

            services.AddSingleton<IMapper>(sp => _mapperConfiguration.CreateMapper());

            services.AddBootstrapPagerGenerator(options =>
            {
                options.ConfigureDefault();
                options.HideOnSinglePage = true;
                options.PagerItemsForEndings = 0;
                options.ExpandPageItemsForCurrentPage = 2;
            });

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
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                loggerFactory.AddConsole(LogLevel.Error);
                app.UseExceptionHandler("/error/{0}");
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseCookieAuthentication(GetCookieAuthenticationConfiguration());

            app.UseMvc(ConfigureRoutes);

            await seedData.InitializeAsync();
        }

        private static CookieAuthenticationOptions GetCookieAuthenticationConfiguration()
        {
            return new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "Cookie",
                LoginPath = new PathString("/account/login"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            };
        }

        private static void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute(
                name: "default",
                template: "{controller=home}/{action=index}/{id?}");
        }
    }

    public static class StartupExtensionMethods
    {
        public static void AddMvcWithFeatureRouting(this IServiceCollection services)
        {
            services.AddMvc(options => options.Conventions.Add(new FeatureConvention()))
                .AddTagHelpersAsServices()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationFormats.Clear();
                    options.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
                    options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
                });
        }
    }
}
