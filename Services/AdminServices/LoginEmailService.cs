using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using wedding_api.Models;

namespace wedding_api.Services.AdminServices
{
    public class LoginEmailService
    {
        private readonly IDbContextFactory<WedDbContext> _contextFactory;
        private readonly PasswordHashService _passwordHashService;
        private readonly IConfiguration _configuration;

        public LoginEmailService(IDbContextFactory<WedDbContext> contextFactory, PasswordHashService passwordHashService, IConfiguration configuration)
        {
            _contextFactory = contextFactory;
            _passwordHashService = passwordHashService;
            _configuration = configuration;
        }

        public async Task<Responses> AuthenticateUserAsync(string email, string password)
        {
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

            // Create JWT token if login is successful
            var token = GenerateJwtToken(user);

            return new Responses
            {
                Success = true,
                Message = "Login successful.",
                Token = token  // Add token to the response
            };
        }

        private string GenerateJwtToken(Admin user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.AdminId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("AdminId", user.AdminId.ToString()), // explicit claim
                // Add more claims as needed
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
