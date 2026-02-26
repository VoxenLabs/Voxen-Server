namespace Voxen.Server.Endpoints.Users.Login;

/// <summary>
/// Represents the response returned upon successful user login.
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// Gets or sets the access token generated for the authenticated user.
    /// </summary>
    public string AccessToken { get; set; } = null!;
}
