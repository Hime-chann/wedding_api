//UpdateWeddingInfoMutation.cs

using wedding_api.DTOs;
using wedding_api.Models;
using wedding_api.Services.AdminServices;

namespace wedding_api.GraphQL.Mutations
{

    [ExtendObjectType("Mutation")]
    public class UpdateWeddingInfoMutation
    {
        private readonly EditWeddingInfoService _adminService;

        public UpdateWeddingInfoMutation(EditWeddingInfoService adminService)
        {
            _adminService = adminService;
        }


        // Mutation to update wedding info
        public async Task<WeddingProfile> UpdateWeddingInfo(int weddingId, int adminId, WeddingProfileDTO weddingDTO)
        {
            return await _adminService.UpdateWeddingInfo(weddingId, adminId, weddingDTO);
        }



    }
}
