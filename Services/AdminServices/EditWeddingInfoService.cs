using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using wedding_api.Models;
using wedding_api.DTOs;

namespace wedding_api.Services.AdminServices
{
    public class EditWeddingInfoService
    {
        private readonly WedDbContext _dbContext;



        public EditWeddingInfoService(WedDbContext dbContext)
        {
            _dbContext = dbContext;


        }


        public async Task<WeddingProfile> UpdateWeddingInfo(int weddingId, int adminId, WeddingProfileDTO weddingDTO)


        {

            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var wedding = await _dbContext.Weddings
                    .FirstOrDefaultAsync(w => w.Id == weddingId && w.AdminId == adminId);

                if (wedding == null)
                    throw new Exception("Unauthorized");

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
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }




        }


    }
}



