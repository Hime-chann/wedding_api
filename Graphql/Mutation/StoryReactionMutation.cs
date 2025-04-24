//StoryreactionMutation

using wedding_api.Models;
using wedding_api.Services;

namespace wedding_api.GraphQL.Mutations
{

    [ExtendObjectType(Name = "Mutation")]
    public class StoryReactionMutation
    {
        private readonly StoryReactionService _reactionService;

        public StoryReactionMutation(StoryReactionService reactionService)
        {
            _reactionService = reactionService;
        }

        // Mutation for adding a reaction to media
        public async Task<StoryReaction> AddReactionToStory(int storyMediaId, string reactionType, string sessionHash)
        {
            return await _reactionService.AddReaction(storyMediaId, reactionType, sessionHash);
        }

    }
}
