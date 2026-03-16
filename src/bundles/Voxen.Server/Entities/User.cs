using Microsoft.AspNetCore.Identity;
using Voxen.Server.Enums;

namespace Voxen.Server.Entities;

/// <summary>
/// Represents a user within the Voxen server.
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public ServerRole Role { get; set; }

    /// <summary>
    /// Messages sent by the user.
    /// </summary>
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
