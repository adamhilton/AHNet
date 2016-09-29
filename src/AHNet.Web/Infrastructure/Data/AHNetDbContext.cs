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

            builder
                .Entity<ContentTag>()
                .HasIndex(b => b.Name)
                .IsUnique();

            builder.Entity<BlogPostContentTag>()
                .HasKey(t => new { t.BlogPostId, t.ContentTagId });

            builder.Entity<BlogPostContentTag>()
                .HasOne(pt => pt.BlogPost)
                .WithMany(p => p.BlogPostContentTags)
                .HasForeignKey(pt => pt.BlogPostId);

            builder.Entity<BlogPostContentTag>()
                .HasOne(pt => pt.ContentTag)
                .WithMany(t => t.BlogPostContentTags)
                .HasForeignKey(pt => pt.ContentTagId);

            base.OnModelCreating(builder);
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<ContentTag> ContentTags { get; set; }
    }
}
