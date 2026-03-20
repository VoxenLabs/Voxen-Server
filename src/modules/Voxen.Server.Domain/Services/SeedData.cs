using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Voxen.Server.Domain.Entities;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Domain.Services;

/// <summary>
/// Provides methods for seeding initial data into the database.
/// </summary>
public static class SeedData
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    /// <summary>
    /// Initializes the database with default data asynchronously.
    /// </summary>
    /// <param name="db">The database context used to interact with the database.</param>
    /// <param name="userManager">The user manager used to create and manage user accounts.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task InitializeDatabaseAsync(VoxenDbContext db, UserManager<User> userManager)
    {
        await db.Database.MigrateAsync();
        if (await db.Server.AnyAsync())
            return;
        
        var adminId = await userManager.CreateAdmin(db);
        db.CreateServer(adminId);

        await db.SaveChangesAsync();
    }

    private static void CreateServer(this VoxenDbContext db, Guid adminId)
    {
        var server = new Entities.Server
        {
            Id = Guid.NewGuid(),
            Name = "Voxen Server",
            CreatedAt = DateTime.UtcNow,
            Logo = null,
            LogoContentType = null
        };

        db.Server.Add(server);
        db.AuditLogs.Add(new Audit
        {
            UserId = adminId,
            Action = AuditAction.Create,
            Category = AuditCategory.Server,
            EntityId = server.Id,
            ChangesJson = """
                          [
                              {
                                    "PropertyName": "Name",
                                    "OldValue": null,
                                    "NewValue": "Voxen Server"
                              }
                          ]
                          """,
            CreatedAt = DateTime.UtcNow
        });
    }

    private static async Task<Guid> CreateAdmin(this UserManager<User> userManager, VoxenDbContext db)
    {
        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = "admin",
            Role = ServerRole.Admin
        };

        var result = await userManager.CreateAsync(adminUser, "Password123!");

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                $"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        db.AuditLogs.Add(new Audit
        {
            UserId = adminUser.Id,
            Action = AuditAction.Create,
            Category = AuditCategory.User,
            EntityId = adminUser.Id,
            ChangesJson = """
                          [
                              {
                                    "PropertyName": "Name",
                                    "OldValue": null,
                                    "NewValue": "admin"
                              },
                              {
                                    "PropertyName": "Role",
                                    "OldValue": null,
                                    "NewValue": "Admin"
                              }
                          ]
                          """,
            CreatedAt = DateTime.UtcNow
        });

        return adminUser.Id;
    }
}
