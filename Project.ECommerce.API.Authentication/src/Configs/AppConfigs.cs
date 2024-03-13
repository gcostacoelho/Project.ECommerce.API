using Microsoft.EntityFrameworkCore;

using Project.ECommerce.API.Authentication.src.Database;
using Project.ECommerce.API.Authentication.src.Facades;
using Project.ECommerce.API.Authentication.src.Facades.Interfaces;
using Project.ECommerce.API.Authentication.src.Interfaces;
using Project.ECommerce.API.Authentication.src.Models.Utils;
using Project.ECommerce.API.Authentication.src.Repositories;
using Project.ECommerce.API.Authentication.src.Repositories.Interfaces;
using Project.ECommerce.API.Authentication.src.Services.Interfaces;
using Project.ECommerce.API.Authentication.src.Services.Token;

namespace Project.ECommerce.API.Authentication.src.Configs;
public static class AppConfigs
{
    public static void RegisterDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            op => op.UseSqlServer(
                configuration.GetConnectionString("DockerConnection")
            )
        );
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IAppSettings, AppSettings>();

        // Facades
        services.AddScoped<IAuthenticationFacade, AuthenticationFacade>();

        // Repositories
        services.AddScoped<ILoginRepository, LoginRepository>();

        // Services
        services.AddScoped<ITokenServices, TokenServices>();
    }
}