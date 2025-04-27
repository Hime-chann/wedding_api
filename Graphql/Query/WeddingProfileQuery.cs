using HotChocolate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using wedding_api.Models;

namespace wedding_api.GraphQL
{
    [ExtendObjectType("Query")]
    public class WeddingProfileQuery
    {
        private readonly IDbContextFactory<WedDbContext> _contextFactory;

        public WeddingProfileQuery(IDbContextFactory<WedDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        [Authorize]
        public async Task<WeddingProfile?> GetWeddingProfile(ClaimsPrincipal claimsPrincipal)
        {
            // Create a new context instance from the factory
            using var context = _contextFactory.CreateDbContext();

            // Extract adminId from the token's claims
            var adminIdString = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(adminIdString) || !int.TryParse(adminIdString, out int adminId))
            {
                throw new UnauthorizedAccessException("Invalid token or admin ID not found in token.");
            }

            // Use the admin's ID to find the corresponding wedding profile
            var weddingProfile = await context.Weddings.FirstOrDefaultAsync(w => w.AdminId == adminId);

            // Return the wedding profile (may be null if not found)
            return weddingProfile;
        }
    }
}
