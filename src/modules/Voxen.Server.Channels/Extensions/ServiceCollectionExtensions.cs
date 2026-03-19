using Microsoft.Extensions.DependencyInjection;

namespace Voxen.Server.Channels.Extensions;

/// <summary>
/// Provides extension methods for registering Voxen Channels services 
/// with an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Voxen Channels services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    public static IServiceCollection AddVoxenChannels(this IServiceCollection services) => services;
}
