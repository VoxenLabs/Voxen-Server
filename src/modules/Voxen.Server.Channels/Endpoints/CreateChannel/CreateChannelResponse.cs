using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Channels.Endpoints.CreateChannel;

/// <summary>
/// Represents the response returned after successfully creating a channel.
/// </summary>
public class CreateChannelResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the created channel.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the created channel.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the channel was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the type of the created channel.
    /// </summary>
    public ChannelType Type { get; set; }
}
