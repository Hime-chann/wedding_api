//AdminStory.cs

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wedding_api.Models
{
    public class AdminStory
    {
        [Key]
        public int StoryId { get; set; }
        public int WeddingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public string MediaType { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public WeddingProfile Wedding { get; set; }
    }
}