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

        [Required]
        public int AdminId { get; set; }  // Add this property

        [Required]
        public int WeddingId { get; set; }

        [ForeignKey("WeddingId")]
        public WeddingProfile Wedding { get; set; }

        [ForeignKey("AdminId")]
        public Admin Admin { get; set; }


        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string FileUrl { get; set; }

        [Required]
        public string MediaType { get; set; } // image/video

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // One Story -> Many Reactions
        public ICollection<StoryReaction> StoryReactions { get; set; }
    }
}
