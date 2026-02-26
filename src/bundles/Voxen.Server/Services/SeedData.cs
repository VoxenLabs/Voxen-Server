using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Voxen.Server.Entities;

namespace Voxen.Server.Services;

/// <summary>
/// Provides methods for seeding initial data into the database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Initializes the database with default data asynchronously.
    /// </summary>
    /// <param name="context">The database context used to interact with the database.</param>
    /// <param name="userManager">The user manager used to create and manage user accounts.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task InitializeDatabaseAsync(VoxenDbContext context, UserManager<User> userManager)
    {
        await context.Database.EnsureCreatedAsync();
        if (await context.Server.AnyAsync())
            return;

        var defaultServer = new Entities.Server
        {
            Id = Guid.NewGuid(),
            Name = "Voxen Server"
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
            throw new InvalidOperationException(
                $"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        await context.SaveChangesAsync();
    }
}
