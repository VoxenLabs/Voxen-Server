using Microsoft.AspNetCore.Identity;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Domain.Entities;

/// <summary>
/// Represents a user within the Voxen server.
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public ServerRole Role { get; set; }
}
