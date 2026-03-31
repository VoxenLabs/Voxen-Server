namespace Voxen.Server.Channels.Endpoints.SendMessage;

/// <summary>
/// Represents the response returned after successfully sending a message to a channel.
/// </summary>
public class SendMessageResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the message.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the channel where the message was sent.
    /// </summary>
    public Guid ChannelId { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the user who sent the message.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the content of the message.
    /// </summary>
    public required string Content { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp indicating when the message was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

