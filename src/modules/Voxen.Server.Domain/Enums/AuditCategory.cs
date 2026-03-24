namespace Voxen.Server.Domain.Enums;

/// <summary>
/// Represents the category of an action logged in the audit system.
/// </summary>
public enum AuditCategory
{
    /// <summary>
    /// Represents a change to server-related properties logged in the audit system.
    /// </summary>
    Server,

    /// <summary>
    /// Represents a change to user-related properties logged in the audit system.
    /// </summary>
    User
}
