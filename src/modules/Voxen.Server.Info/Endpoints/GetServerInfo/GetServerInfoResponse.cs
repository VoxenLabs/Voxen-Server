namespace Voxen.Server.Info.Endpoints.GetServerInfo;

/// <summary>
/// Response model returned by <see cref="GetServerInfoEndpoint"/>.
/// Contains basic information about the server.
/// </summary>
public sealed class GetServerInfoResponse
{
    /// <summary>
    /// The name of the server.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Indicates whether the server has a logo configured.
    /// </summary>
    public required bool HasLogo { get; init; }

    /// <summary>
    /// The creation date and time of the server.
    /// </summary>
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// The URL to access the server's logo if one exists; otherwise <c>null</c>.
    /// </summary>
    public string? LogoUrl { get; init; }
}

