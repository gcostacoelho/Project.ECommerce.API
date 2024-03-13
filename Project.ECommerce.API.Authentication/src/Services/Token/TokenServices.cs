using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Project.ECommerce.API.Authentication.src.Interfaces;
using Project.ECommerce.API.Authentication.src.Services.Interfaces;

namespace Project.ECommerce.API.Authentication.src.Services.Token;
public class TokenServices(IAppSettings appSettings) : ITokenServices
{
    private readonly IAppSettings _appSettings = appSettings;

    public string CreateTokenAsync(string identity, string email)
    {
        var secretEncoded = Encoding.UTF8.GetBytes(_appSettings.Secret);

        var handler = new JwtSecurityTokenHandler();

        var creds = new SigningCredentials(new SymmetricSecurityKey(secretEncoded), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(identity, email),
            SigningCredentials = creds,
            Expires = DateTime.UtcNow.AddDays(1),
        };

        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }

    public bool ValidateTokenAsync(string token)
    {
        throw new NotImplementedException();
    }

    private static ClaimsIdentity GenerateClaims(string identity, string email)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim(ClaimTypes.Email, email));
        ci.AddClaim(new Claim("id", identity));

        return ci;
    }
}