using FastEndpoints;
using Voxen.Server.Interfaces;

namespace Voxen.Server.Endpoints.Server.GetServerLogo;

public sealed class GetServerLogoEndpoint(IServerConfigurationProvider serverConfigurationProvider) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/server/logo");
        AllowAnonymous();
    }

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
