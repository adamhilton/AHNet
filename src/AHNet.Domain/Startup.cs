using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AHNet.Domain.Entities;
using AHNet.Domain.Data;

namespace AHNet.Domain
{
    public class Startup
    {
        private IConfigurationRoot _config { get; set; }
        
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();

            _config = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services
            services.AddInstance(_config);
        }

        public void Configure(IApplicationBuilder app)
        {
            // Add middleware here...
        }
    }
}
