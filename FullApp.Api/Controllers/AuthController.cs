using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// Controller responsible for authentication: register and login.
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtTokenService _tokenService;

    public AuthController(IUserService userService, IJwtTokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    // POST api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)
    {
        // Basic validation (expand with FluentValidation in next steps)
        if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            return BadRequest("Email and password are required.");

        try
        {
            var user = await _userService.RegisterAsync(model.Email, model.Password, model.FullName);
            return Ok(new { user.Id, user.Email, user.FullName, user.Role });
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // POST api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var user = await _userService.AuthenticateAsync(model.Email, model.Password);
        if (user == null)
            return Unauthorized(new { message = "Invalid credentials." });

        var token = _tokenService.CreateToken(user.Id, user.Email, user.Role);
        return Ok(new { token });
    }
}
