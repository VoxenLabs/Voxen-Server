namespace Voxen.Server.Entities;

public class Server
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Channel> Channels { get; set; } = new List<Channel>();
}
