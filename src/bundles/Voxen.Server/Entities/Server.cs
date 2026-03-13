using System.Text.Json.Serialization;

namespace Voxen.Server.Entities;

public class Server
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    /// <summary>
    /// Date and time when the server was created (stored in UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Raw server logo bytes (e.g. PNG/JPEG).
    /// </summary>
    public byte[]? Logo { get; set; }

    /// <summary>
    /// MIME type for <see cref="Logo"/> (e.g. image/png).
    /// </summary>
    public string? LogoContentType { get; set; }

    [JsonIgnore] public ICollection<User> Users { get; set; } = new List<User>();
    [JsonIgnore] public ICollection<Channel> Channels { get; set; } = new List<Channel>();
}
