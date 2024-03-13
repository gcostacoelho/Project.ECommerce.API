namespace Project.ECommerce.API.Authentication.src.Services.Interfaces;
public interface ITokenServices
{
    string CreateTokenAsync(string identity, string email);

    bool? ValidateTokenAsync(string token);
}