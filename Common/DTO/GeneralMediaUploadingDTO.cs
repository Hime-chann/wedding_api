//GeneralMediauploadingDTO.cs
//mainly for guest to upload pics (can be used for admin too)


namespace wedding_api.DTOs

{ 
public class GeneralMediaUploadingDTO
{
    public string ContentUrl { get; set; }
    public string UploadedBy { get; set; } // 'admin' or 'guest'
    public string MediaType { get; set; }
}
}