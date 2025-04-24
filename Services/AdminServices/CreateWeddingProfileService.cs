//CreateWeddingProfileService.cs
//Admin Second flow: Wedding Creation


using Microsoft.EntityFrameworkCore;
using wedding_api.DTOs;
using wedding_api.Models;

namespace wedding_api.Services.AdminServices
{
    public class CreateWeddingProfileService
    {
        private readonly WedDbContext _dbContext;
        private readonly GenerateQrcodeService _qrService;

        public CreateWeddingProfileService(WedDbContext dbContext, GenerateQrcodeService qrService)
        {
            _dbContext = dbContext;
            _qrService = qrService;
        }

        public async Task<WeddingProfile> CreateWedding(WeddingProfileDTO weddingDTO)
        {
            // Validate Admin exists
            var adminExists = await _dbContext.Admins
        .AnyAsync(a => a.AdminId == weddingDTO.AdminId); // "AdminID" matches model
            if (!adminExists)
            {
                throw new ArgumentException("Invalid AdminId. Admin does not exist.");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(weddingDTO.EventTitle))
                throw new ArgumentException("EventTitle is required.");
            if (string.IsNullOrWhiteSpace(weddingDTO.BrideName))
                throw new ArgumentException("BrideName is required.");
            if (string.IsNullOrWhiteSpace(weddingDTO.GroomName))
                throw new ArgumentException("GroomName is required.");
            if (weddingDTO.WeddingDate == default)
                throw new ArgumentException("WeddingDate is required.");

            var wedding = new WeddingProfile
            {
                AdminId = weddingDTO.AdminId,
                EventTitle = weddingDTO.EventTitle,
                BrideName = weddingDTO.BrideName,
                GroomName = weddingDTO.GroomName,
                WeddingDate = weddingDTO.WeddingDate,
                EventPictureUrl = weddingDTO.EventPictureUrl,
                BackgroundPictureUrl = weddingDTO.BackgroundPictureUrl,
                Bio = weddingDTO.Bio,
                QrCodeHash = Guid.NewGuid().ToString("N"),
                CreatedAt = DateTime.UtcNow
            };

            // ⬇ Generate QR Code before adding to DB
            var tempWedding = new WeddingProfile { QrCodeHash = wedding.QrCodeHash };
            wedding.QrCodeImageUrl = await _qrService.GenerateAndStoreQrCode(tempWedding);

            _dbContext.Weddings.Add(wedding);
            await _dbContext.SaveChangesAsync();

            return wedding;
        }



        public async Task<WeddingProfile> GetWeddingByQrCodeHash(string qrCodeHash)
        {
            return await _dbContext.Weddings
                .FirstOrDefaultAsync(w => w.QrCodeHash == qrCodeHash);
        }

        public async Task<List<WeddingProfile>> GetAllWeddings()
        {
            return await _dbContext.Weddings.ToListAsync();
        }
    }
}
