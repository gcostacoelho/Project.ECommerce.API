using RestEase;
using Microsoft.EntityFrameworkCore;

using Project.ECommerce.API.Users.src.Database;
using Project.ECommerce.API.Users.src.Interfaces;
using Project.ECommerce.API.Users.src.Models.Utils;

using Project.ECommerce.API.Users.src.Repositories.Interfaces;
using Project.ECommerce.API.Users.src.Repositories.UserRepository;
using Project.ECommerce.API.Users.src.Repositories.LoginRepository;
using Project.ECommerce.API.Users.src.Repositories.AddressRepository;

using Project.ECommerce.API.Users.src.Services.Interfaces;
using Project.ECommerce.API.Users.src.Services.UserServices;
using Project.ECommerce.API.Users.src.Services.LoginServices;
using Project.ECommerce.API.Users.src.Services.AddressServices;
using Project.ECommerce.API.Users.src.Services.RestEaseServices;
using Project.ECommerce.API.Users.src.Authentication;

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

    public static void RegisterSingletons(this IServiceCollection services, IConfiguration configuration)
    {
        // Singletons
        services.AddSingleton<IAppSettings, AppSettings>();
        
        AddRestEaseClients(services, configuration);
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IAddressRepository, AddressRepository>()
            .AddScoped<ILoginRepository, LoginRepository>();

        // Services
        services.AddScoped<ILoginServices, LoginService>()
            .AddScoped<IAddressServices, AddressService>()
            .AddScoped<IUserServices, UserService>();
    }

    public static void RegisterAuthenticationScheme(this IServiceCollection services)
    {
        services.AddAuthentication("Basic")
            .AddScheme<CustomAuthSchemeOptions, CustomAuthenticationHandler>("Basic", null);
    }


    private static void AddRestEaseClients(this IServiceCollection services, IConfiguration configuration)
    {
        var authUri = new Uri(configuration.GetSection("AppSettings:RestEaseUris:Authentication").Value);

        var authClient = RestClient.For<IAuthenticationRestEaseService>(authUri.AbsoluteUri);

        services.AddSingleton(authClient);
    }
}