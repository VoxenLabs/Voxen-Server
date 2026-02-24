namespace Voxen.Server.Entities;

public class Channel
{
    public Guid Id { get; set; }

    public Guid ServerId { get; set; }
    public Server Server { get; set; } = null!;

    public string Name { get; set; } = null!;
    public ChannelType Type { get; set; }

    public ICollection<Message>? Messages { get; set; }
}

public enum ChannelType
{
    Text = 1,
    Voice = 2
}
