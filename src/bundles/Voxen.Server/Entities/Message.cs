namespace Voxen.Server.Entities;


public class Message
{
    public Guid Id { get; set; }

    public Guid ChannelId { get; set; }
    public Channel Channel { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public required string Content { get; set; }

    public DateTime CreatedAt { get; set; }
}
