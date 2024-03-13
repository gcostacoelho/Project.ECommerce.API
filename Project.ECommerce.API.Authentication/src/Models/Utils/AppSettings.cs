using Project.ECommerce.API.Authentication.src.Interfaces;

namespace Project.ECommerce.API.Authentication.src.Models.Utils;
public class AppSettings(IConfiguration configuration) : IAppSettings
{
    private readonly IConfiguration _config = configuration;

    public string Secret => _config["AppSettings:Secret"];
}