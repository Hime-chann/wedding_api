//Responses.cs
namespace wedding_api.Models;
public class Responses
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }  // Add this property for JWT token

}
