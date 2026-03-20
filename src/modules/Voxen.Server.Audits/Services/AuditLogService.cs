using System.Text.Json;
using Voxen.Server.Audits.Interfaces;
using Voxen.Server.Audits.Models;
using Voxen.Server.Domain;
using Voxen.Server.Domain.Entities;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Audits.Services;

/// <summary>
/// Provides functionality for logging audit entries to the database.
/// </summary>
public class AuditLogService(VoxenDbContext db) : IAuditLogService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    /// <inheritdoc />
    public async Task LogAsync(User actor, AuditAction action, AuditCategory category, Guid entityId, IEnumerable<AuditChange> changes, CancellationToken ct = default)
    {
        var changeList = changes.ToList();
        var audit = new Audit
        {
            UserId = actor.Id,
            Action = action,
            Category = category,
            EntityId = entityId,
            ChangesJson = changeList is { Count: > 0 }
                ? JsonSerializer.Serialize(changeList, JsonOptions)
                : null,
            CreatedAt = DateTime.UtcNow
        };

        db.AuditLogs.Add(audit);
        await db.SaveChangesAsync(ct);
    }
}
