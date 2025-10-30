using System;

namespace FullApp.Api.Entities
{
    // Simple product entity for CRUD operations.
    public class Product
    {
        public int Id { get; set; }

        // Product name
        public string Name { get; set; }

        // Product description
        public string Description { get; set; }

        // Price stored as decimal
        public decimal Price { get; set; }

        // Stock quantity
        public int Stock { get; set; }

        // Who created the product (FK)
        public int CreatedByUserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
