using Project.ECommerce.API.Authentication.src.Services.Interfaces;

namespace Project.ECommerce.API.Authentication.src.Services.Token;
public class TokenServices : ITokenServices
{
    public string CreateTokenAsync(string identity, string email)
    {
        throw new NotImplementedException();
    }

    public bool ValidateTokenAsync(string token)
    {
        throw new NotImplementedException();
    }
}