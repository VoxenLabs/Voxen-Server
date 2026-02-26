using Microsoft.Extensions.Configuration;

namespace Voxen.Server.Authentication.Interfaces;

/// <summary>
/// Provides functionality for generating JSON Web Tokens (JWT) for user authentication and authorization.
/// </summary>
/// <remarks>
/// This interface defines methods for creating JWT tokens that include user-specific claims such as ID, username, and role.
/// The tokens are signed using a security key and are used to authenticate and authorize users within the system.
/// </remarks>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates a JSON Web Token (JWT) for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="userName">The username of the user.</param>
    /// <param name="userRole">The role of the user within the system.</param>
    /// <returns>A string representing the generated JWT token.</returns>
    /// <remarks>
    /// The generated token includes claims for the user's ID, username, role, and other standard JWT claims.
    /// It is signed using a symmetric security key and has a configurable expiration time.
    /// </remarks>
    public string CreateToken(Guid userId, string userName, string userRole);
}
