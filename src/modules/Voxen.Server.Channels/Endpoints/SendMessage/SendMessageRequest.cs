namespace Voxen.Server.Channels.Endpoints.SendMessage;

/// <summary>
/// Represents a request to send a message to a specific channel.
/// </summary>
public class SendMessageRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the channel where the message will be sent.
    /// </summary>
    public Guid ChannelId { get; set; }
    
    /// <summary>
    /// Gets or sets the content of the message to be sent.
    /// </summary>
    public required string Content { get; set; }
}

