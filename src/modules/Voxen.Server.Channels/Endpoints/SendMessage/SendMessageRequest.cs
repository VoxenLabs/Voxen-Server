namespace Voxen.Server.Channels.Endpoints.SendMessage;

public class SendMessageRequest
{
    public Guid ChannelId { get; set; }
    public required string Content { get; set; }
}
