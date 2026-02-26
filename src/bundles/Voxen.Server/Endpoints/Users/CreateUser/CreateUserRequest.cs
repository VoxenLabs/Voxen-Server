using Voxen.Server.Entities;

namespace Voxen.Server.Endpoints.Users.CreateUser;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Gets or sets the username of the user to be created.
    /// </summary>
    public string Username { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the password for the new user being created.
    /// </summary>
    public string Password { get; set; } = null!;
}
