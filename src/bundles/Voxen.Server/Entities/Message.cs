namespace Voxen.Server.Entities;

public class Message
{
    public Guid Id { get; set; }

    public Guid ChannelId { get; set; }
    public Channel Channel { get; set; } = null!;

    public Guid UserId { get; set; } = Guid.Empty;
    public User User { get; set; } = null!;

    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
