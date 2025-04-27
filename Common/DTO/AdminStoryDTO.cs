//AdminStoryDTO.cs
using wedding_api.Models;

namespace wedding_api.DTOs
{
    public class AdminStoryDTO
    {
        public int? StoryId { get; set; }
        public int WeddingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public string MediaType { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}


public class AdminStoryDTOInput
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
    }

