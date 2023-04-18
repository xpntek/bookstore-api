using API.Extensions;
using API.Serialization;
using API.Serialization.Results;
using Application.Helpers.MappingProfiles;
using NLog.Web;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddWebAPI(this WebApplicationBuilder builder)
    {
        builder.Services.AddSerializationResult()
            .AddIdentityServices(builder.Configuration)
            .AddApplicationServices()
            .AddAutoMapper(typeof(MappingProfiles));
        builder.AddLogs();
        return builder.Services;
    }

    private static IServiceCollection AddSerializationResult(this IServiceCollection services)
    {
        services
            .AddTransient<IResultSerializationStrategy, SerializationResultSuccess>()
            .AddTransient<IResultSerializationStrategy, SerializationResultInternalError>()
            .AddTransient<IResultSerializationStrategy, SerializationResultValidationError>()
            .AddTransient<IResultSerializationStrategy, SerializationResultApplicationError>()
            .AddTransient<IResultSerializationStrategy, SerializationResultNotFoundError>()
            .AddTransient<IResultSerializationStrategy, SerializationResultConflictError>();
        
        return services;
    }

    private static void AddLogs(this WebApplicationBuilder builder)
    {
        builder.Logging
            .ClearProviders()
            .SetMinimumLevel(LogLevel.Trace)
            .AddConsole();
        builder.Host.UseNLog();
    }
}