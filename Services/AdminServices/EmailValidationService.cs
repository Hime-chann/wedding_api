//EmailValidationService.cs
//Admin first flow: Signup

using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using wedding_api.Models;
using wedding_api.DTOs;

namespace wedding_api.Services.AdminServices;

public class ValidemailService
{
    private readonly WedDbContext _db;

    public ValidemailService(WedDbContext db)
    {
        _db = db;
    }

    // Check if email format is valid
    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return emailRegex.IsMatch(email);
    }

    // Check if email already exists in DB
    public bool IsEmailTaken(string email)
    {
        return _db.Admins.Any(u => u.Email == email);
    }
}
