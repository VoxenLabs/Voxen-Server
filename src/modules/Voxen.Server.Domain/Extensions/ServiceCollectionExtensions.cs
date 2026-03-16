using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Voxen.Server.Domain.Extensions;

public static class ServiceCollectionExtensions
{
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
