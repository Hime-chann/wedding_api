//GeneralMediaUploadingService.cs
//Guests and admin can upload a media

using Microsoft.EntityFrameworkCore;
using wedding_api.DTOs;
using wedding_api.Models;

namespace wedding_api.Services
{
    public class GeneralMediaUploadingService
    {
        private readonly WedDbContext _dbContext;

        public GeneralMediaUploadingService(WedDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GeneralMediaUploading> UploadGenMedia(int weddingId, GeneralMediaUploadingDTO dto)
        {
            var media = new GeneralMediaUploading
            {
                WeddingId = weddingId,
                ContentUrl = dto.ContentUrl,
                UploadedBy = dto.UploadedBy,
                MediaType = dto.MediaType,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.GenMedia.Add(media);
            await _dbContext.SaveChangesAsync();

            return media;
        }

        public async Task<List<GeneralMediaUploading>> GetGenMediaByWeddingId(int weddingId)
        {
            return await _dbContext.GenMedia
                .Where(m => m.WeddingId == weddingId)
                .ToListAsync();
        }

        public async Task<GeneralMediaUploading> ToggleGenMediaPrivacy(int mediaId)
        {
            var media = await _dbContext.GenMedia.FindAsync(mediaId);
            if (media != null)
            {
                // Optional: media.IsPrivate = !media.IsPrivate;
                await _dbContext.SaveChangesAsync();
                return media;
            }
            return null;
        }

        public async Task<bool> DeleteGenMedia(int mediaId)
        {
            var media = await _dbContext.GenMedia.FindAsync(mediaId);
            if (media != null)
            {
                _dbContext.GenMedia.Remove(media);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

}
