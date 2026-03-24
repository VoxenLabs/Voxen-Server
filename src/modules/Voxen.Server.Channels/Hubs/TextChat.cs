using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Voxen.Server.Domain;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Channels.Hubs;

/// <summary>
/// Represents a SignalR hub for managing text-based chat channels.
/// </summary>
[Authorize]
public class TextChat(VoxenDbContext db) : Hub
{
    /// <summary>
    /// Join a specific channel group.
    /// </summary>
    public async Task JoinChannel(Guid channelId)
    {
        var channel = await db.Channels
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == channelId);

        if (channel == null)
            throw new HubException("CHANNEL_NOT_FOUND");

        if (channel.Type != ChannelType.Text)
            throw new HubException("INVALID_CHANNEL_TYPE");

        await Groups.AddToGroupAsync(Context.ConnectionId, channelId.ToString());
    }

    /// <summary>
    /// Leave a specific channel group.
    /// </summary>
    public async Task LeaveChannel(Guid channelId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelId.ToString());
    }
}
