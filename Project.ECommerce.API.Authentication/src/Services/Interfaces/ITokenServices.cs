namespace Project.ECommerce.API.Authentication.src.Services.Interfaces;
public interface ITokenServices
{
    string CreateTokenAsync(string identity, string email);

    string ValidateTokenAsync(string token);
}