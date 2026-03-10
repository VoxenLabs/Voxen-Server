using FastEndpoints;
using Voxen.Server.Interfaces;

namespace Voxen.Server.Endpoints.Server.GetServerLogo;

/// <summary>
/// Endpoint to retrieve the server's logo image.
/// </summary>
public sealed class GetServerLogoEndpoint(IServerConfigurationProvider serverConfigurationProvider) : EndpointWithoutRequest
{
    /// <inheritdoc />
    public override void Configure()
    {
        Get("/server/logo");
        AllowAnonymous();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(CancellationToken ct)
    {
        var server = await serverConfigurationProvider.GetAsync(ct);

        if (server.Logo is not { Length: > 0 } || string.IsNullOrWhiteSpace(server.LogoContentType))
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        HttpContext.Response.ContentType = server.LogoContentType;
        HttpContext.Response.ContentLength = server.Logo.Length;

        await HttpContext.Response.Body.WriteAsync(server.Logo, ct);
    }
}
