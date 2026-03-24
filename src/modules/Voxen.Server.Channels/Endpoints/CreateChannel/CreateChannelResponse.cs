using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Channels.Endpoints.CreateChannel;

public class CreateChannelResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public ChannelType Type { get; set; }
}
