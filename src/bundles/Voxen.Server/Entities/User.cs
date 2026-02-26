using Microsoft.AspNetCore.Identity;

namespace Voxen.Server.Entities;

/// <summary>
/// Represents a user entity within the system.
/// </summary>
/// <remarks>
/// This class extends the <see cref="Microsoft.AspNetCore.Identity.IdentityUser{TKey}"/> class with a primary key of type <see cref="Guid"/>.
/// It includes additional properties such as <see cref="ServerId"/>, <see cref="Server"/>, <see cref="Role"/>, and <see cref="Messages"/>.
/// The user is associated with a specific server and can have a role within that server.
/// </remarks>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier of the server associated with the user.
    /// </summary>
    public Guid ServerId { get; set; }
    
    /// <summary>
    /// Gets or sets the server associated with the user.
    /// </summary>
    /// <remarks>
    /// This property establishes a navigation relationship between the <see cref="User"/> and the <see cref="Server"/> entities.
    /// It represents the server to which the user belongs.
    /// </remarks>
    public Server Server { get; set; } = null!;

    /// <summary>
    /// Gets or sets the role of the user within the associated server.
    /// </summary>
    /// <remarks>
    /// The role determines the user's level of access and permissions within the server.
    /// Possible values are defined in the <see cref="ServerRole"/> enumeration.
    /// </remarks>
    public ServerRole Role { get; set; }

    /// <summary>
    /// Gets or sets the collection of messages associated with the user.
    /// </summary>
    /// <remarks>
    /// This property represents the messages sent by the user within the system.
    /// Each message is linked to a specific channel and contains content, a timestamp, and other metadata.
    /// </remarks>
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}

/// <summary>
/// Defines the roles that a user can have within a server.
/// </summary>
/// <remarks>
/// The <see cref="ServerRole"/> enumeration is used to specify the level of access and permissions
/// a user has within a server. It is primarily used in conjunction with the <see cref="User.Role"/> property.
/// </remarks>
public enum ServerRole
{
    /// <summary>
    /// Represents a standard member role within a server.
    /// </summary>
    /// <remarks>
    /// Members have basic access and permissions within the server. 
    /// This role is typically assigned to regular users who do not have administrative privileges.
    /// </remarks>
    Member = 1,
    
    /// <summary>
    /// Represents an administrator role within a server.
    /// </summary>
    /// <remarks>
    /// Users with the <see cref="ServerRole.Admin"/> role have elevated permissions and access
    /// to manage the server, including its users, settings, and resources.
    /// </remarks>
    Admin = 2
}
