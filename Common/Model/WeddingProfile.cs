//WeddingProfile.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wedding_api.Models
{
    public class WeddingProfile
    {
        [Key]
        public int WeddingId { get; set; }

        [Required]
        public int AdminId { get; set; }

        [ForeignKey("AdminId")]
        public Admin Admin { get; set; }

        [Required]
        public string EventTitle { get; set; }

        [Required]
        public string BrideName { get; set; }

        [Required]
        public string GroomName { get; set; }

        [Required]
        public DateTime WeddingDate { get; set; }

        public string? EventPictureUrl { get; set; }
        public string? BackgroundPictureUrl { get; set; }
        public string? Bio { get; set; }

        [Required]
        public string QrCodeHash { get; set; }

        public string? QrCodeImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // One Wedding -> Many Stories
        public ICollection<AdminStory> Stories { get; set; }

        // One Wedding -> Many Guest Uploads
        public ICollection<GeneralMediaUploading> GeneralMediaUploads { get; set; }
    }
}
