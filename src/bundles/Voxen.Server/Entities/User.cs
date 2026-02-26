using Microsoft.AspNetCore.Identity;

namespace Voxen.Server.Entities;

/// <summary>
/// Represents a user entity within the system.
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier of the server associated with the user.
    /// </summary>
    public Guid ServerId { get; set; }
    
    /// <summary>
    /// Gets or sets the server associated with the user.
    /// </summary>
    public Server Server { get; set; } = null!;

    /// <summary>
    /// Gets or sets the role of the user within the associated server.
    /// </summary>
    public ServerRole Role { get; set; }

    /// <summary>
    /// Gets or sets the collection of messages associated with the user.
    /// </summary>
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}

/// <summary>
/// Defines the roles that a user can have within a server.
/// </summary>
public enum ServerRole
{
    /// <summary>
    /// Represents a standard member role within a server.
    /// </summary>
    Member = 1,
    
    /// <summary>
    /// Represents an administrator role within a server.
    /// </summary>
    Admin = 2
}
