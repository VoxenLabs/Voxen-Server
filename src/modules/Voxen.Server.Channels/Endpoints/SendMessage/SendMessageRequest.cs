namespace Voxen.Server.Channels.Endpoints.SendMessage;

public class SendMessageToChannelRequest
{
    public required Guid ChannelId { get; set; }
    public required string Content { get; set; }
}
