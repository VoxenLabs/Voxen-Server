using Voxen.Server.Enums;

namespace Voxen.Server.Entities;

/// <summary>
/// Represents a communication channel within a server.
/// </summary>
public class Channel
{
    /// <summary>
    /// Gets or sets the unique identifier for the channel.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the server associated with this channel.
    /// </summary>
    public Guid ServerId { get; set; }
    
    /// <summary>
    /// Gets or sets the server associated with this channel.
    /// </summary>
    public Server Server { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the channel.
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the type of the channel.
    /// </summary>
    public ChannelType Type { get; set; }

    /// <summary>
    /// Gets or sets the collection of messages associated with this channel.
    /// </summary>
    public ICollection<Message>? Messages { get; set; }
}
