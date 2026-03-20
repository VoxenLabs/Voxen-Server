using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Domain.Entities;

/// <summary>
/// Represents an audit log entry within the Voxen server.
/// </summary>
public class Audit
{
    /// <summary>
    /// Unique identifier for the audit log entry.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user who performed the action.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Navigation property for the user.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// The action that was performed (Create, Update, Delete, etc.).
    /// </summary>
    public AuditAction Action { get; set; }

    /// <summary>
    /// The category/type of entity affected (Channel, User, Server, etc.).
    /// </summary>
    public AuditCategory Category { get; set; }

    /// <summary>
    /// The identifier of the affected entity (e.g., ChannelId, UserId).
    /// </summary>
    public Guid? EntityId { get; set; }

    /// <summary>
    /// Optional human-readable identifier
    /// </summary>
    public string? EntityName { get; set; }

    /// <summary>
    /// Backing field for JSON storage in DB.
    /// </summary>
    public string? ChangesJson { get; set; }

    /// <summary>
    /// Timestamp when the action occurred (UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
