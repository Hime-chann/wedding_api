//StoryReactionDTO.cs
//for guest to react to couple's story
//count the number of reactions

namespace wedding_api.DTOs
{
    public class StoryReactionDTO
    {
        public string ReactionType { get; set; }
        public string SessionHash { get; set; }
    }


    public class ReactionCountDTO
    {
        public int StoryMediaId { get; set; }
        public int HeartCount { get; set; }
    }
}
