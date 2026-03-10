namespace Voxen.Server.Endpoints.Server.GetServerInfo;

public sealed class GetServerInfoResponse
{
    public required string Name { get; init; }
    public required bool HasLogo { get; init; }
    public string? LogoUrl { get; init; }
}

