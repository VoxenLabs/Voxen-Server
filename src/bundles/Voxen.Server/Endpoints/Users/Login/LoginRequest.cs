namespace Voxen.Server.Endpoints.Users.Login;

/// <summary>
/// Represents a request for user login.
/// </summary>
/// <remarks>
/// This class contains the necessary information for authenticating a user, 
/// including the username and password.
/// </remarks>
public class LoginRequest
{
    /// <summary>
    /// Gets or sets the username of the user attempting to log in.
    /// </summary>
    /// <remarks>
    /// This property represents the unique identifier for the user within the system.
    /// It is required for authentication purposes.
    /// </remarks>
    public string Username { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the password for the login request.
    /// </summary>
    /// <remarks>
    /// This property contains the user's password, which is used to authenticate the user.
    /// Ensure that this value is securely handled and not exposed in logs or error messages.
    /// </remarks>
    public string Password { get; set; } = null!;
}
