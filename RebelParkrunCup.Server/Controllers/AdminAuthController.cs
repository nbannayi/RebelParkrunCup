using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

[ApiController]
[Route("api/auth")]
public class AdminAuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AdminAuthController(IConfiguration configuration)
    {
        // Comment out when password obtained.
        //string rawPassword = <password>;
        //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(rawPassword);
        //Console.WriteLine($"Password: {rawPassword}");
        //Console.WriteLine($"Hashed Password: {hashedPassword}");

        _configuration = configuration;
    }

    // Endpoint to validate password
    [HttpPost("validate-admin")]
    public IActionResult ValidateAdmin([FromBody] AdminPasswordRequest request)
    {
         // This should be a hashed password stored in your app settings or database
         // Potentially replace with env variable or secure key value store at some ppint. vv
        string? storedPasswordHash = _configuration["AdminPasswordHash"];
        
        if (string.IsNullOrEmpty(storedPasswordHash))
            return BadRequest(new { Message = "Admin password hash is not configured." });

        if (BCrypt.Net.BCrypt.Verify(request.Password, storedPasswordHash))
        {
            // If the password is correct, set the admin state in the session
            HttpContext.Session.SetString("IsAdmin", "true");
            return Ok(new { Message = "Admin mode activated." });
        }

        return Unauthorized(new { Message = "Incorrect password." });
    }

    // Endpoint to log out of admin mode
    [HttpPost("logout-admin")]
    public IActionResult LogoutAdmin()
    {
        HttpContext.Session.Remove("IsAdmin"); // Remove admin state from session
        return Ok();
    }

    // Endpoint to check if the user is in admin mode
    [HttpGet("is-admin")]
    public IActionResult IsAdmin()
    {
        bool isAdmin = HttpContext.Session.GetString("IsAdmin") == "true";
        return isAdmin ? Ok() : Unauthorized();
    }
}

public class AdminPasswordRequest
{
    public string Password { get; set; } = string.Empty;
}