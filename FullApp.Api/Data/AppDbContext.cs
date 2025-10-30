using FullApp.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullApp.Api.Data
{
    // EF Core DbContext. Maps entities to database tables.
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        // Model configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique index on email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Additional configurations can go here
        }
    }
}
