using Microsoft.Extensions.DependencyInjection;
using Voxen.Server.Info.Interfaces;
using Voxen.Server.Info.Services;

namespace Voxen.Server.Info.Extensions;

/// <summary>
/// Provides extension methods for registering Voxen server info services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the Voxen server information services with the dependency injection container.
    /// </summary>
    public static IServiceCollection AddVoxenServerInfo(this IServiceCollection services)
    {
        services.AddScoped<IServerConfigurationProvider, ServerConfigurationProvider>();

        return services;
    }
}
