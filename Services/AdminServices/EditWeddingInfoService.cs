using Microsoft.EntityFrameworkCore;
using wedding_api.Models;
using wedding_api.DTOs;
using System;
using wedding_api.DTOs.wedding_api.DTOs;

namespace wedding_api.Services.AdminServices
{
    public class EditWeddingInfoService
    {
        private readonly IDbContextFactory<WedDbContext> _contextFactory;

        public EditWeddingInfoService(IDbContextFactory<WedDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<WeddingProfile> UpdateWeddingInfo(int adminId, WeddingProfileDTO weddingDTO)
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var wedding = await _dbContext.Weddings
                    .FirstOrDefaultAsync(w => w.AdminId == adminId);

                if (wedding == null)
                    throw new Exception("No wedding profile found for this admin.");

                wedding.EventTitle = weddingDTO.EventTitle;
                wedding.BrideName = weddingDTO.BrideName;
                wedding.GroomName = weddingDTO.GroomName;
                wedding.WeddingDate = weddingDTO.WeddingDate;
                wedding.EventPictureUrl = weddingDTO.EventPictureUrl;
                wedding.BackgroundPictureUrl = weddingDTO.BackgroundPictureUrl;
                wedding.Bio = weddingDTO.Bio;

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return wedding;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
