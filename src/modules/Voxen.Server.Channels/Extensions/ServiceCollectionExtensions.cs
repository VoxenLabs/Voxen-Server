using Microsoft.Extensions.DependencyInjection;

namespace Voxen.Server.Channels.Extensions;

/// <summary>
/// Provides extension methods for configuring Voxen Channels services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds and configures Voxen Channels services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    public static IServiceCollection AddVoxenChannels(this IServiceCollection services) => services;
}
