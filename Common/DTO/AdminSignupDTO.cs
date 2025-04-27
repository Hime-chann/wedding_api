//AdminSignupDTO.cs


using HotChocolate;


namespace wedding_api.DTOs
{
    public class AdminSignupDTO
    {
        public string Email { get; set; }
        public string Password { get; set; } // Plaintext before hashing
    }
}

