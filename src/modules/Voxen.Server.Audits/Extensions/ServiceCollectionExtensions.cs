using Microsoft.Extensions.DependencyInjection;

namespace Voxen.Server.Audits.Extensions;

/// <summary>
/// Provides extension methods for configuring Voxen Audits services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Voxen Audits services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    public static IServiceCollection AddVoxenAudits(this IServiceCollection services) => services;
}
