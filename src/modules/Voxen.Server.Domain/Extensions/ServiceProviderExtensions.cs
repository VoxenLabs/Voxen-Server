using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Voxen.Server.Domain.Entities;
using Voxen.Server.Domain.Services;

namespace Voxen.Server.Domain.Extensions;

public static class ServiceProviderExtensions
{
    public static async Task<IServiceProvider> UseVoxenDb(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<VoxenDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        await SeedData.InitializeDatabaseAsync(db, userManager);

        return services;
    }
}
