using Voxen.Server.Audits.Models;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Audits.Endpoints.GetAuditLogs;

/// <summary>
/// Represents the response structure for audit log data.
/// </summary>
public class GetAuditLogsResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the audit log.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the identifier of the user associated with the audit log.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the user associated with the audit log.
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// Gets or sets the type of action performed in the audit log.
    /// </summary>
    public AuditAction Action { get; set; }
    
    /// <summary>
    /// Gets or sets the category of the action logged in the audit log.
    /// </summary>
    public AuditCategory Category { get; set; }
    
    /// <summary>
    /// Gets or sets the identifier of the entity affected by the audit log.
    /// </summary>
    public Guid? EntityId { get; set; }
    
    /// <summary>
    /// Gets or sets the deserialized list of changes (old/new values) for this audit log.
    /// </summary>
    public List<AuditChange>? Changes { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the audit log was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
