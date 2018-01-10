using Blog.Data.Blog;
using Blog.Data.Security;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public DbSet<BlogUser> Users { get; set; }
        public DbSet<BlogClaim> Claims { get; set; }
        public DbSet<BlogEntry> BlogEntries { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use singular table names
            modelBuilder.Entity<BlogUser>().ToTable("User");
            modelBuilder.Entity<BlogClaim>().ToTable("Claim");
            modelBuilder.Entity<BlogEntry>().ToTable("BlogEntry");
            modelBuilder.Entity<BlogComment>().ToTable("BlogComment");
        }

    }
}