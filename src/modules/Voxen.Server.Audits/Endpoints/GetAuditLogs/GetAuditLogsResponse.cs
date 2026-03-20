using Voxen.Server.Audits.Models;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Audits.Endpoints.GetAuditLogs;

public class GetAuditLogsResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? UserName { get; set; }

    public AuditAction Action { get; set; }
    public AuditCategory Category { get; set; }

    public Guid? EntityId { get; set; }
    public string? EntityName { get; set; }

    /// <summary>
    /// Deserialized list of changes (old/new values) for this audit log
    /// </summary>
    public List<AuditChange>? Changes { get; set; }

    public DateTime CreatedAt { get; set; }
}
