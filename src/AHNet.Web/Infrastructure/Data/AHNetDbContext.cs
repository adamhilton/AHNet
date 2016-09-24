using AHNet.Web.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AHNet.Web.Infrastructure.Data
{
    public class AHNetDbContext : IdentityDbContext<User>
    {
        public AHNetDbContext(DbContextOptions<AHNetDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.
                Entity<BlogPost>()
                .HasIndex(b => b.Title)
                .IsUnique();
            
            base.OnModelCreating(builder);
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}
