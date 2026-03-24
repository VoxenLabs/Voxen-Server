using FastEndpoints;
using Voxen.Server.Domain;
using Voxen.Server.Domain.Entities;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Channels.Endpoints.CreateChannel;

/// <summary>
/// Represents the endpoint responsible for creating a new channel within a server.
/// </summary>
public class CreateChannelEndpoint(VoxenDbContext db) : Endpoint<CreateChannelRequest>
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
