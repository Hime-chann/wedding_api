//GeneralMediaUploading.cs

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wedding_api.Models
{
    public class GeneralMediaUploading
    {
        [Key]
        public int MediaId { get; set; }

        [Required]
        public int WeddingId { get; set; }

        [ForeignKey("WeddingId")]
        public WeddingProfile Wedding { get; set; }

        [Required]
        public string ContentUrl { get; set; }

        [Required]
        public string UploadedBy { get; set; } // "admin" or "guest"

        [Required]
        public string MediaType { get; set; } // "image" or "video"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}



