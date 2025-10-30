using System;

namespace FullApp.Api.Entities
{
    // Represents an application user.
    public class User
    {
        public int Id { get; set; }

        // Email used as login identifier.
        public string Email { get; set; }

        // Hashed password (do not store plaintext).
        public string PasswordHash { get; set; }

        // Full name of the user.
        public string FullName { get; set; }

        // Role e.g. "Admin" or "User"
        public string Role { get; set; } = "User";

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
