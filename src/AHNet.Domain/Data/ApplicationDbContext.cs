using AHNet.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace AHNet.Domain.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private IConfiguration _configuration;

        public ApplicationDbContext(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration["AHNetConnectionString"];
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
