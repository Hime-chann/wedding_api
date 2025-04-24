using Microsoft.EntityFrameworkCore;
using wedding_api.Models;
using wedding_api.DTOs;

namespace wedding_api.Services.AdminServices
{
    public class LoginEmailService
    {
        private readonly IDbContextFactory<WedDbContext> _contextFactory;
        private readonly PasswordHashService _passwordHashService;

        public LoginEmailService(IDbContextFactory<WedDbContext> contextFactory, PasswordHashService passwordHashService)
        {
            _contextFactory = contextFactory;
            _passwordHashService = passwordHashService;
        }

        public async Task<Responses> AuthenticateUserAsync(string email, string password)
        {
            // Create a new DbContext instance per request (with fresh connection)
            using var context = _contextFactory.CreateDbContext();

            var user = await context.Admins.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return new Responses
                {
                    Success = false,
                    Message = "User not found.",
                };
            }

            if (!_passwordHashService.VerifyPassword(password, user.PasswordHash))
            {
                return new Responses
                {
                    Success = false,
                    Message = "Invalid credentials.",
                };
            }

            return new Responses
            {
                Success = true,
                Message = "Login successful.",
            };
        }
    }
}
