namespace Voxen.Server.Channels.Endpoints.SendMessage;

public class SendMessageToChannelResponse
{
    public Guid Id { get; set; }
    public Guid ChannelId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
