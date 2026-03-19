using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Voxen.Server.Domain.Extensions;

/// <summary>
/// Provides extension methods for registering Voxen database services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the <see cref="VoxenDbContext"/> with the dependency injection container
    /// and configures it to use SQLite with the database path from the <c>VOXEN_DB_PATH</c> environment variable.
    /// </summary>
    public static IServiceCollection AddVoxenDb(this IServiceCollection services)
    {
        var dbPath = Environment.GetEnvironmentVariable("VOXEN_DB_PATH") ?? "voxen.db";
        var migrationsAssembly = typeof(VoxenDbContext).GetTypeInfo().Assembly.GetName().Name;
        services.AddDbContext<VoxenDbContext>(options =>
        {
            options.UseSqlite($"Data Source={dbPath}", builder => builder.MigrationsAssembly(migrationsAssembly));
        });

        return services;
    }
}
