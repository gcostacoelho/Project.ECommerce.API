using Project.ECommerce.API.Payment.src.Interfaces;

namespace Project.ECommerce.API.Payment.src.Models.Utils;
public class AppSettings(IConfiguration configuration) : IAppSettings
{
    private readonly IConfiguration _config = configuration;

}