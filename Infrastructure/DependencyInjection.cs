using Application.Interfaces;
using Infrastructure.Services;
using Infrastruture.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<IListOfAuthorsService, ListOfAuthorsService>();
        return services;
    }
}