using Voxen.Server.Entities;

namespace Voxen.Server.Endpoints.Users.CreateUser;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
/// <remarks>
/// This class is used to encapsulate the data required for creating a new user, 
/// including the username and password. It is typically used as part of the 
/// <see cref="CreateUserEndpoint"/> to handle user registration.
/// </remarks>
public class CreateUserRequest
{
    /// <summary>
    /// Gets or sets the username of the user to be created.
    /// </summary>
    /// <remarks>
    /// This property represents the unique identifier for the user within the system.
    /// It is required for creating a new user and is used as the <see cref="User.UserName"/> 
    /// when registering the user.
    /// </remarks>
    public string Username { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the password for the new user being created.
    /// </summary>
    /// <remarks>
    /// This property is required to specify the password for the user during the registration process.
    /// Ensure that the password meets the security requirements defined by the system.
    /// </remarks>
    public string Password { get; set; } = null!;
}
