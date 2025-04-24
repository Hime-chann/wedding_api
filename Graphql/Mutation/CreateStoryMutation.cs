using wedding_api.DTOs;
using wedding_api.Services.AdminServices;

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

        public async Task<AdminStoryDTO> CreateStory(int weddingId, AdminStoryDTO dto)
        {
            return await _createStoryService.CreateStory(weddingId, dto);
        }
    }
}
