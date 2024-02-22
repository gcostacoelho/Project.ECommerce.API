using Project.ECommerce.API.Shopping.src.Interfaces;

namespace Project.ECommerce.API.Shopping.src.Models.Utils;
public class AppSettings(IConfiguration configuration) : IAppSettings
{
    private readonly IConfiguration _config = configuration;

}