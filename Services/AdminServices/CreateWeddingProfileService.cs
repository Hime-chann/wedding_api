using Microsoft.EntityFrameworkCore;
using wedding_api.DTOs;
using wedding_api.DTOs.wedding_api.DTOs;
using wedding_api.Models;

namespace wedding_api.Services.AdminServices
{
    public class CreateWeddingProfileService
    {
        private readonly IDbContextFactory<WedDbContext> _contextFactory;
        private readonly GenerateQrcodeService _qrService;

        public CreateWeddingProfileService(IDbContextFactory<WedDbContext> contextFactory, GenerateQrcodeService qrService)
        {
            _contextFactory = contextFactory;
            _qrService = qrService;
        }

        public async Task<WeddingProfile> CreateWedding(int adminId, WeddingProfileDTO dto)
        {
            using var dbContext = _contextFactory.CreateDbContext();

            if (!await dbContext.Admins.AnyAsync(a => a.AdminId == adminId))
                throw new ArgumentException("Invalid AdminId.");

            bool hasExistingWedding = await dbContext.Weddings.AnyAsync(w => w.AdminId == adminId);
            if (hasExistingWedding)
                throw new InvalidOperationException("This admin already has a wedding profile.");

            var wedding = new WeddingProfile
            {
                AdminId = adminId,
                EventTitle = dto.EventTitle,
                BrideName = dto.BrideName,
                GroomName = dto.GroomName,
                WeddingDate = dto.WeddingDate,
                EventPictureUrl = dto.EventPictureUrl,
                BackgroundPictureUrl = dto.BackgroundPictureUrl,
                Bio = dto.Bio,
                QrCodeHash = Guid.NewGuid().ToString("N"),
                CreatedAt = DateTime.UtcNow
            };

            var tempWedding = new WeddingProfile { QrCodeHash = wedding.QrCodeHash };
            wedding.QrCodeImageUrl = await _qrService.GenerateAndStoreQrCode(tempWedding);

            dbContext.Weddings.Add(wedding);
            await dbContext.SaveChangesAsync();

            return wedding;
        }

        public async Task<WeddingProfile> GetWeddingByQrCodeHash(string qrCodeHash)
        {
            using var dbContext = _contextFactory.CreateDbContext();

            return await dbContext.Weddings
                .FirstOrDefaultAsync(w => w.QrCodeHash == qrCodeHash);
        }

        public async Task<WeddingProfile> GetWeddingByFullQrCodeUrl(string qrCodeUrl)
        {
            using var dbContext = _contextFactory.CreateDbContext();

            var uri = new Uri(qrCodeUrl);
            var hash = System.IO.Path.GetFileNameWithoutExtension(uri.AbsolutePath);
            return await dbContext.Weddings
                .FirstOrDefaultAsync(w => w.QrCodeHash == hash);
        }

        public async Task<List<WeddingProfile>> GetAllWeddings()
        {
            using var dbContext = _contextFactory.CreateDbContext();

            return await dbContext.Weddings.ToListAsync();
        }
    }
}
