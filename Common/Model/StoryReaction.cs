//StoryReaction.cs

using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wedding_api.Models
{
    public class StoryReaction
    {
        [Key]
        public int ReactionId { get; set; }

        [Required]
        public int StoryId { get; set; }

        [ForeignKey("StoryId")]
        public AdminStory Story { get; set; }

        [Required]
        public string ReactionType { get; set; } // "heart"

        [Required]
        public string SessionHash { get; set; } // for anti-spam (guest session)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}



