//WeddingQuery.cs
//Fetch Wedding by QR Code Hash
//for entry of guest to the wedding page of a certain couple

using HotChocolate;
//WeddingQuery.cs
//Fetch Wedding by QR Code Hash
//for entry of guest to the wedding page of a certain couple

using HotChocolate;
using wedding_api.Models;
using wedding_api.Services.AdminServices;

namespace wedding_api.GraphQL.Queries
{
    [ExtendObjectType("Query")] // Explicitly extend the Query object type for GraphQL
    public class WeddingQuery
    {
        private readonly CreateWeddingProfileService _weddingService;

        public WeddingQuery(CreateWeddingProfileService weddingService)
        {
            _weddingService = weddingService;
        }

        // Fetch Wedding Profile based on QR Code Hash
        public async Task<WeddingProfile> GetWeddingByQrCodeHash(string qrCodeHash)
        {
            var wedding = await _weddingService.GetWeddingByQrCodeHash(qrCodeHash);

            if (wedding == null)
            {
                // If no wedding found for the provided QR code hash, return null or throw an error.
                throw new ArgumentException("Wedding profile not found for the provided QR code.");
            }

            return wedding;
        }

        // Fetch Wedding Profile based on Full QR Code URL
        public async Task<WeddingProfile> GetWeddingByFullQrCodeUrl(string qrCodeUrl)
        {
            var wedding = await _weddingService.GetWeddingByFullQrCodeUrl(qrCodeUrl);

            if (wedding == null)
            {
                // If no wedding found for the provided full QR code URL, return null or throw an error.
                throw new ArgumentException("Wedding profile not found for the provided full QR code URL.");
            }

            return wedding;
        }
    }
}
