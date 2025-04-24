using Microsoft.EntityFrameworkCore;
using wedding_api.Models;
using wedding_api.DTOs;

namespace wedding_api.Services.AdminServices
{
    public class CreateStoryService
    {
        private readonly WedDbContext _db;

        public CreateStoryService(WedDbContext db)
        {
            _db = db;
        }

        public async Task<AdminStoryDTO> CreateStory(int weddingId, AdminStoryDTO dto)
        {
            // Automatically detect the media type from the file extension
            var mediaType = GetMediaTypeFromFileExtension(dto.FileUrl);

            var story = new AdminStory
            {
                WeddingId = weddingId,
                Title = dto.Title,
                Description = dto.Description,
                FileUrl = dto.FileUrl,
                MediaType = mediaType, // Automatically assigned based on file extension
                UploadedAt = DateTime.UtcNow
            };

            _db.AdminStories.Add(story);
            await _db.SaveChangesAsync();

            return new AdminStoryDTO
            {
                StoryId = story.StoryId,
                WeddingId = story.WeddingId,
                Title = story.Title,
                Description = story.Description,
                FileUrl = story.FileUrl,
                UploadedAt = story.UploadedAt
            };
        }

        private string GetMediaTypeFromFileExtension(string fileUrl)
        {
            var fileExtension = System.IO.Path.GetExtension(fileUrl).ToLower();

            switch (fileExtension)
            {
                case ".mp4":
                    return "video/mp4";
                case ".mp3":
                    return "audio/mp3";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                default:
                    return "application/octet-stream"; // Fallback type
            }
        }
    }
}
