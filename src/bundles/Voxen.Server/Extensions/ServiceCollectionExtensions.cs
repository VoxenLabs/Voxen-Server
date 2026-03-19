using System.Text.Json.Serialization;
using FastEndpoints;
using FastEndpoints.Swagger;

namespace Voxen.Server.Extensions;

/// <summary>
/// Provides extension methods for configuring services in the application.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds and configures API services for the Voxen Server application.
    /// </summary>
    public static IServiceCollection AddVoxenApiServices(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(o =>
        {
            o.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        
        services.AddFastEndpoints();
        services.SwaggerDocument(o =>
        {
            o.ShortSchemaNames = true;
            o.DocumentSettings = s =>
            {
                s.Title = "Voxen Server API";
                s.Version = "v1";
            };
        });

        return services;
    }
}
