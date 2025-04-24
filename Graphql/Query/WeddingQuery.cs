//WeddingQuery.cs
//Fetch Wedding by QR Code Hash
//for entry of guest to the wedding page of a certain couple

using wedding_api.Models;
using wedding_api.Services.AdminServices;

namespace wedding_api.GraphQL.Queries
{
    public class WeddingQuery
    {
        private readonly CreateWeddingProfileService _weddingService;

        public WeddingQuery(CreateWeddingProfileService weddingService)
        {
            _weddingService = weddingService;
        }

        public async Task<WeddingProfile> GetWeddingByQrCodeHash(string qrCodeHash)
        {
            return await _weddingService.GetWeddingByQrCodeHash(qrCodeHash);
        }


    }
}
