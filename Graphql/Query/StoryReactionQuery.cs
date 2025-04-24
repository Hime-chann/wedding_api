//storyreaction.cs
//to fetch the reaction count of a story

using wedding_api.DTOs;
using wedding_api.Models;
using wedding_api.Services;

namespace wedding_api.GraphQL.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class StoryReactionQuery
    {
        private readonly StoryReactionService _reactionService;

        // Query to get all reactions for a story media
        public async Task<List<StoryReaction>> GetReactionsForStoryMedia(int storyMediaId)
        {
            return await _reactionService.GetReactionsForStoryMedia(storyMediaId);
        }

        // Query to get the count of heart reactions for a specific story
        public async Task<ReactionCountDTO> GetReactionCount(int storyMediaId)
        {
            return await _reactionService.GetReactionCountDTO(storyMediaId);
        }


    }
}
