//Storyreaction.cs
//guest can react to story, everyone can see the number of reactions (the reaction is heart only)

using wedding_api.Models;
using wedding_api.DTOs;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace wedding_api.Services
{
    public class StoryReactionService
    {
        private readonly WedDbContext _dbContext;

        public StoryReactionService(WedDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StoryReaction> AddReaction(int storyId, string reactionType, string sessionHash)
        {
            var existing = await _dbContext.Reactions
                .AnyAsync(r => r.StoryId == storyId && r.SessionHash == sessionHash);

            if (existing) throw new Exception("Already reacted");

            var reaction = new StoryReaction
            {
                StoryId = storyId,
                ReactionType = reactionType,
                SessionHash = sessionHash,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Reactions.Add(reaction);
            await _dbContext.SaveChangesAsync();

            return reaction;
        }

        public async Task<List<StoryReaction>> GetReactionsForStoryMedia(int storyMediaId)
        {
            return await _dbContext.Reactions
                .Where(r => r.StoryId == storyMediaId)
                .ToListAsync();
        }

        public async Task<ReactionCountDTO> GetReactionCountDTO(int storyMediaId)
        {
            var count = await _dbContext.Reactions
                .CountAsync(r => r.StoryId == storyMediaId && r.ReactionType.ToLower() == "heart");

            return new ReactionCountDTO
            {
                StoryId = storyMediaId,
                HeartCount = count
            };
        }


    }
}

