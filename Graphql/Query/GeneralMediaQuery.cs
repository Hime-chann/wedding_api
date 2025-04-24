//GeneralMediaQuery.cs
//to fetch all the general media uploaded by guests and admin of a certain wedding

using wedding_api.Models;
using wedding_api.Services;

namespace wedding_api.GraphQL.Queries
{

    [ExtendObjectType("Query")]
    public class GeneralMediaQuery
    {
        private readonly GeneralMediaUploadingService _mediaService;

        public GeneralMediaQuery(GeneralMediaUploadingService mediaService)
        {
            _mediaService = mediaService;
        }

        public async Task<List<GeneralMediaUploading>> GetGenMediaForWedding(int weddingId)
        {
            return await _mediaService.GetGenMediaByWeddingId(weddingId);
        }
    }

}
