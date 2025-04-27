//createweddingprofilemutation.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using wedding_api.DTOs;
using wedding_api.Models;
using wedding_api.Services.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using wedding_api.DTOs.wedding_api.DTOs;

namespace wedding_api.GraphQL.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    public class CreateWeddingProfileMutation
    {
        private readonly CreateWeddingProfileService _createWeddingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateWeddingProfileMutation(CreateWeddingProfileService createWeddingService, IHttpContextAccessor httpContextAccessor)
        {
            _createWeddingService = createWeddingService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        public async Task<WeddingProfile> CreateWedding(WeddingProfileDTO input)
        {
            // Get the authenticated user ID from the HTTP context
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null || !httpContext.User.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated.");

            // Extract the AdminId from the ClaimsPrincipal
            var adminIdClaim = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(adminIdClaim))
                throw new UnauthorizedAccessException("AdminId claim is missing.");

            int adminId = int.Parse(adminIdClaim);
            return await _createWeddingService.CreateWedding(adminId, input);
        }
    }
}