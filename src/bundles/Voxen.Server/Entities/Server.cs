namespace Voxen.Server.Entities;

public class Server
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    /// <summary>
    /// Raw server logo bytes (e.g. PNG/JPEG).
    /// </summary>
    public byte[]? Logo { get; set; }

    /// <summary>
    /// MIME type for <see cref="Logo"/> (e.g. image/png).
    /// </summary>
    public string? LogoContentType { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Channel> Channels { get; set; } = new List<Channel>();
}
