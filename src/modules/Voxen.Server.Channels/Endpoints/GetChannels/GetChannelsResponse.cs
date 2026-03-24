using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Channels.Endpoints.GetChannels;

/// <summary>
/// Represents the response containing details about a channel.
/// </summary>
public class GetChannelsResponse
{
    /// <summary>
    /// Unique identifier for the channel.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Channel name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Creation timestamp (UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Channel type.
    /// </summary>
    public ChannelType Type { get; set; }
}
