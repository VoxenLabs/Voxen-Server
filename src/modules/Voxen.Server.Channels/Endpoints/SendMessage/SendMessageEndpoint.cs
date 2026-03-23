using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Voxen.Server.Channels.Hubs;
using Voxen.Server.Domain;
using Voxen.Server.Domain.Constants;
using Voxen.Server.Domain.Entities;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Channels.Endpoints.SendMessage;

/// <summary>
/// Represents an endpoint for sending messages to a specific text channel.
/// </summary>
public sealed class SendMessageEndpoint(
    VoxenDbContext db,
    IHubContext<TextChat> hubContext)
    : Endpoint<SendMessageRequest>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/channels/{ChannelId}/messages");
        Roles(RoleGroups.Everyone);

        Summary(s =>
        {
            s.Summary = "Send a message to a channel";
            s.Description = "Creates a new message in the specified channel and broadcasts it to all connected clients via SignalR. Only text channels support messages.";

            s.RequestParam(r => r.Content, "The content of the message to send.");

            s.Response<SendMessageResponse>(200, "Message sent successfully.");
            s.Response(400, "The channel is not a text channel.");
            s.Response(404, "The specified channel could not be found.");
        });
    }

    /// <inheritdoc />
    public override async Task HandleAsync(SendMessageRequest request, CancellationToken ct)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var channel = await db.Channels.FirstOrDefaultAsync(c => c.Id == request.ChannelId, ct);
        if (channel == null)
        {
            AddError($"The provided channel could not be found.");
            await Send.ErrorsAsync(StatusCodes.Status404NotFound, ct);
            return;
        }

        if (channel.Type != ChannelType.Text)
        {
            AddError($"The selected channel is a {channel.Type.ToString()} channel, which does not support text messages.");
            await Send.ErrorsAsync(StatusCodes.Status400BadRequest, ct);
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

        var response = new SendMessageResponse
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
