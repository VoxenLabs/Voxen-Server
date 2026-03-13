using Voxen.Server.Enums;

namespace Voxen.Server.Endpoints.Channels.CreateChannel;

public class CreateChannelRequest
{
    public string Name { get; set; } = null!;

    public ChannelType Type { get; set; }
}
