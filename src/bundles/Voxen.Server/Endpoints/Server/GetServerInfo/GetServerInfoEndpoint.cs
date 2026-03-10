using FastEndpoints;
using Voxen.Server.Interfaces;

namespace Voxen.Server.Endpoints.Server.GetServerInfo;

public sealed class GetServerInfoEndpoint(IServerConfigurationProvider serverConfigurationProvider) : EndpointWithoutRequest<GetServerInfoResponse>
{
    public override void Configure()
    {
        Get("/server/info");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var server = await serverConfigurationProvider.GetAsync(ct);
        var hasLogo = server.Logo is { Length: > 0 } && !string.IsNullOrWhiteSpace(server.LogoContentType);

        await Send.OkAsync(new GetServerInfoResponse
        {
            Name = server.Name,
            HasLogo = hasLogo,
            CreatedAt = server.CreatedAt,
            LogoUrl = hasLogo ? "/server/logo" : null
        }, ct);
    }
}
