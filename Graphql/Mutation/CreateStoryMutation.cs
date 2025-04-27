using wedding_api.DTOs;
using wedding_api.Services.AdminServices;
using Microsoft.AspNetCore.Authorization;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace wedding_api.GraphQL.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    public class CreateStoryMutation
    {
        private readonly CreateStoryService _createStoryService;

        public CreateStoryMutation(CreateStoryService createStoryService)
        {
            _createStoryService = createStoryService;
        }

        [Authorize] // 🛡️ still protected
        public async Task<AdminStoryDTO> CreateStory(AdminStoryDTOInput dto, ClaimsPrincipal claimsPrincipal)
        {
            // Extract adminId from the token's claims
            var adminIdString = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(adminIdString) || !int.TryParse(adminIdString, out int adminId))
            {
                throw new UnauthorizedAccessException("Invalid token or admin ID not found in token.");
            }

            return await _createStoryService.CreateStory(adminId, dto);
        }
    }
}
