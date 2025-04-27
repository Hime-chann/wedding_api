using Microsoft.EntityFrameworkCore;
using wedding_api.DTOs;
using wedding_api.Models;
using System;
using System.Threading.Tasks;

namespace wedding_api.Services.AdminServices
{
    public class CreateStoryService
    {
        private readonly IDbContextFactory<WedDbContext> _contextFactory;

        public CreateStoryService(IDbContextFactory<WedDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<AdminStoryDTO> CreateStory(int adminId, AdminStoryDTOInput dto)
        {
            using var _dbContext = _contextFactory.CreateDbContext();

            var wedding = await _dbContext.Weddings
                .FirstOrDefaultAsync(w => w.AdminId == adminId);

            if (wedding == null)
                throw new InvalidOperationException("Wedding not found for this admin.");

            var mediaType = GetMediaTypeFromFileExtension(dto.FileUrl);

            var story = new AdminStory
            {
                AdminId = adminId,
                WeddingId = wedding.WeddingId,
                Title = dto.Title,
                Description = dto.Description,
                FileUrl = dto.FileUrl,
                MediaType = mediaType,
                UploadedAt = DateTime.UtcNow
            };

            _dbContext.AdminStories.Add(story);
            await _dbContext.SaveChangesAsync();

            return new AdminStoryDTO
            {
                StoryId = story.StoryId,
                WeddingId = story.WeddingId,
                Title = story.Title,
                Description = story.Description,
                FileUrl = story.FileUrl,
                MediaType = story.MediaType,
                UploadedAt = story.UploadedAt
            };
        }

        private string GetMediaTypeFromFileExtension(string fileUrl)
        {
            var extension = System.IO.Path.GetExtension(fileUrl)?.ToLower();
            return extension switch
            {
                ".mp4" => "video/mp4",
                ".mp3" => "audio/mp3",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };
        }
    }
}
