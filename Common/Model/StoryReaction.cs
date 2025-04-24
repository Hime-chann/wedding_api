//StoryReaction.cs

using System;

namespace wedding_api.Models
{
    public class StoryReaction
    {
        public int Id { get; set; }
        public int StoryMediaId { get; set; }
        public string ReactionType { get; set; } //heart
        public string SessionHash { get; set; } // For limiting spam
        public DateTime CreatedAt { get; set; }
        public ICollection<StoryReaction> Reactions { get; set; }

    }
}
