//GeneralMediaUploadingMutation.cs


using wedding_api.DTOs;
using wedding_api.Models;
using wedding_api.Services;

namespace wedding_api.GraphQL.Mutations
{


    [ExtendObjectType(Name = "Mutation")]
    public class GeneralMediaUploadingMutation
    {
        private readonly GeneralMediaUploadingService _mediaService;

        public GeneralMediaUploadingMutation(GeneralMediaUploadingService mediaService)
        {
            _mediaService = mediaService;
        }

        public async Task<GeneralMediaUploading> UploadGenMedia(
            int weddingId, string contentUrl, string mediaType, string uploadedByNickname)
        {
            var dto = new GeneralMediaUploadingDTO
            {
                ContentUrl = contentUrl,
                UploadedBy = uploadedByNickname,
                MediaType = mediaType
            };

            return await _mediaService.UploadGenMedia(weddingId, dto);
        }

        public async Task<GeneralMediaUploading> ToggleGenMediaPrivacy(int mediaId)
        {
            return await _mediaService.ToggleGenMediaPrivacy(mediaId);
        }

        public async Task<bool> DeleteGenMedia(int mediaId)
        {
            return await _mediaService.DeleteGenMedia(mediaId);
        }
    }

}
