using Microsoft.EntityFrameworkCore;
using SubIt.Data.Blog;
using SubIt.Data.Security;

namespace SubIt.Data
{
    public class SubItContext : DbContext
    {
        public SubItContext(DbContextOptions<SubItContext> options) : base(options)
        {
        }

        public DbSet<SubItUser> Users { get; set; }
        public DbSet<SubItClaim> Claims { get; set; }
        public DbSet<BlogEntry> BlogEntries { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // singular table names
            modelBuilder.Entity<SubItUser>().ToTable("User");
            modelBuilder.Entity<SubItClaim>().ToTable("Claim");
            modelBuilder.Entity<BlogEntry>().ToTable("BlogEntry");
            modelBuilder.Entity<BlogComment>().ToTable("BlogComment");
        }

    }
}