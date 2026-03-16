using System.Text.Json.Serialization;

namespace Voxen.Server.Domain.Entities;

/// <summary>
/// Represents a server entity.
/// </summary>
public class Server
{
    /// <summary>
    /// Gets or sets the unique identifier for the server.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the server.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the server was created (stored in UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the raw server logo bytes (e.g., PNG/JPEG).
    /// </summary>
    public byte[]? Logo { get; set; }
    
    /// <summary>
    /// Gets or sets the MIME type for the <see cref="Logo"/> (e.g., image/png).
    /// </summary>
    public string? LogoContentType { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of users associated with the server.
    /// </summary>
    [JsonIgnore]
    public ICollection<User> Users { get; set; } = new List<User>();
    
    /// <summary>
    /// Gets or sets the collection of channels associated with the server.
    /// </summary>
    [JsonIgnore]
    public ICollection<Channel> Channels { get; set; } = new List<Channel>();
}

