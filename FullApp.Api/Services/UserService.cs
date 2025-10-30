using FullApp.Api.Data;
using FullApp.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// Simple user service to handle registration and authentication.
public class UserService : IUserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext db)
    {
        _db = db;
    }

    // Register a new user. Returns created user (without password).
    public async Task<User> RegisterAsync(string email, string password, string fullName, string role = "User")
    {
        if (await _db.Users.AnyAsync(u => u.Email == email))
            throw new Exception("Email already registered.");

        // Hash password with BCrypt (includes salt).
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            Email = email,
            PasswordHash = passwordHash,
            FullName = fullName,
            Role = role
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        user.PasswordHash = null; // do not return the hash
        return user;
    }

    // Authenticate user and return user if password matches.
    public async Task<User> AuthenticateAsync(string email, string password)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return null;

        var verified = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!verified)
            return null;

        user.PasswordHash = null; // hide hash
        return user;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _db.Users.FindAsync(id);
    }
}
