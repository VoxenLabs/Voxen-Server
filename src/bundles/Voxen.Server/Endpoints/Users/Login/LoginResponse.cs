namespace Voxen.Server.Endpoints.Users.Login;

/// <summary>
/// Represents the response returned upon successful user login.
/// </summary>
/// <remarks>
/// This class contains the access token generated for the authenticated user.
/// The access token is used to authorize subsequent requests to the server.
/// </remarks>
public class LoginResponse
{
    /// <summary>
    /// Gets or sets the access token generated for the authenticated user.
    /// </summary>
    /// <remarks>
    /// The access token is a JSON Web Token (JWT) used to authorize subsequent 
    /// requests to the server. It is issued upon successful user authentication.
    /// </remarks>
    public string AccessToken { get; set; } = null!;
}
