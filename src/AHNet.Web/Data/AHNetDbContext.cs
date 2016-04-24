using AHNet.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace AHNet.Data
{
    public class AHNetDbContext : IdentityDbContext<User>
    {
        private IConfiguration _configuration;

        public AHNetDbContext(IConfigurationRoot configuration)
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
