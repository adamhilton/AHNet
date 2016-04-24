using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Identity.EntityFramework;
using AHNet.Entities;
using AHNet.Data;
using AHNet.Web.Data;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;

namespace AHNet.Web
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework()
                    .AddNpgsql()
                    .AddDbContext<AHNetDbContext>();

            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<AHNetDbContext>();
            
            services.AddSingleton(_ => Configuration);

            services.AddTransient<SeedData>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
         public async void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env, 
                              ILoggerFactory loggerFactory,
                              SeedData seedData)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<AHNetDbContext>()
                            .Database.Migrate();
                    }
                }
                catch { }
            }
            
            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseCookieAuthentication(options =>
            {
                options.AuthenticationScheme = "Cookie";
                options.LoginPath = new PathString("/Admin/Account/Login");
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
            });
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "adminRoute",
                    template: "{area:exists}/{controller=Dashboard}/{action=Index}");
            });


            await seedData.InitializeAsync();

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<AHNet.Web.Startup>(args);
    }
}
