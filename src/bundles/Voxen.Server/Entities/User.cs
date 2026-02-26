using Microsoft.AspNetCore.Identity;
using Voxen.Server.Enums;

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
