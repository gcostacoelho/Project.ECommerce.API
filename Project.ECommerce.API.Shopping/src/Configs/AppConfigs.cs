using Microsoft.EntityFrameworkCore;
using Project.ECommerce.API.Shopping.src.Database;

namespace Project.ECommerce.API.Shopping.src.Configs;
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

    public static void RegisterServices(this IServiceCollection services) { }
}