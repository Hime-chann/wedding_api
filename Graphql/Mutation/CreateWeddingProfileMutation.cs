//CreateWeddingProfileMutation.cs
//for admin only

using wedding_api.DTOs;
using wedding_api.Models;
using wedding_api.Services.AdminServices;

namespace wedding_api.GraphQL.Mutations
{

    [ExtendObjectType(Name = "Mutation")] 
    public class CreateWeddingProfileMutation
    {
        private readonly Services.AdminServices.CreateWeddingProfileService _weddingService;

        public CreateWeddingProfileMutation(Services.AdminServices.CreateWeddingProfileService weddingService)
        {
            _weddingService = weddingService;
        }

        // Mutation for creating a wedding


        public async Task<WeddingProfile> CreateWedding(WeddingProfileDTO weddingDTO)
        {
            return await _weddingService.CreateWedding(weddingDTO);
        }
    }
}
