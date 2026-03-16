using FastEndpoints;
using Voxen.Server.Entities;
using Voxen.Server.Enums;
using Voxen.Server.Interfaces;

namespace Voxen.Server.Endpoints.Channels.CreateChannel;

/// <summary>
/// Represents the endpoint responsible for creating a new channel within a server.
/// </summary>
public class CreateChannelEndpoint(IServerConfigurationProvider serverConfigurationProvider, VoxenDbContext db) : Endpoint<CreateChannelRequest>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/channels");
        Roles(nameof(ServerRole.Admin));
    }

    /// <inheritdoc />
    public override async Task HandleAsync(CreateChannelRequest request, CancellationToken ct)
    {
        if (!Enum.IsDefined(request.Type))
        {
            AddError(r => r.Type, "Invalid channel type.");
            await Send.ErrorsAsync(400, ct);
            return;
        }

        var channel = new Channel
        {
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            Type = request.Type
        };

        db.Channels.Add(channel);
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(channel, ct);
    }
}
