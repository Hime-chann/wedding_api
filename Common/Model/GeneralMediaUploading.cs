//GeneralMediaUploading.cs

using System;
using System.Collections.Generic;

namespace wedding_api.Models
{
    public class GeneralMediaUploading 
    {
        public int Id { get; set; }
        public int WeddingId { get; set; }
        public string ContentUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public WeddingProfile Wedding { get; set; }
        public string UploadedBy { get; set; } // 'admin' or 'guest'
        public string MediaType { get; set; }  // 'image' or 'video'
    }

}
