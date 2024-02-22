using Project.ECommerce.API.Orders.src.Interfaces;

namespace Project.ECommerce.API.Orders.src.Models.Utils;
public class AppSettings(IConfiguration configuration) : IAppSettings
{
    private readonly IConfiguration _config = configuration;

}