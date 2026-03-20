using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Voxen.Server.Audits.Models;
using Voxen.Server.Domain;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Audits.Endpoints.GetAuditLogs;

public sealed class GetAuditLogsEndpoint(VoxenDbContext db) : EndpointWithoutRequest<List<GetAuditLogsResponse>>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Get("/audits");
        Roles(nameof(ServerRole.Admin));
    }

    /// <inheritdoc />
    public override async Task HandleAsync(CancellationToken ct)
    {
        var logs = await db.AuditLogs
            .AsNoTracking()
            .Include(a => a.User)
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => new GetAuditLogsResponse
            {
                Id = a.Id,
                UserId = a.UserId,
                UserName = a.User.UserName,
                Action = a.Action,
                Category = a.Category,
                EntityId = a.EntityId,
                Changes = string.IsNullOrWhiteSpace(a.ChangesJson)
                    ? null
                    : JsonSerializer.Deserialize<List<AuditChange>>(a.ChangesJson),
                CreatedAt = a.CreatedAt
            })
            .ToListAsync(ct);

        await Send.OkAsync(logs, ct);
    }
}
