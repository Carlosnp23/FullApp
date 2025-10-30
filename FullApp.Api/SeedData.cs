using FullApp.Api.Data;
using FullApp.Api.Entities;
using System.Linq;
using System.Threading.Tasks;

// Seeds initial data (admin user + sample products) for development/demo.
public static class SeedData
{
    public static async Task EnsureSeedData(AppDbContext db)
    {
        if (!db.Users.Any())
        {
            var admin = new User
            {
                Email = "admin@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                FullName = "Admin User",
                Role = "Admin"
            };

            db.Users.Add(admin);
        }

        if (!db.Products.Any())
        {
            db.Products.Add(new Product { Name = "Sample Product 1", Description = "First sample", Price = 9.99M, Stock = 10, CreatedByUserId = 1 });
            db.Products.Add(new Product { Name = "Sample Product 2", Description = "Second sample", Price = 19.99M, Stock = 5, CreatedByUserId = 1 });
        }

        await db.SaveChangesAsync();
    }
}
