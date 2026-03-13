using Voxen.Server.Enums;

namespace Voxen.Server.Endpoints.Channels.CreateChannel;

/// <summary>
/// Represents a request to create a new channel within the server.
/// </summary>
public class CreateChannelRequest
{
    /// <summary>
    /// Gets or sets the name of the channel to be created.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the type of the channel to be created.
    /// </summary>
    public ChannelType Type { get; set; }
}
