//PasswordHashService.cs
//Admin first flow: Signup


using BCrypt.Net;
using wedding_api.Models;
using wedding_api.DTOs;
namespace wedding_api.Services.AdminServices;

public class PasswordHashService
{
    // Hash password before storing it
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Verify the password during login
    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}

