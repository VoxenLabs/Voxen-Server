namespace Voxen.Server.Endpoints.Users.Login;

/// <summary>
/// Represents a request for user login.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Gets or sets the username of the user attempting to log in.
    /// </summary>
    public string Username { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the password for the login request.
    /// </summary>
    public string Password { get; set; } = null!;
}
