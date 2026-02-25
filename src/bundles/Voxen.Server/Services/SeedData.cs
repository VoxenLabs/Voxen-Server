using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Voxen.Server.Entities;

namespace Voxen.Server.Services;

public static class SeedData
{
    public static async Task InitializeDatabaseAsync(VoxenDbContext context, UserManager<User> userManager)
    {
        await context.Database.EnsureCreatedAsync();
        if (await context.Server.AnyAsync())
            return;

        var defaultServer = new Entities.Server
        {
            Id = Guid.NewGuid(),
            Name = "Default Server"
        };

        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = "admin",
            ServerId = defaultServer.Id,
            Role = ServerRole.Admin
        };

        context.Server.Add(defaultServer);
        var result = await userManager.CreateAsync(adminUser, "Password123!");

        if (!result.Succeeded)
        {
            throw new Exception(
                $"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        await context.SaveChangesAsync();
    }
}
