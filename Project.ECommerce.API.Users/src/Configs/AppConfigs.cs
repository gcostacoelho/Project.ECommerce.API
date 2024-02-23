using Microsoft.EntityFrameworkCore;

using Project.ECommerce.API.Users.src.Database;
using Project.ECommerce.API.Users.src.Interfaces;
using Project.ECommerce.API.Users.src.Models.Utils;

using Project.ECommerce.API.Users.src.Repositories.Interfaces;
using Project.ECommerce.API.Users.src.Repositories.UserRepository;
using Project.ECommerce.API.Users.src.Repositories.LoginRepository;
using Project.ECommerce.API.Users.src.Repositories.AddressRepository;

namespace Project.ECommerce.API.Users.src.Configs;
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
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IAddressRepository, AddressRepository>()
            .AddScoped<ILoginRepository, LoginRepository>();

        // Services
        

        // Singletons
        services.AddSingleton<IAppSettings, AppSettings>();
    }
}