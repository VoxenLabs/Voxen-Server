using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Voxen.Server.Domain;
using Voxen.Server.Domain.Constants;

namespace Voxen.Server.Channels.Endpoints.GetChannels;

/// <summary>
/// Retrieve all channels on the server
/// </summary>
public sealed class GetServerInfoEndpoint(VoxenDbContext db) : EndpointWithoutRequest<List<GetChannelsResponse>>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Get("/channels");
        Roles(RoleGroups.Everyone);
    }

    /// <inheritdoc />
    public override async Task HandleAsync(CancellationToken ct)
    {
        var channels = await db.Channels
            .AsNoTracking()
            .Select(c => new GetChannelsResponse
            {
                Id = c.Id,
                Name = c.Name,
                CreatedAt = c.CreatedAt,
                Type = c.Type
            })
            .ToListAsync(ct);

        await Send.OkAsync(channels, ct);
    }
}
