using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Voxen.Server.Domain;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Channels.Endpoints.DeleteChannel;

/// <summary>
/// Represents the endpoint responsible for handling the deletion of a communication channel.
/// </summary>
public sealed class DeleteChannelEndpoint(VoxenDbContext db) : Endpoint<DeleteChannelRequest>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Delete("/channels/{id}");
        Roles(nameof(ServerRole.Admin));
    }

    /// <inheritdoc />
    public override async Task HandleAsync(DeleteChannelRequest request, CancellationToken ct)
    {
        var channel = await db.Channels.FirstOrDefaultAsync(c => c.Id == request.Id, ct);

        if (channel is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        db.Channels.Remove(channel);
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(new { Message = "Channel deleted successfully" }, ct);
    }
}
