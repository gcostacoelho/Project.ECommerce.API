using Project.ECommerce.API.Users.src.Interfaces;

namespace Project.ECommerce.API.Users.src.Models.Utils;
public class AppSettings(IConfiguration configuration) : IAppSettings
{
    private readonly IConfiguration _config = configuration;

}