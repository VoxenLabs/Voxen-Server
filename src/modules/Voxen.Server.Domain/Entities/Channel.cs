using System.Text.Json.Serialization;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Domain.Entities;

/// <summary>
/// Represents a communication channel.
/// </summary>
public class Channel
{
    /// <summary>
    /// Unique identifier for the channel.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Channel name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Creation timestamp (UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Channel type.
    /// </summary>
    public ChannelType Type { get; set; }

    /// <summary>
    /// Messages in the channel.
    /// </summary>
    [JsonIgnore]
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
