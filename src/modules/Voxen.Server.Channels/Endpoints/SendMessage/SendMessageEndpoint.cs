using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Voxen.Server.Channels.Hubs;
using Voxen.Server.Domain;
using Voxen.Server.Domain.Constants;
using Voxen.Server.Domain.Entities;

namespace Voxen.Server.Channels.Endpoints.SendMessage;

public sealed class SendMessageToChannelEndpoint(
    VoxenDbContext db,
    IHubContext<TextChat> hubContext)
    : Endpoint<SendMessageToChannelRequest>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/messages");
        Roles(RoleGroups.Everyone);
    }

    /// <inheritdoc />
    public override async Task HandleAsync(SendMessageToChannelRequest request, CancellationToken ct)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        var channel = await db.Channels.FirstOrDefaultAsync(c => c.Id == request.ChannelId, ct);
        if (channel == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var message = new Message
        {
            ChannelId = channel.Id,
            UserId = userId,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow
        };

        db.Messages.Add(message);
        await db.SaveChangesAsync(ct);

        var response = new SendMessageToChannelResponse
        {
            Id = message.Id,
            ChannelId = message.ChannelId,
            UserId = message.UserId,
            Content = message.Content,
            CreatedAt = message.CreatedAt
        };

        await hubContext.Clients
            .Group(channel.Id.ToString())
            .SendAsync("ReceiveMessage", response, ct);

        await Send.OkAsync(response, ct);
    }
}
