//WeddingProfile.cs


using System;
using System.Collections.Generic;

namespace wedding_api.Models
{
    public class WeddingProfile
    {
        public int Id{ get; set; }
        public int AdminId { get; set; }
        public string EventTitle { get; set; }
        public string BrideName { get; set; }
        public string GroomName { get; set; }
        public DateTime WeddingDate { get; set; }
        public string EventPictureUrl { get; set; }
        public string BackgroundPictureUrl { get; set; }
        public string Bio { get; set; }
        public string QrCodeHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<GeneralMediaUploading> GenMedia { get; set; }
        public Admin Admin { get; set; }
        public string QrCodeImageUrl { get; set; }  // Stores path to generated QR image
   
    }

}