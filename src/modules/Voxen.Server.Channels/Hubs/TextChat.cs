using Microsoft.AspNetCore.SignalR;

namespace Voxen.Server.Channels.Hubs;

public class TextChat : Hub
{
    /// <summary>
    /// Join a specific channel group.
    /// </summary>
    public async Task JoinChannel(Guid channelId)
    {
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
