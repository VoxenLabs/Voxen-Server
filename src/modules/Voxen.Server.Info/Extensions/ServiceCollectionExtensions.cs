using Microsoft.Extensions.DependencyInjection;
using Voxen.Server.Info.Interfaces;
using Voxen.Server.Info.Services;

namespace Voxen.Server.Info.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddVoxenServerInfo(this IServiceCollection services)
    {
        services.AddScoped<IServerConfigurationProvider, ServerConfigurationProvider>();

        return services;
    }
}
