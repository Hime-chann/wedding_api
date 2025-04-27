//WeddingProfileDTO.cs

namespace wedding_api.DTOs
{
    namespace wedding_api.DTOs
    {
        public class WeddingProfileDTO
        {
            public string EventTitle { get; set; }
            public string BrideName { get; set; }
            public string GroomName { get; set; }
            public DateTime WeddingDate { get; set; }
            public string? EventPictureUrl { get; set; }
            public string? BackgroundPictureUrl { get; set; }
            public string? Bio { get; set; }
        }
    }

}

