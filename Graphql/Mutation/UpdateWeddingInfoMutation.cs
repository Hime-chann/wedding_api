//UpdateWeddingInfoMutation.cs

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using wedding_api.DTOs;
using wedding_api.DTOs.wedding_api.DTOs;
using wedding_api.Models;
using wedding_api.Services.AdminServices;

namespace wedding_api.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class UpdateWeddingInfoMutation
    {
        private readonly EditWeddingInfoService _adminService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateWeddingInfoMutation(EditWeddingInfoService adminService, IHttpContextAccessor httpContextAccessor)
        {
            _adminService = adminService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Mutation to update wedding info
        [Authorize]
        public async Task<WeddingProfile> UpdateWeddingInfo(WeddingProfileDTO weddingDTO)
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

            // Call the service method to update wedding info for this admin
            try
            {
                return await _adminService.UpdateWeddingInfo(adminId, weddingDTO);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception("You are not authorized to perform this action.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the wedding info: " + ex.Message, ex);
            }
        }
    }
}