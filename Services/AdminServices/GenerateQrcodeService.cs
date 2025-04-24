// GenerateQrcodeService.cs
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using wedding_api.Models;
using System.IO; // Explicitly use this for Path to avoid HotChocolate.Path conflict

namespace wedding_api.Services.AdminServices
{
    public class GenerateQrcodeService
    {
        private readonly IWebHostEnvironment _env;
        private readonly WedDbContext _db;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenerateQrcodeService(
            IWebHostEnvironment env,
            WedDbContext db,
            IConfiguration config,
            IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _db = db;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GenerateAndStoreQrCode(WeddingProfile tempWedding)
        {
            if (tempWedding == null || string.IsNullOrWhiteSpace(tempWedding.QrCodeHash))
                throw new ArgumentException("QrCodeHash is required to generate a QR code.");

            var frontendBaseUrl = _config["Frontend:BaseUrl"] ?? "http://localhost:3000";
            var qrContent = $"{frontendBaseUrl}/wedding/{tempWedding.QrCodeHash}";

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCode.GetGraphic(20);

            var wwwrootPath = _env.WebRootPath ?? System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var qrcodeDir = System.IO.Path.Combine(wwwrootPath, "qrcodes");
            Directory.CreateDirectory(qrcodeDir);

            var fileName = $"{tempWedding.QrCodeHash}.png";
            var filePath = System.IO.Path.Combine(qrcodeDir, fileName);
            await File.WriteAllBytesAsync(filePath, qrCodeBytes);

            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}";
            var imageUrl = $"{baseUrl}/qrcodes/{fileName}";

            return imageUrl;
        }
    }
}
