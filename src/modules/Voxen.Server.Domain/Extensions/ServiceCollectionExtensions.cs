using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Voxen.Server.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddVoxenDb(this IServiceCollection services)
    {
        var dbPath = Environment.GetEnvironmentVariable("VOXEN_DB_PATH") ?? "voxen.db";
        services.AddDbContext<VoxenDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

        return services;
    }
}
