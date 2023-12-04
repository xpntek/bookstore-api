using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(
            options => options.UseMySql(configuration.GetConnectionString("mysql"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("mysql")))
            );
        
        return services;
    }
}