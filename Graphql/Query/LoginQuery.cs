//LoginQuery.cs
using HotChocolate;
using System.Threading.Tasks;
using wedding_api.Models;
using wedding_api.Services.AdminServices;

namespace wedding_api.GraphQL;


[ExtendObjectType("Query")]
public class LoginQuery
{
    private readonly LoginEmailService _loginService;

    public LoginQuery(LoginEmailService loginService)
    {
        _loginService = loginService;
    }



    public async Task<Responses> LoginEmail(string email, string password)
    {
        return await _loginService.AuthenticateUserAsync(email, password);
    }
}
