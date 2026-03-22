using FastEndpoints;
using Microsoft.AspNetCore.SignalR;
using Voxen.Server.Channels.Hubs;
using Voxen.Server.Domain;
using Voxen.Server.Domain.Constants;

namespace Voxen.Server.Channels.Endpoints.SendMessageToChannelEndpoint;

public sealed class SendMessageToChannelEndpoint(
    VoxenDbContext db,
    IHubContext<TextChat> hubContext)
    : Endpoint<SendMessageToChannelRequest>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/channels/{ChannelId:guid}/messages");
        Roles(RoleGroups.Everyone);
    }

    /// <inheritdoc />
    public override async Task HandleAsync(SendMessageToChannelRequest request, CancellationToken ct)
    {
        var channelId = Route<Guid>("ChannelId");
        var userId = Guid.Parse(User.FindFirst("sub")!.Value);

        //var message = new Message
        //{
        //    Id = Guid.NewGuid(),
        //    ChannelId = request.ChannelId,
        //    UserId = userId,
        //    Content = request.Content,
        //    CreatedAt = DateTime.UtcNow
        //};

        //db.Messages.Add(message);
        //await db.SaveChangesAsync(ct);

        //var response = new
        //{
        //    message.Id,
        //    message.ChannelId,
        //    message.UserId,
        //    message.Content,
        //    message.CreatedAt
        //};

        //// 🔥 Broadcast to all clients in this channel
        //await hubContext.Clients
        //    .Group(request.ChannelId.ToString())
        //    .SendAsync("ReceiveMessage", response, ct);

        await Send.OkAsync(null, ct);
    }
}
