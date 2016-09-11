using AHNet.Web.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AHNet.Web.Data
{
    public class AHNetDbContext : IdentityDbContext<User>
    {
        public AHNetDbContext(DbContextOptions<AHNetDbContext> options)
            : base(options)
        {
        }
    }
}
