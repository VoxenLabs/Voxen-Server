namespace Voxen.Server.Domain.Entities;


/// <summary>
/// Represents a message sent by a user within a channel.
/// </summary>
public class Message
{
    /// <summary>
    /// Gets or sets the unique identifier for the message.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the channel in which the message was sent.
    /// </summary>
    public Guid ChannelId { get; set; }

    /// <summary>
    /// Gets or sets the channel in which the message was sent.
    /// </summary>
    public Channel Channel { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unique identifier of the user who sent the message.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the user who sent the message.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the textual content of the message.
    /// </summary>
    public required string Content { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the message was created (stored in UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
