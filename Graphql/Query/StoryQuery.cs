using Microsoft.EntityFrameworkCore;
using wedding_api.Models;
using wedding_api.Services;

namespace wedding_api.GraphQL.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class StoryQuery
    {
        private readonly WedDbContext _db;

        public StoryQuery(WedDbContext db)
        {
            _db = db;
        }

        public async Task<List<AdminStory>> GetStoriesByWeddingId(int adminId)
        {
            return await _db.AdminStories
                .Where(s => s.AdminId == adminId)
                .ToListAsync();
        }
    }

}
