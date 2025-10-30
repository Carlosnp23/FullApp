using FullApp.Api.Entities;
using System.Threading.Tasks;

public interface IUserService
{
    Task<User> RegisterAsync(string email, string password, string fullName, string role = "User");
    Task<User> AuthenticateAsync(string email, string password);
    Task<User> GetByIdAsync(int id);
}
