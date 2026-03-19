namespace Voxen.Server.Domain.Enums;

/// <summary>
/// Defines the roles that a user can have within a server.
/// </summary>
public enum ServerRole
{
    /// <summary>
    /// Represents a standard member role within a server.
    /// </summary>
    Member = 0,
    
    /// <summary>
    /// Represents an administrator role within a server.
    /// </summary>
    Admin = 1
}
